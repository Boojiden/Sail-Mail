using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoBoost : MonoBehaviour
{
    public GameObject boost;
    public GameObject effect;
    public GameObject sound;
    public float offset;
    public float fireRate;
    public float range;
    public float boostChargeBonus;
    public float chargeTick;
    public LayerMask hits;

    private bool didFire = false;
    [SerializeField] private float currentCharge = 0f;
    private GameObject effectInstance;
    private Vector3 init = new Vector3(1, 1, 1);

    private Vector3 mousePos;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!didFire && !PlayerBuff.buffed && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(doShot());
        }
        else if(!didFire && PlayerBuff.buffed && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(doChargeShot());
        }
    }
    private IEnumerator doShot()
    {
        didFire = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, range, hits);
        if (hit)
        {
            Instantiate(boost, hit.point, Quaternion.identity);
            Instantiate(sound);
        }
        yield return new WaitForSeconds(1/fireRate);
        didFire = false;
    }

    private IEnumerator doChargeShot()
    {
        didFire = true;
        Vector2 angle = (mousePos - transform.position).normalized;
        while (Input.GetButton("Fire1"))
        {
            angle = (mousePos - transform.position).normalized;
            if (currentCharge < 1f)
            {
                currentCharge += chargeTick * Time.deltaTime;
            }
            if(effectInstance == null)
            {
               effectInstance = Instantiate(effect, angle*offset, Quaternion.identity);
            }
            else
            {
                effectInstance.transform.localScale = init * currentCharge;
                effectInstance.transform.position = transform.position + (Vector3)(angle * offset);
            }
            yield return null;
        }
         RaycastHit2D hit = Physics2D.Raycast(transform.position, angle, range, hits);
         if (hit)
         {
             Instantiate(boost, hit.point, Quaternion.identity).GetComponent<DealKnockBack>().Force += currentCharge*boostChargeBonus;
             Instantiate(sound);
        }
         Destroy(effectInstance);
         currentCharge = 0f;
         yield return new WaitForSeconds(1 / fireRate);
         didFire = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 end = (mousePos - transform.position);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + end.normalized * range);
    }
}
