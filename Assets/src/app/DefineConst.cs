public class DefineConst
{
   public enum Layer_Name
    {
        Default = 0,
        TransparentFX = 1,
        Ignore_Raycast = 2,
        Water = 4,
        UI = 5,
    }
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
}
