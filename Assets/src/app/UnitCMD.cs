using UnityEngine;
public class UnitCMD
{
    private int __move_dir = 0;
    private float __move_dis = 0f;
    private int __rotate_dir = 0;
    private float __rotate_dis = 0f;

    public int move_dir_ { get { return __move_dir; } set { __move_dir = value >= 0 ? value : 0; } }
    public float move_dist_ { get { return __move_dis; } set { __move_dis = value >= 0 ? value : 0; } }

    public float rotate_dist_ { get { return __rotate_dis; } set { __rotate_dis = value >= 0 ? value : 0; } }
    public int rotate_dir_ { get { return __rotate_dir; } set { __rotate_dir = value >= 0 ? value : 0; } }

    private Vector3 GetMoveDir()
    {
        Vector3 dir = Vector3.zero;
        return dir;
    }

    private Vector3 GetMoveOffset()
    {
        Vector3 offset = Vector3.zero;
        return offset;
    }
}
