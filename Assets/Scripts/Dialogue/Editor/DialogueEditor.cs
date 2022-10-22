using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow {

        [MenuItem("Window/Dialogue Editor")]
        private static void ShowEditorWindow()
        {
            var window = GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
            window.titleContent = new GUIContent("Dialogue");
            window.Show();
        }

        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line) => false;

        private void OnGUI()
        {
            
        }
    }
}
