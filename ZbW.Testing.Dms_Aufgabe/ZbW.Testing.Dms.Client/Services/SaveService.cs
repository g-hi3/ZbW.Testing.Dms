using System.IO;

namespace ZbW.Testing.Dms.Client.Services
{
    public class SaveService
    {
        private const string RepositoryBasePath = @"C:\Temp";
        private const char PathSeparator = '\\';
        
        public void SaveDocument(string sourceFilePath, bool keepOriginal)
        {
            var fileName = GetFileNameFromPath(sourceFilePath);
            var destinationFilePath = RepositoryBasePath + PathSeparator + fileName;
            
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
        
    }
}