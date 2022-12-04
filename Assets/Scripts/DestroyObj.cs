using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player")) {
            GameMaster.Instance.onLevelLose();
        }
        else if (collision.gameObject.CompareTag("Package"))
        {
            Destroy(collision.gameObject);
            GameMaster.Instance.onLevelLose();
        }
    }
}
