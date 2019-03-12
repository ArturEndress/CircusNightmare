using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptWelcome : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Transição do texto na tela
    public IEnumerator ShowWelcome(string name)
    {
        anim.Play("Welcome_FadeIn");
        transform.GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(1).GetComponent<Text>().text = name;

        yield return new WaitForSeconds(2f);
        anim.Play("Welcome_FadeOut");
    }
}
