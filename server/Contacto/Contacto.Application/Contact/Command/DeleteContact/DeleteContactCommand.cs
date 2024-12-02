using Contacto.Domain.Abstractions;
using Contacto.Utilities;

namespace Contacto.Application.Contact.Command.DeleteContact;

public record DeleteContactCommand(Guid Id) :ICommand<Result>;