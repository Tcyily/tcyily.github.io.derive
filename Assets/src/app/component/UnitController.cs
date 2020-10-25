using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Vector3 speed;
    private UnitCMD __cmd;

    public UnitCMD cmd_ { get { return __cmd; } set { __cmd = value; } }
    [Tooltip("水平/垂直方向加速度")]
    public Vector2 acceleration;
    void Update()
    {
        if (cmd_ != null)
            Move();
    }

    void Move()
    {
        //TODO:移动逻辑 + 测试用例
        Vector3 move_dir = cmd_.GetMoveDir();
        speed = new Vector3(1, 1, 1);
        transform.position += new Vector3(move_dir.x * speed.x, 0, move_dir.z * speed.z);
        //Debug.Log(__cmd.GetMoveDir());
        //Vector3 move_offset = cmd.GetMoveOffset();
        //transform.LookAt(move_dir);
        __cmd = null;
    }
}
