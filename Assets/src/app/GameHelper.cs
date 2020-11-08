using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public static class GameHelper
{
    /// <summary>
    /// <param name="parent"></param>
    /// <param name="pos"></param>
    /// <param name="res_path"</param>
    /// <param name="text"></param>
    /// <returns>button GameObject</returns>
    /// </summary>
    public static GameObject CreateButton(GameObject parent, Vector3 pos = default(Vector3), string res_path = null, string text = null)
    {
        DefaultControls.Resources ui_res = new DefaultControls.Resources();
        GameObject uiButton = DefaultControls.CreateButton(ui_res);
        uiButton.transform.SetParent(parent.transform, false);
        uiButton.transform.position = pos;
        return uiButton;
    }

    public static GameObject GetObjectByName(string name)
    {
        GameObject ob = null;
        GameApp.name_2_object_.TryGetValue(name, out ob);
        if (ob == null)
        {
            ob = GameObject.Find(name);
            if (ob == null) Debug.LogError("Error:" + name + "is NOT EXIST"); 
            GameApp.name_2_object_[name] = ob;
        }
        return ob;
    }

    public static bool SaveTexture(Texture2D tex, string path, string pngName)
    {

        byte[] bytes = tex.EncodeToPNG();
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        FileStream file = File.Open(path + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(tex);
        tex = null;
        //Debug.Log(path + "/" + pngName + ".png " + "save success");
        return true;
    }

    public static bool SaveTexture(Texture tex, string path, string pngName)
    {
        return SaveTexture(TextureToTexture2D(tex), path, pngName);
    }
    public static bool SaveTexture(RenderTexture tex, string path, string pngName)
    {
        return SaveTexture(RenderTextureToTexture2D(tex), path, pngName);
    }

    /// <summary>
    /// 运行模式下Texture转换成Texture2D
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public static Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }

    /// <summary>
    /// 编辑器模式下Texture转换成Texture2D
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public static Texture2D TextureToTexture2DInEditorMode(Texture texture)
    {
        Texture2D texture2d = texture as Texture2D;
        UnityEditor.TextureImporter ti = (UnityEditor.TextureImporter)UnityEditor.TextureImporter.GetAtPath(UnityEditor.AssetDatabase.GetAssetPath(texture2d));
        //图片Read/Write Enable的开关
        ti.isReadable = true;
        UnityEditor.AssetDatabase.ImportAsset(UnityEditor.AssetDatabase.GetAssetPath(texture2d));
        return texture2d;
    }
    /// <summary>
    /// RenderTexture转Texture2D
    /// </summary>
    /// <param name="rTex"></param>
    /// <returns></returns>
    public static Texture2D RenderTextureToTexture2D(RenderTexture rt)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}