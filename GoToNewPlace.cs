using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New place here!!!";
    public bool needsClick = false;
    public string uuid;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }


    private void Teleport(string objName)
    {
        if (objName == "Player")
        {
            if (!needsClick || (needsClick && Input.GetAxis("Fire1") > 0.2))
            {
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                //Debug.LogFormat("Se enviara al player al nextuuid = {0}", FindObjectOfType<PlayerController>().nextUuid);
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}