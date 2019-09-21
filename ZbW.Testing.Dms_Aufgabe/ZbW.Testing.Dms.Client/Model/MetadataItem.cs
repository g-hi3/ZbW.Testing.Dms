using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem : ISaveableItem
    {
        // TODO: Write your Metadata properties here
        public MetadataItem(string guid, IDictionary map)
        {
            FileName = guid + "_Metadata.xml";
            FileContent = ConvertToXml(map);
        }

        public MetadataItem(FileInfo file)
        {
            FileContent = File.ReadAllText(file.FullName);
            var doc = new XmlDocument();
            doc.LoadXml(FileContent.ToString());
            var root = doc.DocumentElement;
            IDictionary map = new Dictionary<string, object>();
            for (var i = 0; i < root.ChildNodes.Count; i++)
            {
                var nextNode = root.ChildNodes.Item(i);
                map.Add(nextNode.Attributes["name"].Value, nextNode.Attributes["value"].Value);
            }

            FileName = file.Name;
            Map = map;
        }

        private string ConvertToXml(IDictionary map)
        {
            var xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                .Append("<Metadata>");
            foreach (var key in map.Keys)
            {
                xml.Append("<data name=\"")
                    .Append(key)
                    .Append("\" value=\"")
                    .Append(map[key])
                    .Append("\"/>");
            }
            xml.Append("</Metadata>");
            return xml.ToString();
        }
        
        public string FileName { get; }
        public object FileContent { get; }
        public bool KeepOriginal => false;
        public string OriginalPath => null;
        public IDictionary Map { get; }
    }
}