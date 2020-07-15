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
                         SECWEAPON = "SecWeapon";


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

    public bool canMove = true;
    public bool isTalking;

    // Start is called before the first frame update
    void Start()
    {
        //weapon = FindObjectOfType<WeaponManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        isTalking = false;
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
            //_animator.SetBool(ATT, false);
            return;
        }

        if (attacking)
        {
            /*if (!canMove)
            {
                return;
            }*/
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATT, false);
            }
        }
        /*if (bowing)
        {
            weapon.DeactivateWeapon(false);
            bowTimeCounter -= Time.deltaTime;
            if (bowTimeCounter < 0)
            {
                bowing = false;
                _animator.SetBool(SECWEAPON, false);
                GameObject newArrow = Instantiate(arrow, shootPosition.transform.position,shootPosition.transform.rotation) as GameObject;
                Rigidbody2D arrowRB = newArrow.GetComponent<Rigidbody2D>();
                arrowRB.velocity = lastMovement * arrowSpeed;
            }
        }*/
            
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire3"))
        {
            attacking = true;
            //weapon.DeactivateWeapon(true);
            attackTimeCounter = attackTime;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, true);
        }
            /*
            if (Input.GetButtonDown("Jump"))
            {
                bowing = true;
                bowTimeCounter = bowTime;
                _rigidbody.velocity = Vector2.zero;
                _animator.SetBool(SECWEAPON, true);
                
            }
            */


        // S = V*T
        if((Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f) && !attacking) //&& !bowing)
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), Input.GetAxisRaw(AXIS_V)).normalized * speed; 
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if ((Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f) && !attacking) //&& !bowing)
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
        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool(WALK, walking);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
    }
}
