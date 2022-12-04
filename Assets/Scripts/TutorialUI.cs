using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TutorialUI : MonoBehaviour
{
    // Start is called before the first frame update

    public string text;
    public float lifetime = 5;
    private Image image;
    private float currentAlpha = 1;
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = text;
        transform.localPosition = new Vector3(0, -180);
        StartCoroutine(timerAndFade());
        image = GetComponent<Image>();
    }

    private IEnumerator timerAndFade()
    {
        yield return new WaitForSeconds(lifetime);
        while(!(currentAlpha < 0))
        {
            currentAlpha -= 0.01f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
