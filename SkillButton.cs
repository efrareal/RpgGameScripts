using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public enum SkillType { FIRE = 0, BOW = 1, THUNDER = 2, ICE = 3};

    public SkillType type;

    private PlayerController thePlayer;
    public MPManager mpManager;

    private void Start()
    {
        thePlayer =FindObjectOfType<PlayerController>();

    }

    public void ActivateSkillButton()
    {
        switch (type)
        {
            case SkillType.FIRE:
                if (mpManager.MagicPoints <= 0)
                {
                    return;
                }
                if (!thePlayer.castSpell)
                { 
                    thePlayer.CastSpell(1.0f);
                    thePlayer.selectedSpell = thePlayer.fireSpell;
                }
                break;
            case SkillType.BOW:
                break;
            case SkillType.THUNDER:
                if (mpManager.MagicPoints <= 0)
                {
                    return;
                }
                if (!thePlayer.castSpell)
                {
                    thePlayer.CastSpell(1.0f);
                    thePlayer.selectedSpell = thePlayer.thunderSpell;
                }
                break;
            case SkillType.ICE:
                if (mpManager.MagicPoints <= 0)
                {
                    return;
                }
                if (!thePlayer.castSpell)
                {
                    thePlayer.CastSpell(1.0f);
                    thePlayer.selectedSpell = thePlayer.iceSpell;
                }
                break;
        }
    }
}
