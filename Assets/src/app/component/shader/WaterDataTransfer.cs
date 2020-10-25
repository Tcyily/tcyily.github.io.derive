using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDataTransfer : MonoBehaviour
{
    private GameObject __camera_;
    void Start()
    {
        if (transform.gameObject.layer != (int)DefineConst.Layer_Name.Water)
        {
            Debug.LogError("WaterDataTransfer Should be added in 'Water Layer'");
            return;
        }

        if(__camera_ == null)
        {
            __camera_ = new GameObject();
            __camera_.AddComponent<Camera>();
            Camera camera = __camera_.GetComponent<Camera>();
            camera.CopyFrom(Camera.main);
            camera.cullingMask = ~(1 << (int)DefineConst.Layer_Name.Water);

            Matrix4x4 mir_matrix = GetMirrorMatrix();
            __camera_.transform.position = mir_matrix.MultiplyPoint(Camera.main.transform.position);
            __camera_.transform.forward = mir_matrix.MultiplyVector(Camera.main.transform.forward);
        }
    }

    private Matrix4x4 GetMirrorMatrix()
    {
        Vector3 normal = transform.up;
        float dis = Vector3.Dot(-normal,transform.position);
        Matrix4x4 martix = new Matrix4x4();

        martix.m00 = 1 - 2 * normal.x * normal.x;
        martix.m01 = -2 * normal.x * normal.y;
        martix.m02 = -2 * normal.x * normal.z;
        martix.m03 = -2 * dis * normal.x;

        martix.m10 = -2 * normal.x * normal.y;
        martix.m11 = 1 - 2 * normal.y * normal.y;
        martix.m12 = -2 * normal.y * normal.z;
        martix.m13 = -2 * dis * normal.y;

        martix.m20 = -2 * normal.x * normal.z;
        martix.m21 = -2 * normal.y * normal.z;
        martix.m22 = 1 - 2 * normal.z * normal.z;
        martix.m23 = -2 * dis * normal.z;

        martix.m30 = 0;
        martix.m31 = 0;
        martix.m32 = 0;
        martix.m33 = 1;

        return martix;
    }
    void Update()
    {
        
    }
}
