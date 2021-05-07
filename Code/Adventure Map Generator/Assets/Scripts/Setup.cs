using System.IO;
using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Setup : MonoBehaviour
    {
        private static readonly string[] WorkingDirectories = 
        {
            "src", "gfx"
        };
        
        private void Start()
        {
            SetupEnvironment();
        }

        private void SetupEnvironment()
        {
            foreach (string directory in WorkingDirectories)
            {
                CreateWorkingDirectory(directory);
            }
        }

        private static void CreateWorkingDirectory(string directoryName)
        {
            string path = Path.Combine(Application.persistentDataPath, directoryName);
            
            if (Directory.Exists(path)) return;

            Directory.CreateDirectory(path);
            Debug.Log($"Created directory: {path}");
        }
    }
}
