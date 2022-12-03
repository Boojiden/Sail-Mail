using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public GameObject mailTo;
    public bool collected = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerPackage.instance.setPackage(gameObject);
        }
    }

    public void disableCollisions(float delay)
    {
        gameObject.layer = LayerMask.NameToLayer("Override");
        StartCoroutine(EnableCollision(delay));
    }

    private IEnumerator EnableCollision(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnDestroy()
    {
        if (!collected)
        {
            GameMaster.Instance.onLevelLose();
        }
    }
}
