using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script exclusivo del Enemy para hacer daño al Player
public class DamagePlayer : MonoBehaviour
{
    /*
    [Tooltip("Tiempo que tarda el player en revivir")]
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;
    */

    private GameObject thePlayer;

    public int damage;
    public GameObject canvasDamage;

    //Stats del Player
    private CharacterStats playerStats;
    //Stats del Enemigo
    private CharacterStats _stats;

    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el enemigo colisiona con el Player
        if (collision.gameObject.tag.Equals("Player"))
        {
            //Factor de Fuerza del Enemigo
            float strFac = 1.0f + (float)_stats.strengthLevels[_stats.level] / CharacterStats.MAX_STAT_VAL;
            //Debug.Log("Fuerza del Enemigo " +strFac);
            //Factor de Defensa del Player
            float playerFac = 1.0f - (float)playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT_VAL;
            //Debug.Log("Defensa del Player " + playerFac);

            //Valor de ataque del enemigo vs. la defensa del Player
            int totalDamage = Mathf.Clamp( (int)((float)damage * strFac * playerFac) , 1, CharacterStats.MAX_HEALTH);
            //Debug.Log(totalDamage);

            //Probabilidad de falla el golpe tomando los stats del player
            if(Random.Range(0, CharacterStats.MAX_STAT_VAL) < playerStats.luckLevels[playerStats.level])
            {
                //Probabilidad de acertar golpe tomando los stats del enemy
                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < _stats.accuracyLevels[_stats.level])
                {
                    totalDamage = 0;
                }
                
            }

            if (totalDamage == 0)
            {
                var clone1 = (GameObject)Instantiate(canvasDamage, collision.transform.position, Quaternion.Euler(Vector3.zero));
                clone1.GetComponent<DamageNumber>().damagePoints = "MISS";
                return; //Si falló el enemigo, pues no tiene porque parpadear el Player!!!
            }
            else
            {
                //Muestra el valor con animacion
                var clone = (GameObject)Instantiate(canvasDamage, collision.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<DamageNumber>().damagePoints = "" + totalDamage;
            }


            //Resta vida al Player
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            /*
            collision.gameObject.SetActive(false);
            playerReviving = true;
            timeRevivalCounter = timeToRevivePlayer;
            thePlayer = collision.gameObject;
            */
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (playerReviving)
        {
            timeRevivalCounter -= Time.deltaTime;
            if(timeRevivalCounter < 0)
            {
                playerReviving = false;
                thePlayer.SetActive(true);
            }
        }
    }
    */
}
