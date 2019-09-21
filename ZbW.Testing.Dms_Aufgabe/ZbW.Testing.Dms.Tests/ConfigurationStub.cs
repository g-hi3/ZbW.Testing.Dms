using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Tests
{
    public class ConfigurationStub : Configuration
    {
        public override string RepositoryDir => @"C:\Temp\DMS";
    }
}