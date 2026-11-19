using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GettingRidOfIt
{
    public class HolderData : OdinMenuEditorWindow
    {
        [MenuItem("Niilo's Tools/Holder Settings")]
        private static void OpenWindow()
        {
            EditorWindow.GetWindow<HolderData>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            tree.AddAllAssetsAtPath("Holders", "Assets/Scripts/Holders", typeof(HolderSO));

            return tree;
        }
    }
}
