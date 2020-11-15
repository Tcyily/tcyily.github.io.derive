using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDataTransfer : MonoBehaviour
{
    private const float TEXTURE_HEIGHT_SIZE = 100;
    private Camera __camera_;
    private RenderTexture __camera_texture_; //= new RenderTexture(256, 256, 24);
    public Material water_effect_material_;
    void Start()
    {
        InitMirrorCamera();
    }

    private void InitMirrorCamera()
    {
        if (transform.gameObject.layer != (int)DefineConst.Layer_Name.Water)
        {
            Debug.LogError("WaterDataTransfer Should be added in 'Water Layer'");
            return;
        }
        if (__camera_ == null)
        {
            float aspect = Camera.main.aspect;
            __camera_texture_ = new RenderTexture((int)(aspect * TEXTURE_HEIGHT_SIZE), (int)TEXTURE_HEIGHT_SIZE, -10);
            __camera_ = new GameObject().AddComponent<Camera>();
            __camera_.CopyFrom(Camera.main);
            __camera_.depth = (int)DefineConst.CAMERA_DEPTH.MIRROR;
            __camera_.cullingMask = ~(1 << (int)DefineConst.Layer_Name.Water);
            __camera_.targetTexture = __camera_texture_;
        }
        Camera.onPreCull += SetInvertCullingTrue;
        Camera.onPostRender += SetInvertCullingFalse;
    }

    private void OnDestroy()
    {
        Camera.onPreCull -= SetInvertCullingTrue;
        Camera.onPostRender -= SetInvertCullingFalse;
    }

    public void OnWillRenderObject()
    {
        //镜面反射
        Matrix4x4 mir_matrix = GetMirrorMatrix();
        __camera_.worldToCameraMatrix = Camera.main.worldToCameraMatrix * mir_matrix;
        __camera_.transform.position = mir_matrix.MultiplyPoint(Camera.main.transform.position);
        __camera_.transform.forward = mir_matrix.MultiplyVector(Camera.main.transform.forward);

        //斜裁剪
        Vector4 clip_plane = GetPlaneInCameraSpace(__camera_, transform.position, transform.up);
        //__camera_.projectionMatrix = __camera_.CalculateObliqueMatrix(clip_plane);
        Matrix4x4 matrix4X4 = __camera_.projectionMatrix;
        __camera_.projectionMatrix = GetProjectionMatrixInPlane(matrix4X4, clip_plane);

        //渲染
        __camera_.Render();
        water_effect_material_.mainTexture = __camera_texture_;
        water_effect_material_.SetTexture("__mirror_tex_", __camera_texture_);
        //debug贴图
        GameHelper.SaveTexture(__camera_texture_, DefineConst.DEBUG_TEXTURE_PATH, "Mirror_Texture");
    }

    /*************************辅助函数*****************************/
    //Link:https://www.cnblogs.com/wantnon/p/5630915.html
    private Matrix4x4 GetMirrorMatrix()
    {
        Vector3 normal = transform.up.normalized;
        float dis = Vector3.Dot(-normal,transform.position);//平面上所有的点都由过原点的平行面平移 n·p 而来， 故而 d 为 -n·p
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

    private Vector4 GetPlaneInCameraSpace(Camera camera, Vector3 pos, Vector3 normal)
    {
        Matrix4x4 w2c_matrix = camera.worldToCameraMatrix;
        Vector3 w_normal = w2c_matrix.MultiplyVector(normal);
        return new Vector4(w_normal.x, w_normal.y, w_normal.z, -Vector3.Dot(w2c_matrix.MultiplyPoint(pos), w2c_matrix.MultiplyVector(normal)));
    }

    //Link:http://terathon.com/lengyel/Lengyel-Oblique.pdf
    private Matrix4x4 GetProjectionMatrixInPlane(Matrix4x4 projection, Vector4 clip_plane)
    {
        Vector4 q = projection.inverse * new Vector4(
            sign(clip_plane.x),
            sign(clip_plane.y),
            1.0f,
            1.0f
        );
        Vector4 c = clip_plane * (2.0F / (Vector4.Dot(clip_plane, q)));

        projection[2] = c.x - projection[3];
        projection[6] = c.y - projection[7];
        projection[10] = c.z - projection[11];
        projection[14] = c.w - projection[15];
        return projection;
    }

    private int sign(float num)
    {
        if (num == 0f) return 0;
        if (num > 0f) return 1;
        else return - 1;
    }
    private void SetInvertCullingTrue(Camera cam)
    {
        GL.invertCulling = true;
    }
    private void SetInvertCullingFalse(Camera cam)
    {
        GL.invertCulling = false;
    }
}
