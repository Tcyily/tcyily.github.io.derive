using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gzParent_ = null;
    public GameObject gzPrefab_ = null;

    private int[,] mapData_ = new int[,]
    {
        {0,0,1,1,0},
        {1,0,1,1,0},
        {1,0,0,1,0},
        {0,0,0,1,0},
        {0,1,1,1,0}
    };
    //位置和地图各自的映射
    public Dictionary<int, GameObject> gzGzMap_ = new Dictionary<int, GameObject>();

    void Start()
    {
        StartCoroutine(MapCreateing());
    }

    IEnumerator MapCreateing()
    {
        int vertLen = mapData_.GetLength(0);
        int horiLen = mapData_.GetLength(1);
        float gzWidth = this.gzPrefab_.transform.localScale.x;
        float gzHeight = this.gzPrefab_.transform.localScale.z;
        float centerX = horiLen / 2;
        float centerY = horiLen / 2;
        for(int i = 0; i < vertLen; i++)
        {
            for(int j = 0; j < horiLen ; j++)
            {
                Debug.Log(mapData_[i, j]);
                int idx = i * horiLen + j;
                float offsetX = (j - centerX) * gzWidth;
                float offsetY = (i - centerY) * gzHeight;
                Vector3 pos = new Vector3(offsetX, 0, offsetY);
                GameObject gz = Instantiate(gzPrefab_, pos, Quaternion.Euler(Vector3.zero), gzParent_.transform);
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
