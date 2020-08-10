using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<GameObject> skills;
    public bool activeSkill;
    // Start is called before the first frame update
    void Start()
    {
        skills = new List<GameObject>();
        foreach (Transform t in transform)
        {
            if (t.GetComponent<Skill>().inInventory)
            {
                skills.Add(t.gameObject);
            }
        }

    }

    public void SelectSkill(string skillName)
    {
        foreach(GameObject s in skills)
        {
            if (s.GetComponent<Skill>().skillName == skillName)
            {

            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
