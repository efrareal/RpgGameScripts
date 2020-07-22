using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public Button startGameButton;
    public Button exitGameButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Kings Room");
    }

    public void ExitGame()
    {

    }
}
