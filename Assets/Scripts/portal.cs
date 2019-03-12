using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{

    public GameObject target;
    public AudioClip creepy, park;
    AudioSource soundManager;
    Animator animPlayer;

    // TRANSIÇÃO
    // tela
    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;    // opacidade
    float fadeTime = 1f;

    // texto
    GameObject welcome;


    void Start()
    {
        // esconder as sprites
        //GetComponent<SpriteRenderer>().enabled = false;


        soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        // pegar a animação do player 
        animPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        // pegar o texto welcome
        welcome = GameObject.FindGameObjectWithTag("welc");

    }

    // PASSAR PELO PORTAL


    IEnumerator OnTriggerEnter2D(Collider2D outro)
    {
        //outro.GetComponent<Animator>().enabled = false;
        //outro.GetComponent<Player>().enabled = false;
        FadeIn();

        yield return new WaitForSeconds(fadeTime);

        // passa para o outro portal de saída (filho)
        outro.transform.position = target.transform.GetChild(0).transform.position;
        // muda a sprite pra olhar pra baixo
        //animPlayer.SetFloat("MovY", -1);

        FadeOut();
        outro.GetComponent<Animator>().enabled = true;
        outro.GetComponent<Player>().enabled = true;

        // welcome
        if (gameObject.name == "PortalParque")
        {
            StartCoroutine(welcome.GetComponent<ScriptWelcome>().ShowWelcome("Welcome\nto  the\nShow"));
        }
    }

    // TRANSIÇÃO 

    void OnGUI()
    {
        if (!start)
            return;

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        // "opacidade"
        Texture2D textura;
        textura = new Texture2D(1, 1);  // 1pix x 1 pix
        textura.SetPixel(0, 0, Color.black);
        textura.Apply();

        // desenhar 
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textura);

        // transparência
        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);

            // quando chega a 0, pára
            if (alpha < 0)
                start = false;
        }

    }

    void FadeIn()      // começa transição
    {
        start = true;
        isFadeIn = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled;

    }

    void FadeOut()
    {
        isFadeIn = false;


        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled;


        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("MovY", -1);

        if (soundManager.isPlaying == creepy)
        {
            //soundManager.PlayOneShot(park);
            soundManager.clip = park;
        }
        if (soundManager.isPlaying == park)
        {
            //soundManager.PlayOneShot(creepy);
            soundManager.clip = creepy;
        }
        soundManager.Play();
    }
}