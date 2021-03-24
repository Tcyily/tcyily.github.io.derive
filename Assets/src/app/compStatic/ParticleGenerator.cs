using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public int start_num_ = 1;
    private int cur_num_;
    private GameObject[] nodes_;
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
            bool r = Physics.Raycast(ray_, out hit_info_, 5000, 1<<(int)DefineConst.Layer_Name.FunnyBox);//transform.TransformDirection(Vector3.forward)
            if (r) //射线存在碰撞信息
            {
                //生成球球
                Vector3 p = hit_info_.point;
                for(int i = 0; i < cur_num_; i++)
                {
                    Vector3 rand = new Vector3(Random.RandomRange(i-1, i + 1) * prefab.transform.localScale.x - prefab.transform.localScale.x/2,
                        Random.RandomRange(i-1, i + 1) * prefab.transform.localScale.y - prefab.transform.localScale.y / 2,
                        Random.RandomRange(i-1, i + 1) * prefab.transform.localScale.z - prefab.transform.localScale.z / 2);
                    GameObject node = Instantiate(prefab, p+rand, Quaternion.Euler(Vector3.zero), container.transform);
                }
                cur_num_ += (int)speed_and_limit_.x;
                cur_num_ = Mathf.Min(cur_num_, (int)speed_and_limit_.y);
            }
        }
        cur_num_ = start_num_;
    }

    //对象池回收
    private void RecycleNode()
    {
        
    }
}
