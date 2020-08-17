using Facade.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Domain
{
    public interface ISendEmailFacade
    {
        // O retorno dessa classe é o mesmo retorno do terceiro que ela esta implementando; Neste caso aqui é IAllInClient
        bool SendEmailTransactionalAllIn(ParameterList parameterList, PersonMessage personMessage);
    }
}
