using UnityEngine;
using System.Collections;
using UnityEditor;//注意要引用
public class TerrainManager : EditorWindow
{
    private static Vector2 WINDOW_MAX_SIZE = new Vector2(400, 600);
    [MenuItem("Window/Utils/TerrainManager")]
    static void CreateMyWindow()
    {
        TerrainManager window = EditorWindow.GetWindow<TerrainManager>();
        window.maxSize = WINDOW_MAX_SIZE;
        window.minSize = WINDOW_MAX_SIZE;
        window.Show();
    }
    Texture2D terrain_map_ = new Texture2D(256, 512);
    void OnGUI()
    {
        EditorGUILayout.ObjectField("地形俯视图", terrain_map_, terrain_map_.GetType());
        
    }
}
