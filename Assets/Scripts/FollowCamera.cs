using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    public static GameObject instance;
    public GameObject follow;
    private GameObject playerinst;

    private void Start()
    {
        if (playerinst == null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            playerinst = Instantiate(follow, GameMaster.Instance.spawnPoint.position, Quaternion.identity);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    void LateUpdate()
    {
        if (playerinst != null)
        {
            Vector3 pos = new Vector3(playerinst.transform.position.x, playerinst.transform.position.y, -10);
            Rigidbody2D rb = playerinst.GetComponent<Rigidbody2D>();
            transform.position = Vector3.MoveTowards(transform.position, pos, 50f * Time.fixedDeltaTime);
        }
    }
}
