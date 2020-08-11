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
    private ArmorManager armorManager;
    private UIManager uIManager;
    private MPManager mpManager;


    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        armorManager = FindObjectOfType<ArmorManager>();
        healthManager = GetComponent<HealthManager>();
        playerController = GetComponent<PlayerController>();
        mpManager = GetComponent<MPManager>();
        AddStatsToCharacter(strengthLevels[level],defenseLevels[level],magicAttLevels[level],magicDefLevels[level],speedLevels[level],luckLevels[level],accuracyLevels[level]);

        //Vida del Enemigo y Player
        healthManager.UpdateMaxHealth(hpLevels[level]);

        //Actualizar en el arranque el Level en el UI HUD. Solamente al Player!
        if (gameObject.tag.Equals("Player"))
        {
            uIManager.LevelChanged(level, expToLevelUp.Length, expToLevelUp[level], expToLevelUp[level - 1]);
            mpManager.UpdateMaxMP(mpLevels[level]);
        }
        

        if (gameObject.tag.Equals("Enemy"))
        {
            EnemyController controller = GetComponent<EnemyController>();
            controller.speed += (1.0f + (float)newspeedLevels / MAX_STAT_VAL); 
        }
    }


    public void AddExperience(int exp)
    {
        //Añade experiencia!
        this.exp += exp;
        uIManager.ExpChanged(this.exp);

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
            mpManager.UpdateMaxMP(mpLevels[level]);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.LEVEL_UP);

            //Actualiza el HUD
            uIManager.LevelChanged(level, expToLevelUp.Length, expToLevelUp[level], expToLevelUp[level - 1]);

            AddStatsAtLevelUP();

            //Suma velocidad al Player
            playerController.speed += (float)newspeedLevels/ MAX_STAT_VAL;
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

    private void AddStatsAtLevelUP()
    {
        newstrengthLevels = newstrengthLevels + (strengthLevels[level] - strengthLevels[level-1]);
        newdefenseLevels = newdefenseLevels + (defenseLevels[level] - defenseLevels[level-1]);
        newmagicAttLevels = newmagicAttLevels + (magicAttLevels[level] - magicAttLevels[level-1]);
        newmagicDefLevels = newmagicDefLevels + (magicDefLevels[level] - magicDefLevels[level-1]);
        newspeedLevels = newspeedLevels + (speedLevels[level] - speedLevels[level-1]);
        newluckLevels = newluckLevels + (luckLevels[level] - luckLevels[level-1]);
        newaccuracyLevels = newaccuracyLevels + (accuracyLevels[level] - accuracyLevels[level-1]);
    }

    public void AddStatsToCharacter(int str, int def, int mat, int mdf, int spd, int lck, int acc)
    {
        newstrengthLevels = newstrengthLevels + str;
        newdefenseLevels = newdefenseLevels + def;
        newmagicAttLevels = newmagicAttLevels + mat;
        newmagicDefLevels = newmagicDefLevels + mdf;
        newspeedLevels = newspeedLevels + spd;
        newluckLevels = newluckLevels + lck;
        newaccuracyLevels = newaccuracyLevels + acc;
    }

    public void RemoveStatsToCharacter(int str, int def, int mat, int mdf, int spd, int lck, int acc)
    {
        newstrengthLevels = newstrengthLevels - str;
        newdefenseLevels = newdefenseLevels - def;
        newmagicAttLevels = newmagicAttLevels - mat;
        newmagicDefLevels = newmagicDefLevels - mdf;
        newspeedLevels = newspeedLevels - spd;
        newluckLevels = newluckLevels - lck;
        newaccuracyLevels = newaccuracyLevels - acc;
    }

}
