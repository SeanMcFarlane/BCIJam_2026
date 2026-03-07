using UnityEditor;
using UnityEngine;

public class ConvertJunkyardMaterials
{
    [MenuItem("Tools/Convert Junkyard Materials to URP")]
    static void Convert()
    {
        string folder = "Assets/Junkyard models";
        Shader urpLit = Shader.Find("Universal Render Pipeline/Lit");

        if (urpLit == null)
        {
            Debug.LogError("Could not find URP/Lit shader. Make sure URP is installed.");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { folder });
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null || mat.shader == urpLit)
                continue;

            Texture mainTex = mat.HasProperty("_MainTex") ? mat.GetTexture("_MainTex") : null;
            Color color = mat.HasProperty("_Color") ? mat.GetColor("_Color") : Color.white;

            mat.shader = urpLit;

            if (mainTex != null)
                mat.SetTexture("_BaseMap", mainTex);

            mat.SetColor("_BaseColor", color);

            EditorUtility.SetDirty(mat);
            count++;
            Debug.Log($"Converted: {path}");
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Done. Converted {count} material(s).");
    }
}
