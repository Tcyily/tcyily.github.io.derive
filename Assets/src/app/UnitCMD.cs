using UnityEngine;
public class UnitCMD
{
    private int __move_dir_cmd_ = 0;
    private int __rotate_dir_cmd_ = 0;

    /// <summary>
    /// 构造函数：下面通过MOVE_DIR_CMD/ROTATE_DIR_CMD位运算进行 格式化 操作指令
    /// </summary>
    /// <param name="move_cmd"></param>
    /// <param name="rotate_cmd"></param>
    public UnitCMD(int move_cmd, int rotate_cmd)
    {
        __move_dir_cmd_ = move_cmd;
        __rotate_dir_cmd_ = rotate_cmd;
    }
    /// 下面通过MOVE_DIR_CMD/ROTATE_DIR_CMD位运算进行 解析 指令    
    public Vector3 GetMoveDir()
    {
        Vector3 dir = Vector3.zero;
        dir.x = ((__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.RIGHT) > 0 ? 1 : 0) - ((__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.LEFT) > 0 ? 1 : 0) | 0;
        dir.z = ((__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.UP) > 0 ? 1 : 0) - ((__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.DOWN) > 0 ? 1 : 0) | 0;
        dir.y = (__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.JUMP) + (__move_dir_cmd_ & (int)DefineConst.MOVE_DIR_CMD.SQUAT);//TODO:如何表示跳和蹲
        return dir;
    }
}
