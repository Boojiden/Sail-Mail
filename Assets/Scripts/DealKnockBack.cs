using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealKnockBack : MonoBehaviour
{
    public float Force;
    [SerializeField] private float xModifier = 1;
    [SerializeField] private float yModifier = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Knockable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("active");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 diff = (Vector2)(collision.gameObject.transform.position - transform.position).normalized;
            diff.x *= xModifier;
            diff.y *= yModifier;
            diff *= Force;
            if(rb.velocity.y < -2f)
            {
                rb.velocity = new Vector2(rb.velocity.x + diff.x, diff.y);
            }
            else
            {
                rb.velocity += diff;
            }
        }
    }
}
