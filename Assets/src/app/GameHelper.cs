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

}
