using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSource.Model
{
    public class DataSourceResponse : BaseEntity
    {
       
        public string Designation { get; set; }
        public string SqlText { get; set; }
        public bool Distinct { get; set; }
        /// <summary>
        /// classification of data source
        /// </summary>
        public ClassificationResponse Classification { get; set; }
        public IList<DataSourceFieldResponse> DataSourceFields { get; set; }
        public ExpressionTermDataModel WhereCondition { get; set; }
        public ExpressionTermDataModel HavingCondition { get; set; }
    }
}
