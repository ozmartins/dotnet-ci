using FluentValidation;
using FluentValidation.Results;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Validations
{
    public class PersonValidation : AbstractValidator<Person>, IPersonValidation
    {
        private IPhoneValidation _phoneValidation;

        private IPersonPhoneValidation _personPhoneValidation;

        private IAddressValidation _addressValidation;

        private IPersonAddressValidation _personAddressValidation;

        private IPaymentPlanValidation _paymentPlanValidation;

        public PersonValidation(IRepository<Person> personRepository,
                                IRepository<PaymentPlan> paymentPlanRepository,
                                IFilterBuilder<Person> personFilterBuilder,
                                IPhoneValidation phoneValidation,
                                IPersonPhoneValidation personPhoneValidation,
                                IAddressValidation addressValidation,
                                IPersonAddressValidation personAddressValidation,
                                IPaymentPlanValidation paymentPlanValidation)
        {
            _phoneValidation = phoneValidation;

            _personPhoneValidation = personPhoneValidation;

            _addressValidation = addressValidation;

            _personAddressValidation = personAddressValidation;

            _paymentPlanValidation = paymentPlanValidation;

            RuleFor(p => p.Name).NotEmpty().WithMessage("O nome da pessoa não foi informado.");

            RuleFor(p => p.SupplierOrCustomer).IsInEnum().WithMessage("O campo 'Cliente ou Fornecedor' está com um valor inválido.");

            RuleFor(p => p.Document).Length(x => isCPF(x.Document) ? 11 : isCNPJ(x.Document) ? 14 : 0).WithMessage("O CPF/CNPJ precisa ter 11 ou 14 dígitos.");

            RuleFor(p => p.Document).Must(x => x.All(char.IsDigit)).WithMessage("O número do documento deve conter apenas números");

            RuleFor(p => documentAlreadyExists(personRepository, personFilterBuilder, p)).Equal(false).WithMessage("Já existe uma pessoa cadastrada com o documento informado.");

            RuleFor(p => documentIsValid(p.Document)).Equal(true).WithMessage("O documento informado é inválido.");

            RuleFor(p => p.CustomerInfo.BirthDate).LessThan(DateTime.Today).WithMessage("A data de nascimento não pode ser maior que a data atual.");

            RuleFor(p => businessDescriptionIsEmpty(p)).Equal(false).WithMessage("A descrição do negócio não foi informada.");

            RuleForEach(p => p.SupplierInfo.PaymentPlans).ChildRules(pay => pay.RuleFor(x => paymentPlanRepository.RecoverById(x.Id)).NotNull().WithMessage("O plano de pagamento informado não existe."));
        }

        public ValidationResult CustomValidate(Person person)
        {
            var result = Validate(person);

            if (!result.IsValid) return result;

            foreach (var phone in person.Phones)
            {
                result = _phoneValidation.Validate(phone);

                if (!result.IsValid) return result;
            }

            foreach (var address in person.Addresses)
            {
                result = _addressValidation.Validate(address);

                if (!result.IsValid) return result;
            }

            foreach (var paymentPlan in person.SupplierInfo.PaymentPlans)
            {
                result = _paymentPlanValidation.Validate(paymentPlan);

                if (!result.IsValid) return result;
            }

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid) return result;

            result = _personAddressValidation.Validate(person);

            return result;
        }

        private bool isCPF(string document)
        {
            return document.Length == 11;
        }

        private bool isCNPJ(string document)
        {
            return document.Length == 14;
        }

        private bool documentAlreadyExists(IRepository<Person> personRepository, IFilterBuilder<Person> filterBuilder, Person person)
        {
            if (String.IsNullOrEmpty(person.Document)) return false;

            filterBuilder
                .Equal(x => x.Document, person.Document)
                .Unequal(x => x.Id, person.Id);

            return personRepository.Recover(filterBuilder).Count > 0;
        }

        private bool documentIsValid(string document)
        {
            if (String.IsNullOrEmpty(document)) return true;

            if (isCPF(document))
                return IsCpfValid(document);
            else if (isCNPJ(document))
                return IsCnpjValid(document);
            else
                return false;
        }

        private bool businessDescriptionIsEmpty(Person person)
        {
            return person.SupplierOrCustomer == SupplierOrCustomer.Supplier && string.IsNullOrEmpty(person.SupplierInfo.BusinessDescription);
        }

        private bool IsCpfValid(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest.ToString();
            return cpf.EndsWith(digit);
        }

        private bool IsCnpjValid(string cnpj)
        {
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int rest;
            string digit;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest.ToString();
            return cnpj.EndsWith(digit);
        }
    }
}
