using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public Transform target_;
    public float move_speed_ = 1.0f;
    public float rotate_speed_ = 1.0f;

    private float __mouse_hori_;
    private float __mouse_vert_;
    private float __scroll_;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.GetMartix4x4RotateByVec(new Vector3(0, 1, 0), 1));
    }
    
    // Update is called once per frame
    void Update()
    {
        __mouse_hori_ = Input.GetAxis("Mouse X");
        __mouse_vert_ = Input.GetAxis("Mouse Y");
        __scroll_ = Input.GetAxis("Mouse ScrollWheel");
        //transform.position += transform.forward * __scroll_ * move_speed_;
        //transform.position = this.GetMartix4x4RotateByVec(new Vector3(0, 1, 0), 1) * transform.position;
        //Debug.Log(transform.position);
        if (Input.GetMouseButton(0))
        {
            //Vector2 around_dir = new Vector2(__mouse_hori_, __mouse_hori_);
            Vector3 link_vec_ns = transform.InverseTransformVector(transform.position - target_.position);
            Vector3 target_pos_ns = transform.InverseTransformVector(target_.position);
            //Vector4 panel_ns = new Vector4(link_vec_ns.x, link_vec_ns.y, link_vec_ns.z, Vector3.Dot(link_vec_ns, target_pos_ns));
            Vector3 axis_temp = target_pos_ns - new Vector3(__mouse_hori_, __mouse_vert_, 0);
            link_vec_ns.Normalize();
            axis_temp.Normalize();  
            Vector3 axis_around_ns = Vector3.Cross(link_vec_ns, axis_temp);
            //Debug.Log(transform.position);
            Vector3 axis_around_ws = transform.TransformVector(axis_around_ns);
            transform.position = GetMartix4x4RotateByVec(axis_around_ws, 5).MultiplyPoint(transform.position);
        }
        transform.LookAt(target_);
        //Debug.Log((transform.position - target_.position).magnitude);
    }

    Matrix4x4 GetMartix4x4RotateByVec(Vector3 axis, float angle)
    {
        float x = axis.x, y = axis.y, z = axis.z;
        Matrix4x4 matrix = new Matrix4x4();
        matrix.m00 = x + (1 - x * x) * Mathf.Cos(angle);
        matrix.m01 = x * y - z * Mathf.Sin(angle);
        matrix.m02 = x * z * (1 - Mathf.Cos(angle)) + y * Mathf.Sin(angle);
        matrix.m03 = 0;

        matrix.m10 = x * y * (1 - Mathf.Cos(angle)) + z * Mathf.Sin(angle);
        matrix.m11 = y * y + (1 - y * y) * Mathf.Cos(angle);
        matrix.m12 = y * z * (1 - Mathf.Cos(angle)) - x * Mathf.Sin(angle);
        matrix.m13 = 0;

        matrix.m20 = x * z * (1 - Mathf.Cos(angle)) - y * Mathf.Sin(angle);
        matrix.m21 = y * z * (1 - Mathf.Cos(angle)) + x * Mathf.Sin(angle);
        matrix.m22 = z * z - (1 - z * z) * Mathf.Cos(angle);
        matrix.m23 = 0;

        matrix.m30 = 0;
        matrix.m31 = 0;
        matrix.m32 = 0;
        matrix.m33 = 1;
        return matrix;
    }
}
