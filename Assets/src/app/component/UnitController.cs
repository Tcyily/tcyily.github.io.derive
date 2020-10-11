using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Vector3 speed;
    private UnitCMD __cmd;
    
    public UnitCMD cmd { get { return __cmd; } set { __cmd = value; } }
    [Tooltip("水平/垂直方向加速度")]
    public Vector2 acceleration;
    void Update()
    {
        if(cmd != null)
            Move();
    }

    void Move()
    {
        //Vector3 move_dir = cmd.();
        //Vector3 move_offset = cmd.GetMoveOffset();
        //transform.LookAt(move_dir);
        __cmd = null;
    }
}
