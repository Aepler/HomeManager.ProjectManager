using HomeManager.Models.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.DataTableModels
{
    [Serializable]
    public class DataTableResultModel : IDataTableResultModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    [Serializable]
    public class DataTableResultModel<T> : DataTableResultModel, IDataTableResultModel<T>
    {
        public DataTableResultModel()
        {
        }

        public DataTableResultModel(IDataTableResultModel dataTableResultModel)
        {
            if (dataTableResultModel == null)
            {
                throw new ArgumentNullException("dataTableResultModel");
            }

            this.draw = dataTableResultModel.draw;
            this.recordsTotal = dataTableResultModel.recordsTotal;
            this.recordsFiltered = dataTableResultModel.recordsFiltered;
        }

        public IEnumerable<T> data { get; set; }
    }

}
