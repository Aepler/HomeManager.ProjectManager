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

        Extra_Amount = 1,

        TaxList = 2
    }
}
