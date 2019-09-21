using System.IO;

namespace ZbW.Testing.Dms.Client.Model
{
    public class DocumentItem : ISaveableItem
    {

        public DocumentItem(string guid, string filePath, bool keepOriginal = true)
        {
            var fileExtension = ExtractExtension(filePath);
            FileName = guid + "_Content." + fileExtension;
            FileContent = File.ReadAllBytes(filePath);
            KeepOriginal = keepOriginal;
        }

        private string ExtractExtension(string filePath)
        {
            var splitByDot = filePath.Split('.');
            return splitByDot[splitByDot.Length - 1];
        }
        
        public string FileName { get; }
        public object FileContent { get; }
        public bool KeepOriginal { get; }
        public string OriginalPath { get; }
    }
}