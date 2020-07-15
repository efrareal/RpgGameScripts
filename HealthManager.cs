using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Vida maxima en el nivel de exp")]
    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int Health
    {
        get
        {
            return currentHealth;
        }
    }

    [Tooltip("Determina si el character hara flashing al ser dañado")]
    public bool flashActive;
    [Tooltip("Duración del Flashing, si el gameObject tiene valor positivo hara flash!")]
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;

    public int expWhenDefeated;
    private QuestEnemy quest;
    private QuestManager questManager;


    // Start is called before the first frame update
    void Start()
    {
        //Apenas arranca y asigna el maxHealth a la vida al "nacer"
        UpdateMaxHealth(maxHealth);
        _characterRenderer = GetComponent<SpriteRenderer>();

        //QuestEnemy
        quest = GetComponent<QuestEnemy>();
        questManager = FindObjectOfType<QuestManager>();

    }

    /// <summary>
    /// Se asigna valor de dañod con variable "damage"
    /// </summary>
    /// <param name="damage"></param>
    public void DamageCharacter(int damage)
    {
        //Quita vida al Player o enemigo
        currentHealth -= damage;

        //Validar vida del Player o Enemy
        if(currentHealth < 0)
        {
            //Asigna experiencia al "Player" cuando el enemigo muere
            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.FindWithTag("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
                questManager.enemyKilled = quest;
            }
            gameObject.SetActive(false);
        }
        //Si la duracion del Flash es positiva, entonces aplica el flash
        if(flashLength > 0)
        {
            GetComponent<CircleCollider2D>().enabled = false; //Desactiva el Coliider para que no reciba daño
            GetComponent<PlayerController>().canMove = false; //El jugador no podra moverse (Revisar PlayerController)
            flashActive = true; // Activa el Flash
            flashCounter = flashLength; //Asigna el contador de Flashing
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Metodo que sirve para el comportamiento del Flashing
    /// </summary>
    /// <param name="visible"></param>
    void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                             _characterRenderer.color.g,
                                             _characterRenderer.color.b,
                                             (visible ? 1.0f : 0.0f)); // Si Visible es "true", Transparencia 100%, si es "False" 0%
    }

    public void AddHealthPoints(int value)
    {
        if((currentHealth + value) >= maxHealth){
            currentHealth = maxHealth;
            return;
        }
        currentHealth += value;
    }

    // Update is called once per frame
    void Update()
    {
        //Logica del Flashing
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter > flashLength * 0.83f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > flashLength * 0.5f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > flashLength * 0.166f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > 0)
            {
                ToggleColor(true);
            }
            else
            {
                //Normaliza la conficion del Character!!!
                ToggleColor(true);
                flashActive = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<PlayerController>().canMove = true;
            }
        }

    }
}

