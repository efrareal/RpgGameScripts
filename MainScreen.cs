using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public Button startGameButton;
    public Button exitGameButton;
    private PlayerController thePlayer;

    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        thePlayer = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.ToggleHUD();

    }

    public void StartGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        thePlayer.isTalking = false;
        thePlayer.canMove = true;
        SceneManager.LoadScene("Kings Room");
        uiManager.ToggleHUD();

    }

    public void ExitGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.UI_START_MENU_SELECT);
        Application.Quit();
    }
}
