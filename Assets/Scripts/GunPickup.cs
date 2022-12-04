using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    void Start()
    {
        if(PlayerBuff.buffed == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerBuff.buffed = true;
            PlayerBuff.saveFile(PlayerBuff.buffed);
        }
    }
}
