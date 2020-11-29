using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Numerics;
using System;

public static class EditorEx
{
    [MenuItem("EditorEx/FbmNoise")]
    public static void FbmNoise()
    {
        Debug.Log("Generating FBM Noise");
        int width = (int)DefineConst.noise_tex_size_.x;
        int height = (int)DefineConst.noise_tex_size_.y;
        Texture2D fbm_texture = new Texture2D(width, height);
        Texture2D[] noise_textures = new Texture2D[DefineConst.noise_layer_cnt_];
        for(int i = 0; i < DefineConst.noise_layer_cnt_; i++)
            noise_textures[i] = new Texture2D(width, height);

        int seed = System.DateTime.Now.Millisecond;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = Color.black;
                float color_val = 0.0f;
                float u = ((float)x) / ((float)width);
                float v = ((float)y) / ((float)height);
                for (int i = 0; i < DefineConst.noise_layer_cnt_; i++)
                {
                    float noise_color_val = (SimplexNoise.SeamlessNoise(u,v,
                                        10.0f, 10.0f, (float)seed) + 1) * .5f;
                    noise_textures[i].SetPixel(x, y, new Color(noise_color_val, noise_color_val, noise_color_val));
                    color_val += noise_color_val * Mathf.Pow(.5f, i + 1);
                    //取低频样本,偏移系数来自shadertoy:https://www.shadertoy.com/view/lsf3WH
                    float temp_u = u;
                    u = 1.6f * u - 1.2f * v;
                    v = 1.2f * temp_u + 1.6f * v;
                }
                color_val = .5f + color_val * .5f;
                color = new Color(color_val, color_val, color_val);
                fbm_texture.SetPixel(x, y, color);
            }
        }
        for (int i = 0; i < DefineConst.noise_layer_cnt_; i++)
            SaveTexture(noise_textures[i], DefineConst.noise_dir_, "Octave_" + Convert.ToString(i));
        SaveTexture(fbm_texture, DefineConst.noise_dir_, "FbmNoise");
        Debug.Log("Generated FBM Noise");
    }

    public static bool SaveTexture(Texture2D tex, string contents, string pngName)
    {

        byte[] bytes = tex.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(tex);
        tex = null;
        return true;
    }
}
