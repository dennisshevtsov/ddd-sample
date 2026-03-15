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
      throw new ArgumentException($"Value From {from} must less than value To {to}");
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
    return $"{_from} - {_to}";
  }

  public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);
  public static bool operator !=(TimePeriod left, TimePeriod right) => !left.Equals(right);
}
