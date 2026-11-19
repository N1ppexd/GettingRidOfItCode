using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace GettingRidOfIt
{
    public class ActionDataEditor : OdinMenuEditorWindow
    {
        [MenuItem("Niilo's Tools/Action Settings")]
        private static void OpenWindow()
        {
            EditorWindow.GetWindow<ActionDataEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            tree.AddAllAssetsAtPath("Body Interact Data", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Body", typeof(InteractData));
            tree.AddAllAssetsAtPath("Body Pieces Data", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Bodyparts", typeof(InteractData));
            tree.AddAllAssetsAtPath("Tool Data", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Items", typeof(InteractData));
            tree.AddAllAssetsAtPath("Blood Clean settings", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Blood", typeof(InteractData));
            tree.AddAllAssetsAtPath("Trash Bag settings", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/TrashBag", typeof(InteractData));

            tree.AddAllAssetsAtPath("Environment Settings/Kitchen", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Environment/Kitchen", typeof(InteractData));
            tree.AddAllAssetsAtPath("Environment Settings/General", "Assets/Scripts/Interaction/PropmtActions/ScriptableObjects/Environment/General", typeof(InteractData));

            return tree;
        }
    }
}
