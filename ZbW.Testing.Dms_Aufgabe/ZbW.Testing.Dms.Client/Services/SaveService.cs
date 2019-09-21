using System.Configuration;
using System.IO;

namespace ZbW.Testing.Dms.Client.Services
{
    public class SaveService
    {
        private const char PathSeparator = '\\';

        private readonly Configuration _configuration;

        public SaveService(Configuration config) => _configuration = config;

        public SaveService() : this(new Configuration()) {}
        
        public void SaveDocument(string sourceFilePath, bool keepOriginal)
        {
            var fileName = GetFileNameFromPath(sourceFilePath);
            var destinationFilePath = _configuration.RepositoryDir + PathSeparator + fileName;
            
            if (keepOriginal)
                File.Copy(sourceFilePath, destinationFilePath);
            else
                File.Move(sourceFilePath, destinationFilePath);
        }

        private string GetFileNameFromPath(string filePath)
        {
            var pathParts = filePath.Split(PathSeparator);
            return pathParts[pathParts.Length - 1];
        }

        public string RepositoryPath => ConfigurationManager.AppSettings["RepositoryDir"];
    }
}