using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;

    public float speed = 5.0f;
    private const string AXIS_H = "Horizontal", 
                         AXIS_V = "Vertical",
                         WALK = "Walking", 
                         ATT = "Attacking",
                         LAST_H = "LastH", 
                         LAST_V = "LastV",
                         SPELLCAST = "SpellCast",
                         DEAD = "Dead";


    public Vector2 lastMovement = Vector2.zero;
    private bool walking = false;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public string nextUuid;

    private bool attacking = false;
    public float attackTime;
    private float attackTimeCounter;

    //private bool bowing = false;
    //public float bowTime;
    //private float bowTimeCounter;
    //public GameObject arrow;
    //public Transform shootPosition;
    //public float arrowSpeed;
    //private WeaponManager weapon;

    public bool canMove = false;
    public bool isTalking = true;
    public bool isDead;
    
    public bool castSpell;
    public float spellTime;
    private float spellTimeCounter;
    public GameObject fireSpell;
    public GameObject iceSpell;
    public GameObject thunderSpell;
    public Transform shootPosition;
    public float shootSpeed;
    private WeaponManager weapon;

    public bool dashSkill;
    public GameObject dashObject;

    public bool inTransition;

    // Start is called before the first frame update
    void Start()
    {
        weapon = FindObjectOfType<WeaponManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //isTalking = false;
        if (!playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
            playerCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            _animator.SetBool(DEAD, true);
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        if (inTransition)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, false);
            _animator.SetBool(WALK, false);
            return;
        }


        if (isTalking)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, false);
            _animator.SetBool(WALK, false);
            return;
        }

        this.walking = false;

        if (!canMove)
        {
            return;
        }

        if (dashSkill)
        {
            spellTimeCounter -= Time.deltaTime;
            if (spellTimeCounter < 0)
            {
                dashSkill = false;
                attacking = false;
                _animator.SetBool(ATT, false);
                dashObject.SetActive(false);
            }
            return;
        }

        if (castSpell)
        {
            weapon.DeactivateWeapon(false);
            spellTimeCounter -= Time.deltaTime;
            if (spellTimeCounter < 0)
            {
                castSpell = false;
                _animator.SetBool(SPELLCAST, false);
                
                weapon.DeactivateWeapon(true);
                
            }
            return;
        }

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATT, false);
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire3"))
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ATTACK3);
            attacking = true;
            //weapon.DeactivateWeapon(true);
            attackTimeCounter = attackTime;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, true);
        }

        // S = V*T
        if((Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f) && !attacking && !castSpell && !dashSkill)
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), Input.GetAxisRaw(AXIS_V)).normalized * speed; 
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if ((Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f) && !attacking && !castSpell && !dashSkill)
        {
            //Vector3 translation = new Vector3(0, Input.GetAxisRaw(AXIS_V) * speed * Time.deltaTime, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), Input.GetAxisRaw(AXIS_V)).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }
    }

    private void LateUpdate()
    {
        if (!walking && !dashSkill)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool(WALK, walking);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
    }

    public void DeactivateDeadAnimation()
    {
        _animator.SetBool(DEAD, false);
    }

    public void CastSpell(float stime, GameObject selectedSpell)
    {
        castSpell = true;
        attacking = false;
        _animator.SetBool(ATT, false);

        spellTimeCounter = stime;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool(SPELLCAST, true);

        GameObject newSpell = Instantiate(selectedSpell, shootPosition.transform.position, shootPosition.transform.rotation) as GameObject;
        Rigidbody2D spellRB = newSpell.GetComponent<Rigidbody2D>();
        Skill spellSkill = newSpell.GetComponent<Skill>();

        if (spellSkill.skillName == "FIRE")
        {
            spellRB.velocity = lastMovement * shootSpeed;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE_SKILL);
            if (lastMovement.x > 0) //Mirando hacia la derecha
            {
                newSpell.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (lastMovement.x < 0) //Mirando hacia la Izquierda
            {
                newSpell.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (lastMovement.y > 0) //Mirando hacia arriba
            {
                newSpell.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (lastMovement.y < 0) //Mirando hacia la abajo
            {
                newSpell.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        if (spellSkill.skillName == "THUNDER")
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.THUNDER1);
        }
        if (spellSkill.skillName == "ICE")
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.BOSS_ATTACK);
        }
        GetComponent<MPManager>().UseMP(spellSkill.skillMP);
    }

    public void DashSkill()
    {
        //attacking = false;
        //_animator.SetBool(ATT, false);
        dashObject.SetActive(true);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DASH1);
        spellTimeCounter = .40f;
        dashSkill = true;
        Vector2 velocityDirection = lastMovement.normalized * 20;
        _rigidbody.velocity = velocityDirection;

    }

    

}
