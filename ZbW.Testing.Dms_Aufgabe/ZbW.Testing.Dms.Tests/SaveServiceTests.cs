using System;
using System.IO;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Tests
{
    public class SaveServiceTests
    {
        private const string RepositoryPath = @"C:\Temp\DMS";
        private const string OriginalFileName = "original-file.txt";
        private static readonly Configuration Config = new ConfigurationStub();
        
        [SetUp]
        public void CreateAllFolders()
        {
            if (!Directory.Exists(RepositoryPath))
                Directory.CreateDirectory(RepositoryPath);
        }
        
        [Test]
        public void SaveDocument_KeepOriginal_OriginalExists() {
            // Arrange
            var filePath = OriginalFileName;
            DeleteFile(filePath);
            CreateFile(filePath);
            var service = new SaveService(Config);
            
            // Act
            service.SaveDocument(filePath, true);
            
            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }
        
        [Test]
        public void SaveDocument_RemoveOriginal_OriginalDoesntExists() {
            // Arrange
            var filePath = OriginalFileName;
            DeleteFile(filePath);
            CreateFile(filePath);
            var service = new SaveService(Config);
            
            // Act
            service.SaveDocument(filePath, false);
            
            // Assert
            Assert.IsFalse(File.Exists(filePath));
        }

        [TearDown]
        public void RemoveAllFiles()
        {
            File.Delete(OriginalFileName);
            File.Delete(RepositoryPath + '\\' + OriginalFileName);
        }

        private void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;
            
            File.Delete(filePath);
        }

        private void CreateFile(string filePath)
        {
            if (File.Exists(filePath)) 
                return;
            
            var file = File.Create(filePath);
            var random = new Random();
            var buffer = new byte[255];

            random.NextBytes(buffer);
            
            file.Write(buffer);
            file.Close();
        }
        
    }
}