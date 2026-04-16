using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct TimePeriod : IEquatable<TimePeriod>
{
  private readonly TimeOnly _from;
  private readonly TimeOnly _to;

  public TimePeriod(TimeOnly from, TimeOnly to)
  {
    if (from >= to)
    {
      throw new ArgumentException($"Value From {from} must be less than value To {to}");
    }

    _from = from;
    _to = to;
  }

  public static readonly TimePeriod None;

  public override int GetHashCode() => HashCode.Combine(_from.GetHashCode(), _to.GetHashCode());

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not TimePeriod other) return false;
    return Equals(this, other);
  }

  public bool Equals(TimePeriod other) => _from == other._from && _to == other._to;

  public override string ToString()
  {
    if (this == TimePeriod.None)
    {
      return string.Empty;
    }
    return $"{_from:HH:mm}-{_to:HH:mm}";
  }

  public static TimePeriod Parse(string value)
  {
    ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

    ReadOnlySpan<char> trimmed = value.AsSpan().Trim();

    // 00:00-00:00
    // 012345678910
    if (trimmed.Length != 11 ||
        trimmed[2] != ':' || trimmed[5] != '-' || trimmed[8] != ':' ||
        !char.IsDigit(trimmed[0]) || !char.IsDigit(trimmed[1]) ||
        !char.IsDigit(trimmed[3]) || !char.IsDigit(trimmed[4]) ||
        !char.IsDigit(trimmed[6]) || !char.IsDigit(trimmed[6]) ||
        !char.IsDigit(trimmed[9]) || !char.IsDigit(trimmed[10]))
    {
      throw new DomainException($"Invalid format of value {value} to parse {nameof(TimePeriod)}");
    }

    TimeOnly from = TimeOnly.Parse(trimmed[0..5]);
    TimeOnly to = TimeOnly.Parse(trimmed[6..11]);
    TimePeriod period = new(from, to);
    return period;
  }

  public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);
  public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);
}
