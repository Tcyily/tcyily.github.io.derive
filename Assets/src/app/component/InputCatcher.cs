using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCatcher : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        int move_cmd = 0;
        move_cmd += (Input.GetKey(KeyCode.W) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.UP
            + (Input.GetKey(KeyCode.S) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.DOWN
            + (Input.GetKey(KeyCode.D) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.RIGHT
            + (Input.GetKey(KeyCode.A) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.LEFT
            + (Input.GetKey(KeyCode.Space) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.JUMP
            + (Input.GetKey(KeyCode.LeftControl) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.SQUAT;
        int rotate_cmd = 0;
        UnitMng.SetCmd2Unit("player", move_cmd, rotate_cmd);
    }
}
