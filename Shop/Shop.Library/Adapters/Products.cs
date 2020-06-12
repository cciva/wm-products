using Dapper;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Shop.Library
{
    public class DbProducts : SqlStoreAdapter<Product>
    {
        public DbProducts()
        {
            //Dictionary<string, string> map = new Dictionary<string, string>
            //{
            //    /*    Column => Property    */
            //    { "Id",             "Id"          },
            //    { "Description",    "Description" },
            //    { "CategoryId",     "CategoryId"  },
            //    { "Make",           "Make"        },
            //    { "Supplier",       "Supplier"    },
            //    { "Price",          "Price"       }
            //};

            //var mapper = new Func<Type, string, PropertyInfo>((type, col) =>
            //{
            //    if (map.ContainsKey(col))
            //        return type.GetProperty(map[col]);
            //    else
            //        return type.GetProperty(col);
            //});

            //// Create customer mapper for User object
            //var prodmap = new CustomPropertyTypeMap(
            //    typeof(Product),
            //    (type, col) => mapper(type, col));

            //SqlMapper.SetTypeMap(typeof(Product), prodmap);
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
            DynamicParameters parameters = new DynamicParameters();

            switch (op)
            {
                case Operation.Insert:
                    parameters.Add("@id", item.Id, DbType.String);
                    parameters.Add("@description", item.Description, DbType.String);
                    parameters.Add("@category", item.CategoryId, DbType.Int32);
                    parameters.Add("@make", item.Make, DbType.String);
                    parameters.Add("@supplier", item.Supplier, DbType.String);
                    parameters.Add("@price", item.Price, DbType.Decimal);
                    break;
                case Operation.Update:
                    break;
            }

            result = connection.Execute("[dbo].[sp_insert_product]", parameters, null, null, CommandType.StoredProcedure);

            return (result == 0) 
                ? new Status(new Exception("Unable to insert data")) 
                : Status.Ok;
        }
    }
}