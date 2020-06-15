using Dapper;
using Shop.Library.Model;
using Shop.Library.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library
{
    public class DbCategories : SqlRepositoryAdapter<Category>
    {
        protected override IEnumerable<Category> ReadData(
            IDbConnection connection, 
            Predicate<Category> filter = null)
        {
            return connection.Query<Category>("dbo.sp_get_categories", commandType: CommandType.StoredProcedure);
        }

        protected override Status WriteData(
            IDbConnection connection, Category item, 
            Operation op, Predicate<Category> where = null)
        {
            throw new NotImplementedException();
        }
    }
}
