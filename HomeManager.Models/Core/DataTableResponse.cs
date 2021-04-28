using HomeManager.Models.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Core
{
    [Serializable]
    public class DataTableResponse : IDataTableResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    [Serializable]
    public class DataTableResponse<T> : DataTableResponse, IDataTableResponse<T>
    {
        public DataTableResponse()
        {
        }

        public DataTableResponse(IDataTableResponse dataTableResponse)
        {
            if (dataTableResponse == null)
            {
                throw new ArgumentNullException("dataTableResponse");
            }

            this.draw = dataTableResponse.draw;
            this.recordsTotal = dataTableResponse.recordsTotal;
            this.recordsFiltered = dataTableResponse.recordsFiltered;
        }

        public IEnumerable<T> data { get; set; }
    }

}
