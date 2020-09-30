using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithWeapon : MonoBehaviour
{
    public float speed = 1.5f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public bool isWalking = false;

    public float walkTime = 1.5f;
    private float walkCounter;
    public float waitTime = 4.0f;
    private float waitCounter;

    private bool isChasing;
    private bool isAttacking;
    public float attackTime = 1f;
    private float attackCounter;

    public float attackRange = 3f;

    private Transform thePlayer;

    private Vector2[] walkingDirections =
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

    private int currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        thePlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        waitCounter = waitTime;
        walkCounter = walkTime;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Vector2.Distance(thePlayer.position, this.transform.position) <= attackRange)
            {
                Debug.Log("Llego a la zona de ataque");
                isWalking = false;
                isAttacking = true;
                Vector2 directionToMove;
                directionToMove = thePlayer.position - this.transform.position;
                _animator.SetFloat("LastH", directionToMove.normalized.x);
                _animator.SetFloat("LastV", directionToMove.normalized.y);
            }
            else
            {
                isChasing = true;
                isAttacking = false;
                isWalking = true;
                this.transform.position = Vector2.MoveTowards(this.transform.position, thePlayer.position, speed * Time.fixedDeltaTime);
                Vector2 directionToMove;
                directionToMove = thePlayer.position - this.transform.position;
                _animator.SetFloat("Horizontal", directionToMove.normalized.x);
                _animator.SetFloat("Vertical", directionToMove.normalized.y);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isWalking = false;
            isChasing = false;
            isAttacking = false;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isChasing)
        {
            return;
        }
        if (isAttacking)
        {
            return;
        }
        if (isWalking)
        {
            _rigidbody.velocity = walkingDirections[currentDirection] * speed;
            walkCounter -= Time.fixedDeltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            waitCounter -= Time.fixedDeltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
        _animator.SetFloat("LastH", walkingDirections[currentDirection].x);
        _animator.SetFloat("LastV", walkingDirections[currentDirection].y);
        _animator.SetBool("Attacking", isAttacking);
    }

    public void StartWalking()
    {
        currentDirection = Random.Range(0, walkingDirections.Length);
        isWalking = true;
        walkCounter = walkTime;
    }
    public void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        _rigidbody.velocity = Vector2.zero;
    }
}
