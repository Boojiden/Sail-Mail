using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public GameObject LevelPanel;
    public GameObject ClearPanel;
    public GameObject CreditsPanel;

    private void Start()
    {
        LevelPanel.SetActive(false);
        ClearPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }
    public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void clear()
    {
        LevelStat.clearData();
        SceneManager.LoadScene(0);
    }

    public void Leave()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); //does not work in the editor, it works when you compile
#endif
    }
}
