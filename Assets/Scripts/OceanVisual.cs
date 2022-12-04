using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanVisual : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth.player != null)
        {
            transform.position = new Vector3(PlayerHealth.player.transform.position.x, transform.position.y, 92);
        }
        
    }
}
