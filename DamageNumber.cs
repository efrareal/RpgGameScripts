using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    public float damageSpeed;
    public string damagePoints;

    public Text damageText;
    public TMP_Text damageTextPro;

    // Update is called once per frame
    void Update()
    {
        if(damageText != null)
        {
            damageText.text = damagePoints;
        }
        if(damageTextPro != null)
        {
            damageTextPro.text = "" + damagePoints;
        }
        
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + damageSpeed * Time.deltaTime,
                                                this.transform.position.z);
        this.transform.localScale = this.transform.localScale * (1 - Time.deltaTime / 1.75f);
     
        
    }
}
