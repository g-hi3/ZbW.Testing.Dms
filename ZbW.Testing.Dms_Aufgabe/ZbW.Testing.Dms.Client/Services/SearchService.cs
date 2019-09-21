using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class SearchService
    {
        private readonly Configuration _configuration;

        public SearchService(Configuration config) => _configuration = config;

        public SearchService() : this(new Configuration()) {}

        public List<MetadataItem> FindMetadataItems(string searchTerm, string type)
        {
            // Find all meta data files.
            var basePath = _configuration.RepositoryDir;
            var metadataFiles = FindMetadataFilePaths(basePath, new List<FileInfo>());
            // for each: parse data
            var metadataItems = ConvertToMetadata(metadataFiles);
            // compare search term and type
            return FilterItems(metadataItems, searchTerm, type);
        }

        private List<FileInfo> FindMetadataFilePaths(string basePath, List<FileInfo> listSoFar)
        {
            var baseDir = new DirectoryInfo(basePath);
            listSoFar.AddRange(baseDir.GetFiles()
                .Where(file => file.Name.EndsWith("_Metadata.xml")));
            foreach (var dir in baseDir.GetDirectories())
                FindMetadataFilePaths(dir.FullName, listSoFar);
            return listSoFar;
        }

        private List<MetadataItem> ConvertToMetadata(List<FileInfo> files)
        {
            return files.Select(file => new MetadataItem(file)).ToList();
        }

        private List<MetadataItem> FilterItems(List<MetadataItem> items, string searchTerm, string type)
        {
            return items.Where(
                item => item.Map["Bezeichnung"].ToString().Contains(searchTerm)
                        || item.Map["Stichwörter"].ToString().Contains(searchTerm)
                        || item.Map["Typ"].ToString().Equals(type))
                .ToList();
        }

        public FileInfo FindDocumentFile(string guid)
        {
            var basePath = _configuration.RepositoryDir;
            var baseDir = new DirectoryInfo(basePath);
            return FindDocumentFile(baseDir, guid);
        }

        private FileInfo FindDocumentFile(DirectoryInfo parentDir, string guid)
        {
            foreach (var file in parentDir.GetFiles())
                if (file.Name.Contains(guid))
                    return file;
            foreach (var dir in parentDir.GetDirectories())
                FindDocumentFile(dir, guid);
            throw new IOException("File with guid " + guid + "not found!");
        }

    }
}