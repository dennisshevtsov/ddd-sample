namespace DddSample.Domain;

public readonly struct Phone
{
  public const int Length = 12;

  private readonly string _phone;

  private Phone(string phone)
  {
    ArgumentNullException.ThrowIfNullOrWhiteSpace(phone);
    
    if (phone.Length != Length)
    {
      throw new DomainException($"Invalid phone: {phone}. Phone length must be {Length}.");
    }

    for (int i = 0; i < phone.Length; i++)
    {
      if (phone[i] < '0' ||  phone[i] > '9')
      {
        throw new DomainException($"Invalid phone: {phone}. Phone must contain only digits.");
      }
    }

    _phone = phone;
  }

  public static Phone Parse(string phone) => new(phone);
  public static implicit operator string(Phone phone) => phone._phone;
}
