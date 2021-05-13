using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dazel.Compiler
{
    public static class SourceFileGetter
    {
        public static IEnumerable<string> GetFilesInDirectory(string directory)
        {
            return Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
        }
    }
}