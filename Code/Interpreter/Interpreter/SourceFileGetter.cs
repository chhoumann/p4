using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Interpreter
{
    internal static class SourceFileGetter
    {
        public static IEnumerable<string> GetFilesInDirectory(string directory)
        {
            return Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
        }
    }
}