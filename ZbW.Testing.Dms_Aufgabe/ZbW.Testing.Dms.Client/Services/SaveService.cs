using System.IO;
using System.Security.Permissions;
using ZbW.Testing.Dms.Client.Model;

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

        public void SaveDocument(ISaveableItem item)
        {
            CreateDirectory();
            var fullPath = _configuration.RepositoryDir + PathSeparator + item.FileName;
            var content = item.FileContent;

            if (content is string s)
                File.WriteAllText(fullPath, s);
            
            if (content is byte[] b)
                File.WriteAllBytes(fullPath, b);
            
            if (!item.KeepOriginal && item.OriginalPath != null)
                File.Delete(item.OriginalPath);
        }

        private string GetFileNameFromPath(string filePath)
        {
            var pathParts = filePath.Split(PathSeparator);
            return pathParts[pathParts.Length - 1];
        }

        public void CreateDirectory(string folderPath = "")
        {
            var fullPath = _configuration.RepositoryDir + PathSeparator + folderPath;
            
            if (Directory.Exists(fullPath))
                return;

            var pathParts = fullPath.Split(PathSeparator);
            var currentPath = "";
            foreach (var part in pathParts)
            {
                currentPath += part + PathSeparator;

                if (!Directory.Exists(currentPath))
                    Directory.CreateDirectory(currentPath);
            }
        }
    }
}