using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject effect;
    public GameObject sound;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            var package = collision.gameObject.GetComponent<Package>();
            Debug.Log("1");
            if(package.mailTo == gameObject)
            {
                Debug.Log("2");
                GameMaster.Instance.tickObjs();
                package.collected = true;
                Destroy(collision.gameObject);
                Instantiate(sound);
                Instantiate(effect, transform.position, Quaternion.identity);
            }
        }
    }
}
