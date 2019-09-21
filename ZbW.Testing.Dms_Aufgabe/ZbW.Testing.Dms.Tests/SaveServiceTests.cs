using System;
using System.IO;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Tests
{
    public class SaveServiceTests
    {
        
        [Test]
        public void SaveDocument_KeepOriginal_OriginalExists() {
            // Arrange
            var filePath = "original-file.txt";
            DeleteFile(filePath);
            CreateFile(filePath);
            var service = new SaveService();
            
            // Act
            service.SaveDocument(filePath, true);
            
            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }
        
        [Test]
        public void SaveDocument_RemoveOriginal_OriginalDoesntExists() {
            // Arrange
            var filePath = "original-file.txt";
            DeleteFile(filePath);
            CreateFile(filePath);
            var service = new SaveService();
            
            // Act
            service.SaveDocument(filePath, false);
            
            // Assert
            Assert.IsFalse(File.Exists(filePath));
        }

        [TearDown]
        public void RemoveAllFiles()
        {
            File.Delete("original-file.txt");
            File.Delete(@"C:\Temp\original-file.txt");
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