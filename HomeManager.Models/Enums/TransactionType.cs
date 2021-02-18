using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Enums
{
    [Serializable]
    public enum TransactionType
    {
        Both = 0,

        Debit = 1,

        Deposit = 2,
    }
}
