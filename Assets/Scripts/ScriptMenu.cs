using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour {


    //BOTOES
    public void CRED() { }

    public void START()
    {
       // abrir outra cena
        SceneManager.LoadScene("inicio");
    }

    public void EXIT()
    {
        //encerrar o jogo
        Application.Quit();
    }

}