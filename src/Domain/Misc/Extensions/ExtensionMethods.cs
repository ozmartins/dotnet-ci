namespace Domain.Infra.Extensions
{
    public static class ExtensionMethods
    {
        public static T? ExceptionIfNull<T>(this T obj, string msg) where T : class
        {
            if (obj == null)
                throw new Exception(msg);
            return obj;
        }

        public static T? IfNull<T>(this T? obj, Action action) where T : class
        {
            if (obj == null)
                action?.Invoke();
            return obj;
        }

        public static T? If<T>(this T obj, Action action) where T : class
        {
            if (obj == null)
                action?.Invoke();
            return obj;
        }

        public static DateTime EndOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
    }
}
