using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab_ = null;
    public HashSet<GameObject> playerSet_ = new HashSet<GameObject>();//TODO:HashSet<Actor>
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddPlayer()
    {
        GameObject player = Instantiate(playerPrefab_);
        player.transform.parent = transform;
        playerSet_.Add(player);
    }
}
