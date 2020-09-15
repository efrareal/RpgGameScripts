using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    public GameObject target;
    public GameObject throwObject;

    public float throwSpeed = 10.0f;

    public Vector2 directionToThrow;

    private bool isThrowing;

    public float timeToThrow = 2.0f;
    public float timeToThrowCounter;

    public float timeBetweenThrows = 2.0f;
    public float timeBetweenThrowsCounter;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
        timeToThrowCounter = timeToThrowCounter * Random.Range(0.5f, 1.5f);
        timeBetweenThrowsCounter = timeBetweenThrows * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {


        if (isThrowing)
        {
            timeToThrowCounter -= Time.deltaTime;
            if (timeToThrowCounter < 0)
            {
                ThrowSomething();
                isThrowing = false;
                timeToThrowCounter = timeToThrow;
            }
        }
        else
        {
            timeBetweenThrowsCounter -= Time.deltaTime;
            if(timeBetweenThrowsCounter < 0)
            {
                isThrowing = true;
            }
            
        }

    }

    void ThrowSomething()
    {
        timeToThrowCounter = timeToThrow;
        directionToThrow = (target.transform.position - this.transform.position).normalized;
        GameObject newThrow = Instantiate(throwObject, this.transform.position, this.transform.rotation) as GameObject;
        Rigidbody2D newThrowRB = newThrow.GetComponent<Rigidbody2D>();
        Skill spellSkill = newThrow.GetComponent<Skill>();

       newThrowRB.velocity = directionToThrow * throwSpeed;
       SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE_SKILL);
       if (directionToThrow.x > 0) //Mirando hacia la derecha
       {
           newThrow.transform.rotation = Quaternion.Euler(0, 0, 90);
       }
       if (directionToThrow.x < 0) //Mirando hacia la Izquierda
       {
           newThrow.transform.rotation = Quaternion.Euler(0, 0, 270);
       }
       if (directionToThrow.y > 0) //Mirando hacia arriba
       {
           newThrow.transform.rotation = Quaternion.Euler(0, 0, 180);
       }
       if (directionToThrow.y < 0) //Mirando hacia la abajo
       {
           newThrow.transform.rotation = Quaternion.Euler(0, 0, 0);
       }
    }
}
