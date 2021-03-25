using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private static EventSystem instance = null;
    public Dictionary<string, string> appEventBind = new Dictionary<string, string>();
    public Dictionary<string, string> netEventBind = new Dictionary<string, string>();
    private EventSystem()
    {
        if(instance != null) {
            return;
        }
        instance = this;
    }

    public static EventSystem instance_{
        get {
            if(instance == null)
            {
                instance = new EventSystem();
            }
            return instance;
        }
    }

}
