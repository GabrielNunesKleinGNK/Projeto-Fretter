namespace System
{
    public static class DateTimeExtentions
    {
        public static bool IsWeekend(this DateTime self) => (self.DayOfWeek == DayOfWeek.Sunday || self.DayOfWeek == DayOfWeek.Saturday);
        public static bool IsLeapYear(this DateTime self) => DateTime.DaysInMonth(self.Year, 2) == 29;
        public static int Age(this DateTime self) => Age(self, DateTime.Today);

        public static int Age(this DateTime self, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - self.Year;
            if (age > 0)
                age -= Convert.ToInt32(laterDate.Date < self.Date.AddYears(age));
            else
                age = 0;

            return age;
        }
    }
}
