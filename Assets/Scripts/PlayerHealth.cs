using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameObject player;
    public static PlayerHealth instance;
    public int healthMax;
    public int healthCurrent;
    public float invuln;
    public bool canBeHit;

    private SpriteRenderer sr;

    void Start()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
        player = gameObject;
        healthCurrent = healthMax;


    }

    public void disableCollisions(float delay)
    {
        gameObject.tag = "Untagged";
        StartCoroutine(EnableCollision(delay));
    }

    private IEnumerator EnableCollision(float delay)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "Player";
    }
}
