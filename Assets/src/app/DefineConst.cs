using UnityEngine;
public class DefineConst
{
    public static int VERSION = (int)VERSION_TYPE.DEV;

    /// <summary>
    /// Layer层级
    /// </summary>
    public enum Layer_Name
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
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
        WATER_EFFECT = -9,
        DEFAULT = -1,
        MAIN = 0,
    }

    public static string DEBUG_TEXTURE_PATH = "./Assets/debug/texture";

    public enum VERSION_TYPE
    {
        DEBUG = -1,
        DEV = 0,
    }

    /// <summary>
    /// 生成贴图的参数设置
    /// </summary>
    public const string noise_dir_ = "Assets/res/texture/noise";
    public static Vector2 noise_tex_size_ = new Vector2(256, 256);
    public const int noise_layer_cnt_ = 4;
}
