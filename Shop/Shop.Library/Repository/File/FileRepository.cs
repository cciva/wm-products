using Newtonsoft.Json;
using Shop.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shop.Library.Repository
{
    public class FileRepository : IRepository
    {
        string _target = string.Empty;

        public FileRepository(string file)
        {
            _target = file;
        }

        public Status Delete<T>(T obj) where T : class, new()
        {
            Status status = Status.Ok;
            IEnumerable<T> collection = this.Load<T>();

            if (collection == null || collection.Count() == 0)
                return new Status(new InvalidOperationException("collection is empty"));

            return status;
        }

        public Status Insert<T>(T obj) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Load<T>(object filter = null) where T : class, new()
        {
            Status status = Status.Ok;
            IEnumerable<T> collection = Enumerable.Empty<T>();

            try
            {
                string content = File.ReadAllText(_target);
                collection = JsonConvert.DeserializeObject<List<T>>(content);
            }
            catch (Exception err)
            {
                status = new Status(err);
            }
            finally
            {
                
            }

            return collection;
        }

        public Status Update<T>(T obj) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public RepositoryType Kind
        {
            get { return RepositoryType.JsonFile; }
        }
    }
}
