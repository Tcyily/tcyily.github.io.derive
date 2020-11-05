using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDataTransfer : MonoBehaviour
{
    private GameObject __camera_ob_;
    private RenderTexture __camera_texture_;
    public Material water_effect_material_;
    void Start()
    {
        if (transform.gameObject.layer != (int)DefineConst.Layer_Name.Water)
        {
            Debug.LogError("WaterDataTransfer Should be added in 'Water Layer'");
            return;
        }

        if(__camera_ob_ == null)
        {
            __camera_ob_ = new GameObject();
            __camera_ob_.AddComponent<Camera>();
            Camera camera = __camera_ob_.GetComponent<Camera>();

            camera.CopyFrom(Camera.main);
            camera.depth = (int)DefineConst.CAMERA_DEPTH.MIRROR;
            camera.cullingMask = ~(1 << (int)DefineConst.Layer_Name.Water);
            camera.targetTexture = __camera_texture_;

            //镜面反射
            Matrix4x4 mir_matrix = GetMirrorMatrix();
            camera.transform.position = mir_matrix.MultiplyPoint(Camera.main.transform.position);
            camera.transform.forward = mir_matrix.MultiplyVector(Camera.main.transform.forward);
            camera.worldToCameraMatrix = Camera.main.worldToCameraMatrix * mir_matrix;
            GL.invertCulling = true;
            //斜裁剪
            Vector4 clip_plane = GetPlaneInCameraSpace(camera, transform.position, transform.up);
            Matrix4x4 matrix4X4 = camera.projectionMatrix;
            camera.projectionMatrix = GetProjectionMatrixInPlane(matrix4X4, clip_plane);
            //设置渲染目标
            camera.Render();
            water_effect_material_.mainTexture = __camera_texture_;
            //Break Point:__camera_texture_ is NULL
            Debug.Log(__camera_texture_ == null);
            var flag = GameHelper.SaveTexture(__camera_texture_, DefineConst.DEBUG_TEXTURE_PATH, "Mirror_Texture");
            Debug.Log(flag ? "success" : "fail");
        }
    }

    //Link:https://www.cnblogs.com/wantnon/p/5630915.html
    private Matrix4x4 GetMirrorMatrix()
    {
        Vector3 normal = transform.up;
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
        float dis = -Vector3.Dot(pos, normal);
        Vector3 w_normal = w2c_matrix.MultiplyVector(normal);
        return new Vector4(w_normal.x, w_normal.y, w_normal.z, dis);
    }

    //Link:http://terathon.com/lengyel/Lengyel-Oblique.pdf
    private Matrix4x4 GetProjectionMatrixInPlane(Matrix4x4 projection, Vector4 clip_plane)
    {
        Vector4 q = projection.inverse * new Vector4(
            Mathf.Sign(clip_plane.x),
            Mathf.Sign(clip_plane.y),
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
}
