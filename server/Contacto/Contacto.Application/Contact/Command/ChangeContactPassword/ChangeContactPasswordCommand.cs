using Contacto.Domain.Abstractions;
using Contacto.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.Application.Contact.Command.ChangeContactPassword
{
    public record ChangeContactPasswordCommand(Guid ContactId, string Password) : ICommand<Result>; 
}
