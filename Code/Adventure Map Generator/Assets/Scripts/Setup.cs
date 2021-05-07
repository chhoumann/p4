using System.Diagnostics;
using System.IO;
using Interpreter;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace P4.MapGenerator
{
    public sealed class Setup : MonoBehaviour
    {
        private const string SourceFileDirectory = "src";
        
        private static readonly string[] WorkingDirectories = 
        {
            SourceFileDirectory, "gfx"
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
            string path = GetDirectoryPath(SourceFileDirectory);
            new DazelInterpreter(path).Run();
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
            string path = GetDirectoryPath(directoryName);
            
            if (Directory.Exists(path)) return;

            Directory.CreateDirectory(path);
            Debug.Log($"Created directory: {path}");
        }

        private static string GetDirectoryPath(string directoryName)
        {
            return Path.Combine(Application.persistentDataPath, directoryName);
        }
    }
}
