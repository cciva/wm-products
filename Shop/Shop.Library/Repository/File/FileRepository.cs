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
        public IEnumerable<T> Load<T>(Predicate<T> filter = null) where T : class, new()
        {
            return this.OnLoad<T>(filter);
        }

        public Status Delete<T>(T obj, Predicate<T> where = null) where T : class, new()
        {
            List<T> collection = new List<T>(this.Load<T>());

            if (collection == null || collection.Count() == 0)
                return new Status(new InvalidOperationException("collection is empty"));

            T item = collection.Find(where);

            if (item == null)
                return new Status(new Exception("not found"));

            if (!collection.Remove(item))
                return new Status(new Exception("not found"));

            return OnCommit<T>(collection);
        }

        public Status Insert<T>(T obj) where T : class, new()
        {
            List<T> collection = new List<T>(this.Load<T>());

            if (collection == null || collection.Count() == 0)
                return new Status(new InvalidOperationException("collection is empty"));

            collection.Add(obj);

            return OnCommit<T>(collection);
        }

        public Status Update<T>(T obj, Predicate<T> where = null) where T : class, new()
        {
            List<T> collection = new List<T>(this.Load<T>());

            if (collection == null || collection.Count() == 0)
                return new Status(new InvalidOperationException("collection is empty"));

            T existing = collection.Find(where);
            if (existing != null)
            {
                collection.Remove(existing);
                collection.Add(obj);
            }

            return OnCommit<T>(collection);
        }

        public DbResult Execute<T>(Operation op, IDictionary<string, object> args = null)
            where T : class, new()
        {
            if(op == Operation.Count)
            {
                return new DbResult(this.Load<T>().Count(), Status.Ok);
            }

            return new DbResult(null, Status.Ok);
        }

        public RepositoryType Kind
        {
            get { return RepositoryType.JsonFile; }
        }

        private IEnumerable<T> OnLoad<T>(Predicate<T> filter = null) where T : class, new()
        {
            Type target = typeof(T);

            if (!FileConfig.ModelMap.ContainsKey(target))
                throw new InvalidOperationException(string.Format("can not read or write model '{0}'", target));

            Status status = Status.Ok;
            IEnumerable<T> collection = Enumerable.Empty<T>();
            string file = FileConfig.ModelMap[target];

            try
            {
                string content = File.ReadAllText(file);
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

        private Status OnCommit<T>(IEnumerable<T> collection)
        {
            string json = string.Empty;
            string backup = string.Empty;
            Status status = Status.Ok;
            Type target = typeof(T);

            if (!FileConfig.ModelMap.ContainsKey(target))
                throw new InvalidOperationException(string.Format("can not read or write model '{0}'", target));

            string file = FileConfig.ModelMap[target];

            try
            {
                backup = File.ReadAllText(file);
                json = JsonConvert.SerializeObject(collection);

                using (FileStream fs = new FileStream(file, FileMode.Truncate, FileAccess.Write))
                {
                    byte[] jsonb = Encoding.UTF8.GetBytes(json);
                    fs.Write(jsonb, 0, jsonb.Length);
                }
            }
            catch(Exception e)
            {
                status = new Status(e);
                File.WriteAllText(file, backup);
            }

            return status;
        }
    }
}
