using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public const int MAX_STAT_VAL = 100;
    public const int MAX_HEALTH = 999;

    public int level;
    public int exp;
    public int[] expToLevelUp; //Array de enteros

    [Tooltip("Niveles de vida del Jugador")]
    public int[] hpLevels;
    [Tooltip("Niveles de Magia del Jugador")]
    public int[] mpLevels;
    [Tooltip("Fuerza que se suma a la del arma")]
    public int[] strengthLevels;
    [Tooltip("Defensa que divide al daño del enemigo")]
    public int[] defenseLevels;
    [Tooltip("Fuerza que suma a la magia de ataque")]
    public int[] magicAttLevels;
    [Tooltip("Defensa que divide al daño magico del enemigo")]
    public int[] magicDefLevels;
    [Tooltip("Velocidad de ataque")]
    public int[] speedLevels;
    [Tooltip("Probabilidad de que el enemigo falle")]
    public int[] luckLevels;
    [Tooltip("Probabilidad de que el Player Haga daño critico")]
    public int[] accuracyLevels;

    private HealthManager healthManager;
    private PlayerController playerController;
    private WeaponManager weaponManager;


    // Start is called before the first frame update
    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        healthManager = GetComponent<HealthManager>();
        playerController = GetComponent<PlayerController>();
        AddStatsToCharacter(0,0,0,0,0,0,0);

        //Vida del Enemigo
        healthManager.UpdateMaxHealth(hpLevels[level]);
        if (gameObject.tag.Equals("Enemy"))
        {
            EnemyController controller = GetComponent<EnemyController>();
            controller.speed += (1.0f + (float)speedLevels[level] / MAX_STAT_VAL); 
        }
    }


    public void AddExperience(int exp)
    {
        //Añade experiencia!
        this.exp += exp;

        // Cuando llegue al ultimo Nivel no se rompa el juego
        if (level >= (expToLevelUp.Length -1))
        {
            return;
        }
        //Cuando exp se igual o mayor a la exp para subir al siguiente nivel
        if (this.exp >= expToLevelUp[level])
        {
            level++;
            healthManager.UpdateMaxHealth(hpLevels[level]);
            
            //Truco para refrescar Stats al subir de nivel
            weaponManager.ChangeWeapon(weaponManager.activeWeapon); 

            playerController.attackTime -= (float)speedLevels[level]/ MAX_STAT_VAL;
        }
    }


    public int newhpLevels;
    public int newmpLevels;
    public int newstrengthLevels;
    public int newdefenseLevels;
    public int newmagicAttLevels;
    public int newmagicDefLevels;
    public int newspeedLevels;
    public int newluckLevels;
    public int newaccuracyLevels;

    public void AddStatsToCharacter(int str, int def, int mat, int mdf, int spd, int lck, int acc)
    {
        newstrengthLevels = strengthLevels[level] + str;
        newdefenseLevels = defenseLevels[level] + def;
        newmagicAttLevels = magicAttLevels[level] + mat;
        newmagicDefLevels = magicDefLevels[level] + mdf;
        newspeedLevels = speedLevels[level] + spd;
        newluckLevels = luckLevels[level] + lck;
        newaccuracyLevels = accuracyLevels[level] + acc;
    }
}
