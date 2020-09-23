using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUI
{
    [RuntimeInitializeOnLoadMethod]
    private static void __OnSceneChange()
    {
        Debug.Log("The Scene Had Changed To：" + SceneManager.GetActiveScene().name);
        Debug.Log("is out ");
        __Init();
    }

    private static void __Init()
    {
        Debug.Log("is me ");
        foreach (dynamic value in GameApp.DataConf_["NavicatResource"])
        {
            //TODO:生成跳转ui
        }
    }
    //private void __
}
