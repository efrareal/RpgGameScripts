using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool statsForSkill;

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
        if (!statsForSkill)
        {
            healthManager.UpdateMaxHealth(hpLevels[level]);
        }

        //Actualizar en el arranque el Level en el UI HUD. Solamente al Player!
        if (gameObject.tag.Equals("Player"))
        {
            uIManager.LevelChanged(level, expToLevelUp.Length, expToLevelUp[level], expToLevelUp[level - 1]);
            mpManager.UpdateMaxMP(mpLevels[level]);
        }
        

        if (gameObject.tag.Equals("Enemy") && !statsForSkill)
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

            AddStatsAtLevelUP(level-1);

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

    public void AddStatsAtLevelUP(int prevLevel)
    {
        newstrengthLevels = newstrengthLevels + (strengthLevels[level] - strengthLevels[prevLevel]);
        newdefenseLevels = newdefenseLevels + (defenseLevels[level] - defenseLevels[prevLevel]);
        newmagicAttLevels = newmagicAttLevels + (magicAttLevels[level] - magicAttLevels[prevLevel]);
        newmagicDefLevels = newmagicDefLevels + (magicDefLevels[level] - magicDefLevels[prevLevel]);
        newspeedLevels = newspeedLevels + (speedLevels[level] - speedLevels[prevLevel]);
        newluckLevels = newluckLevels + (luckLevels[level] - luckLevels[prevLevel]);
        newaccuracyLevels = newaccuracyLevels + (accuracyLevels[level] - accuracyLevels[prevLevel]);
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

    public void InitialStats()
    {
        newstrengthLevels = 0; 
        newdefenseLevels = 0; 
        newmagicAttLevels = 0; 
        newmagicDefLevels = 0; 
        newspeedLevels = 0; 
        newluckLevels = 0;
        newaccuracyLevels = 0;

        AddStatsToCharacter(strengthLevels[1], defenseLevels[1], magicAttLevels[1], magicDefLevels[1], speedLevels[1], luckLevels[1], accuracyLevels[1]);

    }

}
