using System.IO;
using Dazel.Game.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dazel.Game.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public const int PixelsPerUnit = 16;

        private const string MenuSceneName = "MainMenu";
        
        public const string SourceFileDirectory = "src";
        public const string GraphicsFileDirectory = "gfx";
        
        public static readonly string[] WorkingDirectories = 
        {
            SourceFileDirectory, "gfx"
        };
        
        public static string WorkingEnvironment { get; private set; }
        
        public static GameManager Instance { get; private set; }

        public GfxLoader GfxLoader { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            if (SceneManager.GetActiveScene().name != MenuSceneName)
            {
                SceneManager.LoadScene(MenuSceneName);
                return;
            }

            Setup();
            DontDestroyOnLoad(gameObject);
        }

        public void Setup()
        {
            Instance = this;
            WorkingEnvironment = Application.persistentDataPath;
            GfxLoader = new GfxLoader(Path.Combine(WorkingEnvironment, GraphicsFileDirectory));
        }

        public static void ReturnToMainMenu()
        {
            SceneManager.LoadScene(MenuSceneName);
        }
    }
}
