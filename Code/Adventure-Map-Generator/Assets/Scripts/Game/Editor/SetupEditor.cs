using UnityEditor;
using UnityEngine;

namespace Dazel.Game.Editor
{
    [CustomEditor(typeof(Setup))]
    public sealed class SetupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Setup setup = (Setup)target;

            if (GUILayout.Button("Run compiler"))
            {
                setup.Play();
            }
        }
    }
}
