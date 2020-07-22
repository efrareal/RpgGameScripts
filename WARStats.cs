using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WARStats : MonoBehaviour
{
    public int strength;
    [Tooltip("Defensa que divide al daño del enemigo")]
    public int defense;
    [Tooltip("Fuerza que suma a la magia de ataque")]
    public int magicAtt;
    [Tooltip("Defensa que divide al daño magico del enemigo")]
    public int magicDef;
    [Tooltip("Velocidad de ataque")]
    public int speed;
    [Tooltip("Probabilidad de que el enemigo falle")]
    public int luck;
    [Tooltip("Probabilidad de que el Player Haga daño critico")]
    public int accuracy;

    private CharacterStats thePlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStats = FindObjectOfType<CharacterStats>().GetComponent<CharacterStats>();
        //thePlayerStats.AddStatsToCharacter(strength, defense, magicAtt, magicDef, speed, luck, accuracy);
    }


     


}
