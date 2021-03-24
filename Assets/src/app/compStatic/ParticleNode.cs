using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleNode : MonoBehaviour
{
    public Transform container;
    public int max_distance_;

    void Start()
    {
        StartCoroutine(Timer());
    }

    //TODO：放回对象池
    IEnumerator Timer()
    {
        while (true)
        {
            if ((container.position - transform.position).magnitude > max_distance_)
            {
                Destroy(transform.gameObject);
            }
            yield return new WaitForSeconds(1.0f); // 停止执行1秒

        }
    }
}
