using System;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Common.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Involys.Poc.Api.services.DynamicLinq
{
    public class DynamicLinqService : IDynamicLinqService
    {
        private readonly IConfiguration _configuration;

        public DynamicLinqService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<dynamic> GetQuery(DynamicLinqModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder("select * from [" + model.TableName + "] ");

                if (model.JoinTables.Count > 0)
                {
                    foreach (var jt in model.JoinTables)
                    {
                        sb.Append("join [" + jt.TableName + "] on [" + jt.ParentTableName + "]." + jt.ParentColumnOn + "= [" + jt.TableName + "]." + jt.CurrentColumnOn + " ");
                    }
                }
                if (model.WhereConditions.Count > 0)
                {
                    
                    int count = 0;
                    sb.Append("where ");
                    foreach (var wc in model.WhereConditions)
                    {
                        
                        if(wc.ValueType == "string")
                        {
                            sb.Append("[" + wc.ConditionTable + "]." + wc.ConditionColumn + " " + wc.Condition + " '" + wc.Value +"'");
                        }
                        else if (wc.ValueType == "int")
                        {
                            sb.Append("[" + wc.ConditionTable + "]." + wc.ConditionColumn + " " + wc.Condition + " " + Convert.ToInt32(wc.Value));
                        }
                        
                        count++;
                        if (count < model.WhereConditions.Count)
                        {
                            sb.Append(" and ");
                        }
                    }
                }

                var query = sb.ToString();

                using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await con.OpenAsync();

                var result =  con.Query(query).AsList();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }
    }
}
