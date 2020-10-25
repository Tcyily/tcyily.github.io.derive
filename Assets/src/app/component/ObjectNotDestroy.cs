using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(transform.name + "don't be destroy");
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
