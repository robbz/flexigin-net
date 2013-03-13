namespace Flexigin.Core
{
    public class ComponentUrl
    {
        public string Path { get; set; }
        public string Extension { get; set; }
        public FileType FileType { get; set; }

        public ComponentUrl(string path, string extension, FileType fileType)
        {
            this.Path = path;
            this.Extension = extension;
            this.FileType = fileType;
        }
    }
}