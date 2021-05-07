using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

        public void ShowWorkingEnvironment()
        {
            Process.Start(Application.persistentDataPath);
        }

        public void Play()
        {
            new 
        }

        private static void SetupEnvironment()
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
