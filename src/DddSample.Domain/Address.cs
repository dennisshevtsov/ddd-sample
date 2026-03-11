namespace DddSample.Domain;

public readonly struct Address
{
  private readonly string _address;

  private Address(string address) => _address = address;

  public readonly static Address None;

  public static Address Parse(string value)
  {
    ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
    return new Address(value);
  }

  public override string ToString() => _address;

  public static implicit operator string(Address address) => address.ToString();
}

