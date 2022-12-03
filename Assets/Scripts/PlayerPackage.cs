using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPackage : MonoBehaviour
{
    public float throwForce = 3f;
    [SerializeField] private GameObject current;
    public GameObject arrow;
    private GameObject arrowinstance;
    public static PlayerPackage instance;
    private bool hasPackage = false;
    void Start()
    {
        current = null;
        instance = this;
    }

    private void Update()
    {
        if (hasPackage)
        {
            if(arrowinstance == null)
            {
                arrowinstance = Instantiate(arrow, new Vector3(transform.position.x, transform.position.y + 3), Quaternion.identity);
            }
            else
            {
                var box = current.GetComponent<Package>().mailTo;
                Vector2 diff = ((Vector2)arrowinstance.transform.position - (Vector2)box.transform.position).normalized;
                arrowinstance.transform.position = new Vector3(transform.position.x, transform.position.y + 3);
                arrowinstance.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
            }
            if (Input.GetButtonDown("Throw"))
            {
                throwPackage();
            }
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(transform.position, (mousePos - (Vector2)transform.position).normalized, Color.red);
        }
        else if(arrowinstance != null)
        {
            Destroy(arrowinstance);
        }
    }

    public void setPackage(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().simulated = false;
        obj.transform.SetParent(transform, true);
        obj.transform.rotation = Quaternion.identity;
        obj.transform.position = new Vector3(transform.position.x, transform.position.y + 1);
        current = obj;
        hasPackage = true;
    }

    public void throwPackage()
    {
        current.transform.SetParent(null);
        var rb = current.GetComponent<Rigidbody2D>();
        current.GetComponent<Package>().disableCollisions(0.2f);
        rb.simulated = true;
        current.transform.position = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = (mousePos - (Vector2)transform.position).normalized;
        rb.velocity = diff * throwForce;
        current = null;
        hasPackage = false;
    }

    
}
