using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public int start_num_ = 1;
    private int cur_num_;
    public Vector2 speed_and_limit_;

    private Ray ray_;
    private RaycastHit hit_info_;
    public GameObject container;
    public GameObject prefab;
    private void Start()
    {
        cur_num_ = start_num_;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool r = Physics.Raycast(ray_, out hit_info_, 1<<(int)DefineConst.Layer_Name.FunnyBox);//transform.TransformDirection(Vector3.forward)
            if (r) //射线存在碰撞信息
            {
                //生成球球
                Vector3 p = hit_info_.point;
                //Debug.Log(hit_info_.transform.name);
                for(int i = 0; i < cur_num_; i++)
                {
                    GameObject node = Instantiate(prefab, p, Quaternion.Euler(Vector3.zero), container.transform);
                }
                cur_num_ += (int)speed_and_limit_.x;
                cur_num_ = Mathf.Min(cur_num_, (int)speed_and_limit_.y);
            }

        }
        cur_num_ = start_num_;
    }
}
