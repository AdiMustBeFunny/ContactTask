namespace Contacto.Domain.Entities;

public class ContactCategory
{
    private readonly List<ContactSubCategory> _contactSubCategories;

    private ContactCategory() { }

    internal ContactCategory(Guid id, string title, List<ContactSubCategory> contactSubCategories, bool customCategory = false)
    {
        Id = id;
        Title = title;
        _contactSubCategories = contactSubCategories;
        CustomCategory = customCategory;
    }

    public Guid Id { get; internal init; }

    public string Title { get; internal set; }

    public bool CustomCategory { get; internal set; }

    public IReadOnlyCollection<ContactSubCategory> ContactSubCategories => _contactSubCategories;

    public static readonly ContactCategory ContactCategoryWork = new(Guid.Parse("076B70FE-4E04-4007-B1D2-4516004ACB1D"), "Służbowy", []);

    public static readonly ContactCategory ContactCategoryPrivate = new(Guid.Parse("577DFADD-3C5E-48B1-B912-744EE62A4A1C"), "Prywatny", []);

    public static readonly ContactCategory ContactCategoryCustom = new(Guid.Parse("A137DA6B-2BB9-4C98-8D16-ABBCD426CA44"), "Inny", [], true);

    public static readonly ContactSubCategory ContactSubCategoryBoss = new(Guid.Parse("10977B9D-7547-43CA-86D5-F1C66EA8601A"), "Szef", ContactCategoryWork, ContactCategoryWork.Id);
    public static readonly ContactSubCategory ContactSubCategoryClient = new(Guid.Parse("21E22646-29C2-445C-ABFE-723469D29AA2"), "Klient", ContactCategoryWork, ContactCategoryWork.Id);
    public static readonly ContactSubCategory ContactSubCategoryCoworker = new(Guid.Parse("9CA3E265-692D-4DDF-B3E4-E06D731BF60D"), "Współpracownik", ContactCategoryWork, ContactCategoryWork.Id);
}
