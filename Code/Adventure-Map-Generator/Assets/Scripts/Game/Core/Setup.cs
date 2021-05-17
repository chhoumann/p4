using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Dazel.Compiler;
using Dazel.IntermediateModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Dazel.Game.Core
{
    public sealed class Setup : MonoBehaviour
    {
        private const string GameSceneName = "GameScene";

        private void Start()
        {
            SetupEnvironment();
        }

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

            if (new DazelCompiler(path).TryRun(out IEnumerable<ScreenModel> screenModels))
            {
                World.ScreenModels = screenModels;
                
                if (Application.isPlaying)
                {
                    SceneManager.LoadScene(GameSceneName);
                }
            }
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
