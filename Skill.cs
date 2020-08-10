using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string skillName;
    public string skillType;
    private CharacterStats stats;

    public bool inInventory;
    public bool skillActive;
    public int skillDamage;

    public GameObject bloodAnim;
    private GameObject hitPoint;
    public GameObject canvasDamage;

    public GameObject hpPoints;
    public GameObject moneyDrop;


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


            //Accuracy(Player) vs. Luck(Enemy) 
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < stats.newaccuracyLevels)
            {
                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.newluckLevels)
                {
                    totalDamage = 0;
                }
                else
                {
                    totalDamage *= (Random.Range(2, 4)); //HAce daño critico entre el doble y triple

                    //Animacion del System Particle
                    if (bloodAnim != null && hitPoint != null)
                    {
                        Destroy(Instantiate(bloodAnim, hitPoint.transform.position, hitPoint.transform.rotation), 1.0f);
                    }

                    //Animacion del letrero de puntos
                    var clone2 = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
                    clone2.GetComponent<DamageNumber>().damagePoints = "CRIT " + totalDamage;

                    //Suelta HP Points si hay daño critico
                    Destroy(Instantiate(hpPoints, hitPoint.transform.position, hpPoints.transform.rotation), 7.0f);
                }
            }
            else
            {
                //Ataque normal probabilidad de que dañe al enemigo
                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.newluckLevels)
                {
                    totalDamage = 0;
                }
                else
                {


                    //Animacion del System Particle
                    if (bloodAnim != null && hitPoint != null)
                    {
                        Destroy(Instantiate(bloodAnim, hitPoint.transform.position, hitPoint.transform.rotation), 1.0f);
                    }

                    //Animacion del letrero de puntos
                    var clone3 = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
                    clone3.GetComponent<DamageNumber>().damagePoints = "" + totalDamage;
                }
            }

            if (totalDamage == 0)
            {
                var clone1 = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
                clone1.GetComponent<DamageNumber>().damagePoints = "MISS";
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MISS_ATTACK);
            }

            //Suelta Money si hay tienes suerte
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.newluckLevels)
            {
                Destroy(Instantiate(moneyDrop, hitPoint.transform.position, hpPoints.transform.rotation), 7.0f);
            }

            //Reduce daño a vida
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            collision.gameObject.GetComponent<EnemyController>().EnemyWasHit();
        }
    }
}
