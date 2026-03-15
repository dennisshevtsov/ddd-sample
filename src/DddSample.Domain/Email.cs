namespace DddSample.Domain;

public readonly struct Email
{
  private readonly string _email;

  private Email(string email)
  {
    ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

    if (!HasValidFormat(email))
    {
      throw new DomainException($"Invalid email: {email}. Email must contain only one symbol @.");
    }

    _email = email;
  }

  public override string ToString() => _email;

  private static bool HasValidFormat(string value)
  {
    int atSings = 0;
    for (int i = 0; i < value.Length; i++)
    {
      if (value[i] == '@')
      {
        atSings++; 
      }
    }
    return atSings == 1;
  }

  public static Email Parse(string value) => new(value);
  public static implicit operator string(Email email) => email._email;
}
