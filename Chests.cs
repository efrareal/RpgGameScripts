using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    private bool hasBeenOpened;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(hasBeenOpened == true)
        {
            _animator.SetBool("chestOpened", true);
            return;
        }
        else {
            if((hasBeenOpened == false) && (Input.GetAxis("Fire3") > 0.2))
            {
                _animator.SetBool("chestOpened", true);
                hasBeenOpened = true;
            }

        }
    }


}
