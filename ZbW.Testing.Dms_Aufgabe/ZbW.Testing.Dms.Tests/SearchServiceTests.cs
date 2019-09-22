using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Tests
{
    public class SearchServiceTests
    {
        private static readonly Configuration Config = new ConfigurationStub();

        [Test]
        public void FindMetadataItems_SearchTerm_Found()
        {
            // Arrange
            SetupMetadataItems();
            var searchService = new SearchService(Config);
            var searchTerm = "ok";
            var type = "Quittung";
            
            // Act
            var items = searchService.FindMetadataItems(searchTerm, type);

            // Assert
            Assert.IsTrue(items.Any(item => item.Map["Bezeichnung"].ToString().Contains("ok")));
            Assert.IsTrue(items.Any(item => item.Map["Typ"].ToString().Equals("Quittung")));
            Assert.IsTrue(items.Any(item => item.Map["Stichwörter"].ToString().Contains("ok")));
        }
        
        [Test]
        public void FindMetadataItems_Type_Found()
        {
            // Arrange
            
            // Act
            
            // Assert
            
        }
        
        [Test]
        public void FindMetadataItems_SearchTerm_NotFound()
        {
            // Arrange
            
            // Act
            
            // Assert
            
        }

        private void SetupMetadataItems()
        {
            var guidProvider = new GuidProvider();
            var saveService = new SaveService(Config);
            var firstMap = new Dictionary<string, object>()
            {
                {"Bezeichnung", "ok then"},
                {"Typ", "D4C"},
                {"Stichwörter", "kocchi wo miro"}
            };
            var firstItem = new MetadataItem(guidProvider.NextGuid, firstMap);
            saveService.SaveDocument(firstItem);
            var secondMap = new Dictionary<string, object>()
            {
                {"Bezeichnung", "ok then"},
                {"Typ", "Quittung"},
                {"Stichwörter", "some jojo meme"}
            };
            var secondItem = new MetadataItem(guidProvider.NextGuid, secondMap);
            saveService.SaveDocument(secondItem);
            var thirdMap = new Dictionary<string, object>()
            {
                {"Bezeichnung", "My deadly queen has already touched this code"},
                {"Typ", "Crazy Diamond"},
                {"Stichwörter", "ok I guess"}
            };
            var thirdItem = new MetadataItem(guidProvider.NextGuid, thirdMap);
            saveService.SaveDocument(thirdItem);
        }
        
    }
}