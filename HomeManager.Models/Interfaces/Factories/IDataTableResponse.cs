using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Factories
{
    public interface IDataTableResponse
    {
        int draw { get; set; }
        int recordsTotal { get; set; }
        int recordsFiltered { get; set; }
    }

    public interface IDataTableResponse<T> : IDataTableResponse
    {
        IEnumerable<T> data { get; set; }
    }
}
