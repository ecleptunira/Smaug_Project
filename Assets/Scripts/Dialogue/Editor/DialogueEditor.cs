using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        //public static bool OpneDialogue(int instanceID, int line)
        
            
        

        private void OnGUI()
        {
            
        }
    }
}
