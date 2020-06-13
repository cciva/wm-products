using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Shop.Library.Model
{
    public class RepositoryConfig : ConfigurationSection
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public RepositoryType Type
        {
            get { return (RepositoryType)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("params", IsRequired = true)]
        public string Parameters
        {
            get { return (string)this["params"]; }
            set { this["params"] = value; }
        }
    }

    public enum RepositoryType
    {
        SqlDb,
        JsonFile
    }
}
