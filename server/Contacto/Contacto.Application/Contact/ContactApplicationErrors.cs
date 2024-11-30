using Contacto.Application.User;
using Contacto.Utilities;

namespace Contacto.Application.Contact;

public class ContactApplicationErrors
{
    public static readonly string NoContactWithGivenIdentifierId = $"{nameof(ContactApplicationErrors)}.{nameof(NoContactWithGivenIdentifier)}";
    public static readonly Error NoContactWithGivenIdentifier = new(NoContactWithGivenIdentifierId, "There is no contact with this id");

    public static readonly string ContactCategoryDoesntExistId = $"{nameof(ContactApplicationErrors)}.{nameof(ContactCategoryDoesntExist)}";
    public static readonly Error ContactCategoryDoesntExist = new(ContactCategoryDoesntExistId, "Contact category doesn't exist");

    public static readonly string ContactSubCategoryDoesntExistId = $"{nameof(ContactApplicationErrors)}.{nameof(ContactSubCategoryDoesntExist)}";
    public static readonly Error ContactSubCategoryDoesntExist = new(ContactSubCategoryDoesntExistId, "Contact sub-category doesn't exist");
}
