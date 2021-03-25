using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerConfig : MonoBehaviour
{

    private static readonly string[] ctrlName =
    {
        "PlayerController",
    }; 
    public static readonly HashSet<string> ctrlSet_ = new HashSet<string>(ctrlName);
}
