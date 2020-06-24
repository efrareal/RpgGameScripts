using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
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

    public bool flashActive;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMaxHealth(maxHealth);
        _characterRenderer = GetComponent<SpriteRenderer>();

    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            gameObject.SetActive(false);
        }
        if(flashLength > 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<PlayerController>().canMove = false;
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                             _characterRenderer.color.g,
                                             _characterRenderer.color.b,
                                             (visible ? 1.0f : 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
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
                ToggleColor(true);
                flashActive = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<PlayerController>().canMove = true;
            }
        }

    }
}

