using System;


namespace AuthorizationServer.OperationalClasses
{
    public static class Extensions
    {
        public static int ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }

            int result = -1;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException();
        }
    }
}