using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playDestroy());
    }

    IEnumerator playDestroy()
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(gameObject);
    }
}
