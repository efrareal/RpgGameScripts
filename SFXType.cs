using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXType : MonoBehaviour
{
    public enum SoundType
    {
        ATTACK1, ATTACK2, ATTACK3, DIE, HIT, CHANGE_SCENE, M_START, M_END, GAME_OVER, GATHER_DROPS, UI_START_MENU_SELECT,
        UI_MENU_SELECT, UI_MENU_ERROR, UI_CHANGE_EQ, RECEIVE_ITEM, LOCK_DOOR, MISS_ATTACK, LEVEL_UP, BOSS_ATTACK, ENEMY_DIE, USE_ITEM
    }

    public SoundType type;
}
