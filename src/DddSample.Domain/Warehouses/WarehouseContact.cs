namespace DddSample.Domain.Warehouses;

public sealed class WarehouseContact
{
  public WarehouseContact()
  {
    _emails = [];
    _phones = [];
  }

  public WarehouseContact(IReadOnlyList<Email> emails, IReadOnlyList<Phone> phones)
  {
    _emails = [.. emails];
    _phones = [.. phones];
  }

  private readonly IList<Email> _emails;
  public IReadOnlyList<Email> Emails => _emails.AsReadOnly();

  private readonly IList<Phone> _phones;
  public IReadOnlyList<Phone> Phones => _phones.AsReadOnly();
}
