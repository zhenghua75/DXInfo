using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FairiesCoolerCash.Business
{
    public interface IValidationExceptionHandler
    {
        void ValidationExceptionsChanged(int count);
    }
}
