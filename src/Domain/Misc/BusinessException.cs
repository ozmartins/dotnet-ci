namespace Domain.Infra
{
    public class BusinessException : Exception
    {
        public BusinessException(string? message) : base(message)
        {
        }
    }
}
