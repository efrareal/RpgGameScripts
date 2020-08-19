using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator _animator;
    public float transitionTime= 2.0f;
    private float transitioncounter;
    private bool transition;
    private string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        transitioncounter = transitionTime;
    }

    private void Update()
    {
        if (transition)
        {
            transitioncounter -= Time.deltaTime;
            if (transitioncounter < 0)
            {
                transition = false;
                transitioncounter = transitionTime;
                SceneManager.LoadScene(levelToLoad);
                
            }

        }
    }

    public void Transition(string scene)
    {
        _animator.SetBool("FadeOut", true);
        transition = true;
        levelToLoad = scene;

    }
}
