using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public List<string> tutorial = new List<string>();
    public GameObject Panel;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            string total = "";
            foreach(var tutorial in tutorial)
            {
                total += tutorial + "\n";
            }
            var obj = Instantiate(Panel);
            obj.GetComponent<TutorialUI>().text = total;
            obj.transform.SetParent(Camera.main.gameObject.transform.GetChild(0), false);
            triggered = true;
        }
    }
}
