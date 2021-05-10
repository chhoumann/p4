using System.IO;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class GameManager : MonoBehaviour
    {
        public const int PixelsPerUnit = 16;

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

            Instance = this;
            WorkingEnvironment = Application.persistentDataPath;
            GfxLoader = new GfxLoader(Path.Combine(WorkingEnvironment, GraphicsFileDirectory));
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
