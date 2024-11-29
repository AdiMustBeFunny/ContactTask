namespace Contacto.Domain.Entities;

public class ContactSubCategory
{
    private ContactSubCategory() { }

    internal ContactSubCategory(Guid id, string title, ContactCategory category)
    {
        Id = id;
        Title = title;
        Category = category;
    }

    public Guid Id { get; internal init; }

    public string Title { get; internal set; }

    public ContactCategory Category { get; internal set; }

}
