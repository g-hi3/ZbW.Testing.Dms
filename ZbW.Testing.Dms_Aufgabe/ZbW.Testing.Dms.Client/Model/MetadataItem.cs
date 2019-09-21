using System.Collections;
using System.Net.Mail;
using System.Text;

namespace ZbW.Testing.Dms.Client.Model
{
    internal class MetadataItem : ISaveableItem
    {
        // TODO: Write your Metadata properties here
        public MetadataItem(string guid, IDictionary map)
        {
            FileName = guid + "_Metadata.xml";
            FileContent = ConvertToXml(map);
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
    }
}