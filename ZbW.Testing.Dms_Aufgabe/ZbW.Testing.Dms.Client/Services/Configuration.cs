using System.Configuration;

namespace ZbW.Testing.Dms.Client.Services
{
    public class Configuration
    {
        public virtual string RepositoryDir => ConfigurationManager.AppSettings["RepositoryDir"];
    }
}