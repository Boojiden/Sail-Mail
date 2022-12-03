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


    
    public void onHurt(int damage)
    {
        if (canBeHit)
        {
            StartCoroutine(hurt(damage));
        }
    }

    private IEnumerator hurt(int damage)
    {
        canBeHit = false;
        healthCurrent -= damage;
        if(healthCurrent <= 0)
        {
            GameMaster.Instance.onLevelLose();
        }
        yield return new WaitForSeconds(invuln);
        canBeHit = true;
    }

}
