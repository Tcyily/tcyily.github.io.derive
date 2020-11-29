using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCamera : MonoBehaviour
{
    public float TEXTURE_HEIGHT_SIZE = 75;
    private Camera __camera_reflect_;
    private Camera __camera_refract_;
    private RenderTexture __camera_reflect_texture_; //= new RenderTexture(256, 256, 24);
    private RenderTexture __camera_refract_texture_; //= new RenderTexture(256, 256, 24);
    public Material water_effect_material_;
    void Start()
    {
        InitReflectCameraa();
    }

    private void InitReflectCameraa()
    {
        if (transform.gameObject.layer != (int)DefineConst.Layer_Name.Water)
        {
            Debug.LogError("WaterDataTransfer Should be added in 'Water Layer'");
            return;
        }
        if (__camera_reflect_ == null)
        {
            float aspect = Camera.main.aspect;
            __camera_reflect_texture_ = new RenderTexture((int)(aspect * TEXTURE_HEIGHT_SIZE), (int)TEXTURE_HEIGHT_SIZE, -10);
            __camera_reflect_ = new GameObject().AddComponent<Camera>();
            __camera_reflect_.CopyFrom(Camera.main);
            __camera_reflect_.depth = (int)DefineConst.CAMERA_DEPTH.WATER_EFFECT;
            __camera_reflect_.cullingMask = ~(1 << (int)DefineConst.Layer_Name.Water);
            __camera_reflect_.targetTexture = __camera_reflect_texture_;
            __camera_reflect_.transform.name = "reflect_camera_";

            __camera_refract_texture_ = new RenderTexture((int)(aspect * TEXTURE_HEIGHT_SIZE), (int)TEXTURE_HEIGHT_SIZE, -10);
            __camera_refract_ = new GameObject().AddComponent<Camera>();
            __camera_refract_.CopyFrom(Camera.main);
            __camera_refract_.depth = (int)DefineConst.CAMERA_DEPTH.WATER_EFFECT;
            __camera_refract_.cullingMask = ~(1 << (int)DefineConst.Layer_Name.Water);
            __camera_refract_.targetTexture = __camera_refract_texture_;
            __camera_refract_.transform.name = "refract_camera_";
        }
    }


    public void OnWillRenderObject()
    {
        /***************镜面反射***************/
        GL.invertCulling = true;
        //反射矩阵
        Matrix4x4 mir_matrix = GetMirrorMatrix();
        __camera_reflect_.worldToCameraMatrix = Camera.main.worldToCameraMatrix * mir_matrix;
        __camera_reflect_.transform.position = mir_matrix.MultiplyPoint(Camera.main.transform.position);
        __camera_reflect_.transform.forward = mir_matrix.MultiplyVector(Camera.main.transform.forward);
        //斜裁剪
        Vector4 clip_plane = GameHelper.GetPlaneInCameraSpace(__camera_reflect_, transform.position, transform.up);
        //__camera_reflect_.projectionMatrix = __camera_reflect_.CalculateObliqueMatrix(clip_plane);
        Matrix4x4 matrix4X4 = __camera_reflect_.projectionMatrix;
        __camera_reflect_.projectionMatrix = GameHelper.GetProjectionMatrixInPlane(matrix4X4, clip_plane);
        //渲染
        __camera_reflect_.Render();
        water_effect_material_.SetTexture("_ReflectTex", __camera_reflect_texture_);
        GL.invertCulling = false;

        /***************折射***************/
        __camera_refract_.transform.position = Camera.main.transform.position;
        __camera_refract_.transform.rotation = Camera.main.transform.rotation;
        //渲染
        __camera_refract_.Render();
        water_effect_material_.SetTexture("_RefractTex", __camera_refract_texture_);

        /***************debug贴图***************/
        if (DefineConst.VERSION == (int)DefineConst.VERSION_TYPE.DEBUG)
        {
            Debug.Log("调试模式-水面相机渲染图");
            GameHelper.SaveTexture(__camera_refract_texture_, DefineConst.DEBUG_TEXTURE_PATH, "Refract_Texture");
            GameHelper.SaveTexture(__camera_reflect_texture_, DefineConst.DEBUG_TEXTURE_PATH, "Reflect_Texture");
        }
    }

    /*************************辅助函数*****************************/
        //Link:https://www.cnblogs.com/wantnon/p/5630915.html
        private Matrix4x4 GetMirrorMatrix()
    {
        Vector3 normal = transform.up.normalized;
        float dis = Vector3.Dot(-normal, transform.position);//平面上所有的点都由过原点的平行面平移 n·p 而来， 故而 d 为 -n·p
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

}
