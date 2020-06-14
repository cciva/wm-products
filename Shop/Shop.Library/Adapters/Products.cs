using Dapper;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Shop.Library
{
    public class DbProducts : SqlRepositoryAdapter<Product>
    {
        public DbProducts()
        {
        }

        protected override IEnumerable<Product> ReadData(
            IDbConnection connection, 
            object filter = null)
        {
            return connection.Query<Product>("dbo.sp_get_products", commandType: CommandType.StoredProcedure);
        }

        protected override Status WriteData(
            IDbConnection connection, 
            Product item, 
            Operation op)
        {
            int result = 0;
            string cmd = string.Empty;
            DynamicParameters parameters = new DynamicParameters();

            switch (op)
            {
                case Operation.Insert:
                    parameters.Add("@description", item.Description, DbType.String);
                    parameters.Add("@category", item.CategoryId, DbType.Int32);
                    parameters.Add("@make", item.Make, DbType.String);
                    parameters.Add("@supplier", item.Supplier, DbType.String);
                    parameters.Add("@price", item.Price, DbType.Decimal);
                    cmd = "[dbo].[sp_insert_product]";
                    break;
                case Operation.Delete:
                    parameters.Add("@id", item.Id, DbType.Int32);
                    cmd = "[dbo].[sp_delete_product]";
                    break;
                case Operation.Update:
                    parameters.Add("@id", item.Id, DbType.Int32);
                    parameters.Add("@description", item.Description, DbType.String);
                    parameters.Add("@category", item.CategoryId, DbType.Int32);
                    parameters.Add("@make", item.Make, DbType.String);
                    parameters.Add("@supplier", item.Supplier, DbType.String);
                    parameters.Add("@price", item.Price, DbType.Decimal);
                    cmd = "[dbo].[sp_update_product]";
                    break;
            }

            result = connection.Execute(cmd, parameters, null, null, CommandType.StoredProcedure);

            return (result == 0) 
                ? new Status(new Exception(string.Format("unable to execute {0}", cmd))) 
                : Status.Ok;
        }
    }
}