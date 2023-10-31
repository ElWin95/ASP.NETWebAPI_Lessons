namespace ShopAppP416.Helpers
{
    public static class DateTimeGetDayExtension
    {
        public static int CalculateDay(this DateTime date)
        {
            return DateTime.Now.Day - date.Day;
        }
    }
}
