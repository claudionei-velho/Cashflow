namespace Domain.Extensions {
  public static class CustomCalendar {
    public const int WeeksPerYear = 52;
    public const int MonthsPerYear = 12;

    public static int DaysPerYear(int year) {
      return !IsLeapYear(year) ? 365 : 366;
    }

    private static bool IsLeapYear(int year) {
      return (year % 400 == 0 || year % 100 != 0) && year % 4 == 0;
    }
  }
}
