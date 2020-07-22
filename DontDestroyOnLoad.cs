using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerController.playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            //Debug.LogFormat("Se destruyo objeto {0}", this.gameObject.name);
            Destroy(gameObject);
        }
        
        
    }

}
