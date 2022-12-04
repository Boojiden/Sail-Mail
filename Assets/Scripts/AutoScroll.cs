using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float speed;
    private Material mat;
    private float currentOffset = 0f;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        currentOffset += speed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(currentOffset, 0);
    }
}
