using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject ob = GameApp.name_2_object_[name];
        if (ob == null)
        {
            ob = GameObject.Find(name);
            if (ob == null) Debug.LogError("Error:" + name + "is NOT EXIST"); 
            GameApp.name_2_object_[name] = ob;
        }
        return ob;
    }
}
