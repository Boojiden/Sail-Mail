using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoBoost : MonoBehaviour
{
    public GameObject boost;
    public float fireRate;
    public float range;
    public LayerMask hits;

    private bool didFire = false;

    private Vector3 mousePos;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!didFire && Input.GetButton("Fire1"))
        {
            StartCoroutine(doShot());
        }
    }
    private IEnumerator doShot()
    {
        didFire = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, range, hits);
        if (hit)
        {
            Instantiate(boost, hit.point, Quaternion.identity);
        }
        yield return new WaitForSeconds(1/fireRate);
        didFire = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 end = (mousePos - transform.position);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + end.normalized * range);
    }
}
