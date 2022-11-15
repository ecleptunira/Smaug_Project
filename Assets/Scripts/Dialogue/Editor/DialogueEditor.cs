using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow 
    {
        Dialogue selectedDialogue = null;
        [NonSerialized]
        GUIStyle nodeStyle;
        [NonSerialized]
        DialogueNode draggingNode = null;
        [NonSerialized]
        Vector2 draggingOffset;
        [NonSerialized]
        DialogueNode creatingNode = null;
        [NonSerialized]
        DialogueNode deletingNode = null;
        [NonSerialized]
        DialogueNode linkingParentNode = null;
        [NonSerialized]
        bool draggingCanvas = false;
        [NonSerialized]
        Vector2 draggingCanvasOffSet;
        const float canvasSize = 4000;
        const float backgroundSize = 50;

        Vector2 scrollPosition;

        public Dialogue SelectedDialogue { get => selectedDialogue; set => selectedDialogue = value; }

        [MenuItem("Window/Dialogue Editor")]
        private static void ShowEditorWindow()
        {
            var window = GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
            window.titleContent = new GUIContent("Dialogue");
            window.Show();
        }
        
        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line) 
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            nodeStyle.normal.textColor = Color.white;
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                SelectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (SelectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue");
            }
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords); 
                
                foreach (DialogueNode node in SelectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in SelectedDialogue.GetAllNodes())
                {
                    DrawNode(node); 
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {   
                    Undo.RecordObject(SelectedDialogue, "Added Dialogue");
                    SelectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    Undo.RecordObject(SelectedDialogue, "Deleted Dialogue");
                    SelectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }
        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffSet = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = SelectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(SelectedDialogue, "Move Dialogue Node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffSet - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }

        private void DrawNode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();
            string newText = EditorGUILayout.TextField(node.text);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(SelectedDialogue, "Update dialogue text");
                node.text = newText;
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("x"))
            {
                deletingNode = node;
            }

            DrawLinkButton(node);

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButton(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                    {
                        linkingParentNode = null;
                    }
            }
            else if (linkingParentNode.children.Contains(node.name))
            {
                if (GUILayout.Button("unlink"))
                {
                    Undo.RecordObject(SelectedDialogue, "remove Dialogue link");
                    linkingParentNode.children.Remove(node.name);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {
                    Undo.RecordObject(SelectedDialogue, "Add Dialogue link");
                    linkingParentNode.children.Add(node.name);
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startposition = new Vector2(node.rect.xMax, node.rect.center.y);
            foreach(DialogueNode childNode in SelectedDialogue.GetAllChildren(node))
            {
                Vector3 endposition = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
                Vector3 controlPointOffset = endposition - startposition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startposition, 
                    endposition, 
                    startposition + controlPointOffset, 
                    endposition - controlPointOffset, 
                    Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundValue = null;
            foreach (DialogueNode node in SelectedDialogue.GetAllNodes())
            {
                if (node.rect.Contains(point))
                {
                    foundValue = node;
                }
            }
            return foundValue;
        }
    }
}
