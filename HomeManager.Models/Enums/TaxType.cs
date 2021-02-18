using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Enums
{
    [Serializable]
    public enum TaxType
    {
        NoValue = 0,

        Net = 1,

        Gross = 2,
    }
}
