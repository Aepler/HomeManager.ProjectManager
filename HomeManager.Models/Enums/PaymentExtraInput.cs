using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Enums
{
    [Serializable]
    public enum PaymentExtraInput
    {
        NoValue = 0,

        ExtraCost = 1,

        DetailedTax = 2,

        Category = 3,

        Warranty = 4
    }
}
