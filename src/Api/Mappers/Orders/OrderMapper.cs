using AutoMapper;
using Api.Dtos.Addresses;
using Api.Dtos.Orders;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Interfaces.Mapppers;
using Api.Views.Orders;
using Domain.Infra.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models.Addresses;
using Domain.Models.Orders;
using Domain.Models.PaymentPlans;

namespace Api.Mappers.Orders
{
    public class OrderMapper : BaseMapper<Order>, IOrderMapper
    {
        private readonly ISupplierService _supplierService;

        private readonly ICustomerService _customerService;

        private readonly IRepository<PaymentPlan> _paymentPlanRepository;

        private readonly IAddressMapper _addressMapper;

        private readonly IOrderItemMapper _orderItemMapper;

        private readonly IMapper _autoMapper;

        public OrderMapper(ISupplierService supplierService,
                           ICustomerService customerService,
                           IRepository<PaymentPlan> paymentPlanRepository,
                           IAddressMapper addressMapper,
                           IOrderItemMapper orderItemMapper,
                           IMapper autoMapper)
        {
            _supplierService = supplierService;
            _customerService = customerService;
            _paymentPlanRepository = paymentPlanRepository;
            _addressMapper = addressMapper;
            _orderItemMapper = orderItemMapper;
            _autoMapper = autoMapper;
        }

        public MapperResult<Order> Map(OrderDto dto)
        {
            var supplier = _supplierService.Get(dto.SupplierId).IfNull(() => { AddError("O fornecedor informado não existe."); });

            var customer = _customerService.Get(dto.CustomerId).IfNull(() => { AddError("O cliente informado não existe."); });

            var paymentPlanForOrder = mapPaymentPlan(dto);

            var shippingAddress = mapShippingAddress(dto.ShippingAddress);

            var items = new List<OrderItem>();

            if (!SuccessResult()) return GetResult();

            var order = new Order(
                _autoMapper.Map<PersonForOrder>(supplier),
                _autoMapper.Map<PersonForOrder>(customer),
                dto.Freight,
                dto.Notes,
                dto.PartyDate,
                items);

            if (shippingAddress != null)
                order.DefineShippingAddress(shippingAddress);

            if (paymentPlanForOrder != null)
                order.DefinePaymentPlan(paymentPlanForOrder);

            SetEntity(order);

            return GetResult();
        }

        private PaymentPlanForOrder? mapPaymentPlan(OrderDto? dto)
        {
            var paymentPlan = _paymentPlanRepository.RecoverById(dto?.PaymentPlanId ?? Guid.Empty).IfNull(() => { AddError("O plano de pagamento informado não existe."); });

            if (paymentPlan == null) return null;

            var installment = paymentPlan.Instalments.Where(x => x.Quantity == (dto?.Installments ?? 0)).FirstOrDefault();

            if (installment == null)
                AddError("O plano de pagamento informado não aceita a quantidade de parcelas que foi informada.");

            return new PaymentPlanForOrder(paymentPlan.Id, paymentPlan.PaymentMethod, dto?.Installments ?? 0, installment!.Fee);

        }

        public OrderView? Map(Order? order)
        {
            return MapToView(order);
        }

        public List<OrderView> Map(List<Order> orders)
        {
            var orderViewList = new List<OrderView>();

            foreach (var order in orders)
            {
                var mapped = MapToView(order);
                if (mapped != null)
                    orderViewList.Add(mapped);
            }

            return orderViewList;
        }

        private static OrderView? MapToView(Order? order)
        {
            if (order == null) return null;

            var orderView = new OrderView()
            {
                Id = order.Id,
                Version = order.Version,
                DateTime = order.DateTime,
                CustomerId = order.Customer.Id,
                SupplierId = order.Supplier.Id,
                ShippingAddressId = order.ShippingAddress?.Id ?? Guid.Empty,
                Freight = order.Freight,
                PaymentPlanFee = order.PaymentPlanFee,
                ItemsTotal = order.ItemsTotal,
                OrderTotal = order.OrderTotal,
                PaymentPlanId = order.PaymentPlan?.Id ?? Guid.Empty,
                Notes = order.Notes,
                Status = order.Status,
                PartyDate = order.PartyDate,
                ExpirationDate = order.ExpirationDate,
            };

            return orderView;
        }

        private Address? mapShippingAddress(AddressDto shippingAddress)
        {
            var result = _addressMapper.Map(shippingAddress);

            if (result.Success)
                return result.Entity;
            else
            {
                foreach (var erro in result.Errors) AddError(erro);

                return null;
            }
        }
    }
}
