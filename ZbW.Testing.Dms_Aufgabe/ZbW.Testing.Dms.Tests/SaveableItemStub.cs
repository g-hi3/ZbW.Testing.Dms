using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Tests
{
    public class SaveableItemStub : ISaveableItem
    {
        public string FileName => "Saveable.txt";
        public object FileContent => "hello world";
        public bool KeepOriginal => false;
        public string OriginalPath => null;
    }
}