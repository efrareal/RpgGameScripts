using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour
{
    public GameObject leader; // the game object to follow - assign in inspector
    public int steps; // number of steps to stay behind - assign in inspector

    private Queue<Vector3> record = new Queue<Vector3>();
    private Vector3 lastRecord;

    private Rigidbody2D _rigibody;
    private Vector2 offSet = new Vector2(0.9f, 0.9f);

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        // record position of leader
        record.Enqueue(leader.transform.position * offSet);

        // remove last position from the record and use it for our own
        if (record.Count > steps)
        {
            this.transform.position = record.Dequeue();
            //_rigibody.velocity = new Vector2(0,0);
            
        }
    }
}
