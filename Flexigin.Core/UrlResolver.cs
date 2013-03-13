using System;
using System.Linq;

namespace Flexigin.Core
{
    public class UrlResolver
    {
        public ComponentUrl Resolve(string path)
        {
            path = path.ToLower();
            if (path.StartsWith("../") || path.StartsWith("./")) { throw new ArgumentException("Invalid path, must be absolute."); }
            if (path.StartsWith("/")) { path = path.Substring(1, path.Length - 1); }
            if (path.Contains("?")) { path = path.Split('?')[0]; }
            if (path.EndsWith("/")) { path = path.Substring(0, path.Length - 1); }

            var urlParts = path.Split('/');
            var fileType = this.GetFileType(urlParts.Last());
            var extension = this.GetExtension(fileType);
            var componentPath = path.Substring(0, path.LastIndexOf('/') + 1);

            return new ComponentUrl(componentPath, extension, fileType);
        }

        private string GetExtension(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Html:
                    return "html";
                case FileType.JavaScript:
                    return "js";
                case FileType.StyleSheet:
                    return "css";
                default:
                    throw new NotSupportedException("Filetype not supported.");
            }
        }

        private FileType GetFileType(string extension)
        {
            switch (extension)
            {
                case "js":
                    return FileType.JavaScript;
                case "css":
                    return FileType.StyleSheet;
                case "html":
                    return FileType.Html;
                default:
                    return FileType.Unknown;
            }
        }
    }
}
