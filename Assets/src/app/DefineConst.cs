public class DefineConst
{
    /// <summary>
    /// Layer层级
    /// </summary>
   public enum Layer_Name
    {
        Default = 0,
        TransparentFX = 1,
        Ignore_Raycast = 2,
        Water = 4,
        UI = 5,
    }

    /// <summary>
    /// 角色控制器
    /// </summary>
    public enum MOVE_DIR_CMD
    {
        UP = 1,
        DOWN = 2,
        LEFT = 4,
        RIGHT = 8,
        JUMP = 16,
        SQUAT = 32,
    }

    public enum ROTATE_DIR_CMD
    {
        UP = 1,
        DOWN = 2,
        LEFT = 4,
        RIGHT = 8,
    }
    public const string UNIT_TAG = "UNIT";

    /// <summary>
    /// 相机渲染次序，最高为main
    /// </summary>
    public enum CAMERA_DEPTH
    {
        MIRROR = -9,
        DEFAULT = -1,
        MAIN = 0,
    }

    public static string DEBUG_TEXTURE_PATH = "./Assets/texture/debug";
}
