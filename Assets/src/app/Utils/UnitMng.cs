using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitMng
{
    public static void SetCmd2Unit(string name, int move_cmd, int rotate_cmd)
    {
        GameObject unit = GameHelper.GetObjectByName(name);
        UnitController unit_controller = unit.GetComponent<UnitController>();
        if (unit_controller == null) Debug.LogError(name + "no Component UnitController");
        unit_controller.cmd_ = new UnitCMD(move_cmd, rotate_cmd);
    }
}
