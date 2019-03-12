using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scriptPause : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject telaPauseUI;

    void Update () {
        // tecla ESC para pausar o jogo
        if (Input.GetKeyDown(KeyCode.Escape))   {
            Pause();
        }
	}

    void Pause()    {
        telaPauseUI.SetActive(true);
        // muda a velocidade do tempo para zero
        Time.timeScale = 0f;
        isPaused = true;

    }

    // continuar
    public void Resume()    {
        telaPauseUI.SetActive(false);
        // volta o tempo para o normal
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void LoadMenu()    {
        // volta o tempo para o normal
        Time.timeScale = 1f;
        // abrir outra cena
        SceneManager.LoadScene("menu");
    }

    public void LoadHelp()
    {    }

}
