using UnityEditor;
using UnityEngine;

namespace Example.Editor
{
    /// <summary>
    /// 要想修改Asset，要查看对应文件，然后根据字段名称修改，然后设置Dirty，最后保存
    /// </summary>
    public static class ModifyMaterial
    {
        private static readonly int RenderTypePropertyId = Shader.PropertyToID("_Surface");
    
        [MenuItem("Tools/ModifyMaterial")]
        public static void Modify()
        {
            var materials = Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
            if (materials == null || materials.Length == 0)
            {
                Debug.LogError("必须选中材质");
                return;
            }

            foreach (var obj in materials)
            {
                var material = obj as Material;
                if (material == null)
                {
                    continue;
                }
            
                material.SetOverrideTag("RenderType", "Transparent");
                material.renderQueue = 3000;
                material.SetInt(RenderTypePropertyId, 1);
            
                EditorUtility.SetDirty(material);
            }
        
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
