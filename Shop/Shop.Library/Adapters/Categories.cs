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
        protected override IEnumerable<Category> ReadData(IDbConnection connection, object filter = null)
        {
            return connection.Query<Category>("dbo.sp_get_categories", commandType: CommandType.StoredProcedure);
        }

        protected override Status WriteData(IDbConnection connection, Category item, Operation op)
        {
            throw new NotImplementedException();
        }
    }
}
