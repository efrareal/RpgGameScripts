using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public enum SkillType { FIRE = 0, BOW = 1, THUNDER = 2, ICE = 3, DASH = 4};

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
                    thePlayer.CastSpell(1.0f, thePlayer.fireSpell);
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
                    thePlayer.CastSpell(1.0f, thePlayer.thunderSpell);
                }
                break;
            case SkillType.ICE:
                if (mpManager.MagicPoints <= 0)
                {
                    return;
                }
                if (!thePlayer.castSpell)
                {
                    thePlayer.CastSpell(1.0f, thePlayer.iceSpell);
                }
                break;
            case SkillType.DASH:
                if (mpManager.MagicPoints <= 0)
                {
                    return;
                }
                if (!thePlayer.castSpell || !thePlayer.dashSkill)
                {
                    thePlayer.DashSkill();
                    mpManager.UseMP(3);
                }
                break;
        }
    }
}
