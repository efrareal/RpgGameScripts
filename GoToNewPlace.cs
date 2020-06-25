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

    //Esto puede ser usado para abrir cofres
    private void OnTriggerStay2D(Collider2D collision)
    {
        Teleport(collision.gameObject.tag);
    }

    private void Teleport(string objName)
    {
        if (objName == "Player")
        {
            if (!needsClick || (needsClick && Input.GetAxis("Fire1") > 0.2))
            {
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}