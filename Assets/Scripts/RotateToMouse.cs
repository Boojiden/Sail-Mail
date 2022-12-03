using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = ((Vector2)transform.position - mousePos).normalized;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x)*Mathf.Rad2Deg - 90);
    }
}
