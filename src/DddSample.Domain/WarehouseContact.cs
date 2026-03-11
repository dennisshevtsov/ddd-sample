namespace DddSample.Domain;

public sealed class WarehouseContact
{
  public WarehouseContact()
  {
    _phones = [];
    _emails = [];
  }

  public WarehouseContact(IReadOnlyList<Phone> phones, IReadOnlyList<Email> emails)
  {
    _phones = [.. phones];
    _emails = [.. emails];
  }

  private readonly IList<Phone> _phones;
  public IReadOnlyList<Phone> Phones => _phones.AsReadOnly();

  private readonly IList<Email> _emails;
  public IReadOnlyList<Email> Emails => _emails.AsReadOnly();
}
