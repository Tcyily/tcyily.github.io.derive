using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCatcher : MonoBehaviour
{
    private int move_cmd_ = 0;
    private int rotate_cmd_ = 0;
    public Transform target = null;
    void Start()
    {
        
    }

    void Update()
    {
        KeyboardListener();
        MousListener();
        UnitMng.SetCmd2Unit(target.name, move_cmd_, rotate_cmd_);
        move_cmd_ = 0;
        rotate_cmd_ = 0;
    }

    //TODO:操作listener
    void KeyboardListener()
    {
        move_cmd_ += (Input.GetKey(KeyCode.W) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.UP
            + (Input.GetKey(KeyCode.S) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.DOWN
            + (Input.GetKey(KeyCode.D) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.RIGHT
            + (Input.GetKey(KeyCode.A) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.LEFT
            + (Input.GetKey(KeyCode.Space) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.JUMP
            + (Input.GetKey(KeyCode.LeftControl) ? 1 : 0) * (int)DefineConst.MOVE_DIR_CMD.SQUAT;
    }

    void MousListener()
    {

    }
}
