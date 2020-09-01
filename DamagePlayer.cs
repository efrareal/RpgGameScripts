using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script exclusivo del Enemy para hacer daño al Player
public class DamagePlayer : MonoBehaviour
{
    private GameObject thePlayer;

    public int damage;
    public GameObject canvasDamage;

    //Stats del Player
    private CharacterStats playerStats;
    //Stats del Enemigo
    private CharacterStats _stats;
    private EnemyController enemyController;

    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
        enemyController = GetComponent<EnemyController>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el enemigo colisiona con el Player
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().dashSkill)
            {
                return;
            }
            //Factor de Fuerza del Enemigo
            float strFac = 1.0f + (float)_stats.strengthLevels[_stats.level] / CharacterStats.MAX_STAT_VAL;
            //Debug.Log("Fuerza del Enemigo " +strFac);
            //Factor de Defensa del Player
            float playerFac = 1.0f - (float)playerStats.newdefenseLevels / CharacterStats.MAX_STAT_VAL;
            //Debug.Log("Defensa del Player " + playerFac);

            //Valor de ataque del enemigo vs. la defensa del Player
            int totalDamage = Mathf.Clamp( (int)((float)damage * strFac * playerFac) , 1, CharacterStats.MAX_HEALTH);
            //Debug.Log(totalDamage);

            //Probabilidad de falla el golpe tomando los stats del player
            if(Random.Range(0, CharacterStats.MAX_STAT_VAL) < playerStats.newluckLevels)
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
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MISS_ATTACK);
                return; //Si falló el enemigo, pues no tiene porque parpadear el Player!!!
            }
            else
            {
                //Muestra el valor con animacion
                var clone = (GameObject)Instantiate(canvasDamage, collision.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<DamageNumber>().damagePoints = "" + totalDamage;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ATTACK1);
            }


            //Resta vida al Player
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            enemyController.HitThePlayer();

        }
    }
}
