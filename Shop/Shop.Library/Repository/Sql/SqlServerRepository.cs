﻿using Shop.Library.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Shop.Library.Repository
{
    public class SqlServerRepository : IRepository
    {
        string _connId = string.Empty;
        string _conn = string.Empty;

        public SqlServerRepository(string connectionId)
        {
            _connId = connectionId;

            if (!SqlCache.ConnParams.ContainsKey(_connId))
                throw new InvalidOperationException(
                    string.Format("can not find sql connection parameters '{0}' inside configuration", _connId));

            _conn = SqlCache.ConnParams[_connId];
        }

        public IEnumerable<T> Load<T>(Predicate<T> filter = null) where T : class, new()
        {
            SqlConnection connection = new SqlConnection(_conn);
            IEnumerable<T> collection = Enumerable.Empty<T>();

            try
            {
                ISqlRepositoryAdapter adapter = SqlCache.AdapterFor<T>();
                if (adapter == null)
                    throw new InvalidOperationException(
                        string.Format("unable to use model '{0}' with sql store - not registered", typeof(T)));

                connection.Open();
                collection = (adapter as ISqlRepositoryAdapter<T>).Read(connection);
            }
            catch (Exception err)
            {

            }
            finally
            {
                connection.Close();
            }

            return collection;
        }

        public Status Delete<T>(T obj, Predicate<T> where = null) where T : class, new()
        {
            return this.Write<T>(obj, Operation.Delete, where);
        }

        public Status Insert<T>(T obj) where T : class, new()
        {
            return this.Write<T>(obj, Operation.Insert);
        }

        public Status Update<T>(T obj, Predicate<T> where = null) where T : class, new()
        {
            return this.Write<T>(obj, Operation.Update, where);
        }

        public DbResult Execute<T>(Operation op, IDictionary<string, object> args = null)
            where T : class, new()
        {
            // TBD
            return new DbResult(null, Status.Ok);
        }

        private Status Write<T>(T obj, Operation op, Predicate<T> where = null) where T : class, new()
        {
            SqlConnection connection = new SqlConnection(_conn);
            Status status = Status.Ok;

            try
            {
                ISqlRepositoryAdapter adapter = SqlCache.AdapterFor<T>();
                if (adapter == null)
                    throw new InvalidOperationException(
                        string.Format("unable to use model '{0}' with sql store - not registered", typeof(T)));

                connection.Open();
                status = adapter.Write(connection, obj, op);
            }
            catch (Exception err)
            {
                status = new Status(err);
            }
            finally
            {
                connection.Close();
            }

            return status;
        }

        public RepositoryType Kind
        {
            get { return RepositoryType.SqlDb; }
        }
    }
}
