using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Velodcidad de movimiento")]
    public float speed = 1;
    private Rigidbody2D _rigidbody;

    private bool isMoving;
    private bool wasHit;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 directionToMove;

    private Transform thePlayer;

    //Animacion
    private Animator _animator;

    private int xPos;
    private int yPos;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //Para que arranque moviendose
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (wasHit)
            {
                isMoving = true;
                this.transform.position = Vector2.MoveTowards(this.transform.position, thePlayer.position, -2* speed * Time.deltaTime);
                directionToMove = this.transform.position - thePlayer.position;
                _animator.SetFloat("Horizontal", directionToMove.x);
                _animator.SetFloat("Vertical", directionToMove.y);
                return;
            }
            //Debug.Log("Player entró en la zona de vision");
            isMoving = true;
            this.transform.position = Vector2.MoveTowards(this.transform.position, thePlayer.position, speed * Time.deltaTime);
            directionToMove = thePlayer.position - this.transform.position;
            _animator.SetFloat("Horizontal", directionToMove.x);
            _animator.SetFloat("Vertical", directionToMove.y);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            //Debug.Log("Player salio de la zona de vision");
            isMoving = false;
            wasHit = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            _rigidbody.velocity = directionToMove * speed;

            //Cuando me quedo sin tiempo de movimeinto paramos al enemigo
            if(timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;

            //Cuando me quedo sin tiempo de estar parado
            if(timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
                _animator.SetFloat("Horizontal", directionToMove.x);
                _animator.SetFloat("Vertical", directionToMove.y);
            }
        }
    }

    public void EnemyWasHit()
    {
        wasHit = true;
    }
}
