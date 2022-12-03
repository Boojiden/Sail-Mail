using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    private Text hpUI;
    private Text timeUI;

    private void Start()
    {
       hpUI = transform.GetChild(0).gameObject.GetComponent<Text>();
       timeUI = transform.GetChild(1).gameObject.GetComponent<Text>();
    }
    void Update()
    {
        if (PlayerHealth.instance != null)
        {
            hpUI.text = "Current Objectives: " + GameMaster.Instance.objs;
            timeUI.text = "" + GameMaster.clockConvert(GameMaster.currentTime);
        }
        
    }
}
