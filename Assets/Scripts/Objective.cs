using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    //public GameObject effect;
    private bool collected = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collected == false)
        {
            //Instantiate(effect, transform.position, Quaternion.identity);
            GameMaster.Instance.tickObjs();
            collected = true;
        }
        else if (collision.gameObject.CompareTag("Package"))
        {
            var package = collision.gameObject.GetComponent<Package>();
            Debug.Log("1");
            if(package.mailTo == gameObject)
            {
                Debug.Log("2");
                GameMaster.Instance.tickObjs();
                package.collected = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
