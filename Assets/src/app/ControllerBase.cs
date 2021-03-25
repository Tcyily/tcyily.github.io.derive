using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase
{
    private static ControllerBase instance = new ControllerBase();
    private static ControllerBase instance_
    {
        get { return instance; }
    }
    private ControllerBase()
    {

    }

}
