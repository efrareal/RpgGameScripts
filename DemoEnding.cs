using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnding : MonoBehaviour
{

    public void ExitGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Application.Quit();
    }
    public void GoToMainScreen()
    {
        FindObjectOfType<UIManager>().NewStart();
    }
}
