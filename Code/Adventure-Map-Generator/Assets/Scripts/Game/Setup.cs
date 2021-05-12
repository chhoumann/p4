using System.Diagnostics;
using System.IO;
using Dazel.Interpreter;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Dazel.Game
{
    public sealed class Setup : MonoBehaviour
    {
        private const string GameSceneName = "GameScene";
        
        public void ShowWorkingEnvironment()
        {
            Process.Start(GameManager.WorkingEnvironment);
        }

        public void Play()
        {
            if (Application.isEditor)
            {
                FindObjectOfType<GameManager>().Setup();
            }
            
            string path = GetDirectoryPath(GameManager.SourceFileDirectory);
            new DazelInterpreter(path).Run();

            SceneManager.LoadScene(GameSceneName);
        }

        private static void SetupEnvironment()
        {
            foreach (string directory in GameManager.WorkingDirectories)
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
            return Path.Combine(GameManager.WorkingEnvironment, directoryName);
        }
    }
}
