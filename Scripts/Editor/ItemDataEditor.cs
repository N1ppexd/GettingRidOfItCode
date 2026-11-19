using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace GettingRidOfIt
{
    public class ItemDataEditor : OdinMenuEditorWindow
    {
        [MenuItem("Niilo's Tools/Item Settings")]
        private static void OpenWindow()
        {
            EditorWindow.GetWindow<ItemDataEditor>().Show();
        }

        private CreateNewItemData createNewItemData;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (createNewItemData != null)
                DestroyImmediate(createNewItemData.itemData);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            createNewItemData = new CreateNewItemData();
            tree.Add("Create New Item", createNewItemData);

            tree.AddAllAssetsAtPath("Tools", "Assets/Scripts/Items/Tools", typeof(ItemSO));
            tree.AddAllAssetsAtPath("Body", "Assets/Scripts/Items/Bodyparts", typeof(ItemSO));
            tree.AddAllAssetsAtPath("Body/Torso", "Assets/Scripts/Items/Bodyparts/Torso", typeof(ItemSO));
            tree.AddAllAssetsAtPath("Body/Legs", "Assets/Scripts/Items/Bodyparts/Legs", typeof(ItemSO));
            tree.AddAllAssetsAtPath("Body/Arms", "Assets/Scripts/Items/Bodyparts/Arms", typeof(ItemSO));

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection menuItem = this.MenuTree.Selection;


            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                if(SirenixEditorGUI.ToolbarButton("Delete Current"))
                {
                    ItemSO item = menuItem.SelectedValue as ItemSO;

                    string path = AssetDatabase.GetAssetPath(item);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }

    public class CreateNewItemData
    {
        public CreateNewItemData()
        {
            itemData = ScriptableObject.CreateInstance<ItemSO>();
            itemData.itemName = "New Item";
        }

        [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.Hidden)]
        public ItemSO itemData;


        [Button("Create New Item")]
        private void CreateNewItem()
        {
            AssetDatabase.CreateAsset(itemData, "Assets/Scripts/Items/Tools/" + itemData.itemName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
