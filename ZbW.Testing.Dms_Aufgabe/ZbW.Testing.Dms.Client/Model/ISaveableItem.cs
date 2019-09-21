namespace ZbW.Testing.Dms.Client.Model
{
    public interface ISaveableItem
    {
        
        string FileName { get; }
        object FileContent { get; }
        bool KeepOriginal { get; }
        string OriginalPath { get; }
        
    }
}