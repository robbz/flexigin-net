using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Flexigin.Core
{
    public class ComponentLoader
    {
        public ComponentResult GetComponent(string basePath, string path, bool includeSubDirectories = false)
        {
            var resolver = new UrlResolver();
            var component = resolver.Resolve(path);
            var contentType = this.GetContentType(component.FileType);
            var fullPath = Path.Combine(basePath, component.Path);

            if (!Directory.Exists(fullPath))
            {
                return new ComponentResult(HttpStatusCode.NotFound, contentType);
            }

            var contents = this.GetFileContents(fullPath, component.Extension, includeSubDirectories);
            var content = String.Join("", contents);

            return new ComponentResult(HttpStatusCode.OK, contentType, Minify(content, component.FileType));
        }

        private string Minify(string content, FileType fileType)
        {
            // TODO
            return content;
        }

        private IEnumerable<string> GetFileContents(string path, string fileType, bool traverse)
        {
            return Directory.GetFiles(path, "*." + fileType, 
                traverse 
                    ? SearchOption.AllDirectories 
                    : SearchOption.TopDirectoryOnly)
                .Select(ReadFile);
        }

        private string ReadFile(string file)
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(file);
            }
            catch (Exception)
            {
                throw;
            }
            return fileContent;
        }

        private string GetContentType(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Html:
                    return "text/html";
                case FileType.JavaScript:
                    return "application/x-javascript";
                case FileType.StyleSheet:
                    return "text/css";
                default:
                    throw new NotSupportedException("Filetype not supported.");
            }
        }
    }
}
