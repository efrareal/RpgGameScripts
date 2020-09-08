using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Skill : MonoBehaviour
{
    public string skillName;
    public string skillType;
    private CharacterStats stats;
    public int skillMP;

    public bool inInventory;
    public bool skillActive;
    public int skillDamage;

    public GameObject bloodAnim;
    private GameObject hitPoint;
    public GameObject canvasDamage;

    public GameObject hpPoints;
    public GameObject moneyDrop;

    private float waitCounter= 0.0f;
    private float waitTime = 0.6f;
    private bool isTimeWait;


    //public Sprite skillSprite;

    // Start is called before the first frame update
    void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
        stats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();
            float enemyFactor = 1.0f - (float)enemyStats.newmagicDefLevels / CharacterStats.MAX_STAT_VAL;

            float playerFactor = 1.0f + (float)stats.newmagicAttLevels / CharacterStats.MAX_STAT_VAL;

            int totalDamage = (int)((float)skillDamage * playerFactor * enemyFactor);

            //Animacion del System Particle
            if (bloodAnim != null && hitPoint != null)
            {
                Destroy(Instantiate(bloodAnim, collision.transform.position, collision.transform.rotation), 1.0f);
            }
            
            //Animacion del letrero de puntos
            var clone3 = (GameObject)Instantiate(canvasDamage, collision.transform.position, Quaternion.Euler(Vector3.zero));
            clone3.GetComponent<DamageNumber>().damagePoints = "" + totalDamage;

            //Suelta Money si tienes suerte
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.newluckLevels)
            {
                Destroy(Instantiate(moneyDrop, collision.transform.position, collision.transform.rotation), 7.0f);
                //Suelta HP points
                Destroy(Instantiate(hpPoints, collision.transform.position, collision.transform.rotation), 7.0f);
            }
            //Suelta HP points si tienes suerte
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.newluckLevels)
            {
                //Suelta HP points
                Destroy(Instantiate(hpPoints, collision.transform.position, collision.transform.rotation), 7.0f);
            }

            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ENEMY_HIT);

            //Reduce daño a vida
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            collision.gameObject.GetComponent<EnemyController>().EnemyWasHit();
        }
    }

    private void Update()
    {
        if (isTimeWait)
        {
            waitCounter += Time.deltaTime;
            if(waitCounter > waitTime)
            {
                isTimeWait = false;
            }
        }
    }

}
