namespace DddSample.Domain;

public readonly struct WeeklyOpeningHours
{
  public WeeklyOpeningHours(bool worksOnHoliday = false, TimePeriod mon = default, TimePeriod tue = default, TimePeriod wed = default, TimePeriod thu = default, TimePeriod fri = default, TimePeriod sat = default, TimePeriod sun = default)
  {
    WorksOnHolidays = worksOnHoliday;
    Mon = mon;
    Tue = tue;
    Wed = wed;
    Thu = thu;
    Fri = fri;
    Sat = sat;
    Sun = sun;
  }

  public bool WorksOnHolidays { get; }

  public TimePeriod Mon { get; }
  public TimePeriod Tue { get; }
  public TimePeriod Wed { get; }
  public TimePeriod Thu { get; }
  public TimePeriod Fri { get; }
  public TimePeriod Sat { get; }
  public TimePeriod Sun { get; }
}
