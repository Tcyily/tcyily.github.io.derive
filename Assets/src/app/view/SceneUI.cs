using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneUI : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void __OnSceneChange()
    {
        __Init();
    }

    private static GameObject canvas;
    private static void __Init()
    {
        canvas = GameObject.Find("/Canvas") ?? new GameObject("Canvas");
        canvas.layer = (int)DefineConst.Layer_Name.UI;
        DontDestroyOnLoad(canvas);
        bool cmd;
        cmd = canvas.GetComponent<Canvas>() ?? canvas.AddComponent<Canvas>();
        cmd = canvas.GetComponent<CanvasScaler>() ?? canvas.AddComponent<CanvasScaler>();
        cmd = canvas.GetComponent<GraphicRaycaster>() ?? canvas.AddComponent<GraphicRaycaster>();
        foreach (dynamic pairs in GameApp.DataConf_["NavicatResource"])
        {
            string scene_name = pairs.Value.sceneName;
            GameHelper.CreateButton(canvas);
        }
    }
    //private void __
}
