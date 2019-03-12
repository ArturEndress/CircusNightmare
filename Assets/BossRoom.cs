using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{

    public GameObject[] portas;
    GameObject portal, player;
    public int counter;
    bool counted, playCong, playThanks;
    float cooldown = 0;

    GameObject welc;



    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;    // opacidade
    float fadeTime = 1f;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        playCong = false;
        playThanks = false;
        welc = GameObject.FindGameObjectWithTag("welc");

        counter = 0;
        counted = false;

        portal = GameObject.Find("Portal");
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < portas.Length; i++)
        {
            portas[i].GetComponent<SpriteRenderer>().enabled = false;
            portas[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counted)
        {
            if (counter == 0)
            {
                start = true;
                isFadeIn = true;
                cooldown += Time.deltaTime;
            }
        }


        //yield return new WaitForSeconds(fadeTime);
        if (cooldown > fadeTime)
        {
            player.transform.position = player.GetComponent<Player>().initialPos;
            player.GetComponent<Animator>().SetFloat("MovY", -1);
            player.GetComponent<Player>().enabled = false;
            isFadeIn = false;
            timer += Time.deltaTime;

            if (timer < 5f)
            {
                playCong = true;
            }
            if (timer > 5f)
            {
                playThanks = true;
            }
            if (playCong)
            {
                StartCoroutine(welc.GetComponent<ScriptWelcome>().ShowWelcome("Congratulations\nYou've completed the game"));
            }
            if (playThanks)
            {
                StartCoroutine(welc.GetComponent<ScriptWelcome>().ShowWelcome("Thank you for playing"));
            }
            if (timer > 10f)
            {
                SceneManager.LoadScene("menu");
            }
        }
    }

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < portas.Length; i++)
            {
                portas[i].GetComponent<SpriteRenderer>().enabled = true;
                portas[i].GetComponent<Collider2D>().enabled = true;
            }
        }
        if (collision.tag == "Enemy")
        {
            counter++;
            counted = true;

            Debug.Log("Contagem Ini: " + counter);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            counter--;

            Debug.Log("Contagem Ini: " + counter);
        }
    }

    //void FadeIn()      // começa transição
    //{
    //    start = true;
    //    isFadeIn = true;

    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled;
    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled;

    //}

    //void FadeOut()
    //{
    //    isFadeIn = false;


    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled;
    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled;


    //    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("MovY", -1);


    //}
    //IEnumerator Transicao()
    //{
    //    FadeIn();

    //    yield return new WaitForSeconds(fadeTime);

    //    player.transform.position = player.GetComponent<Player>().initialPos;

    //    FadeOut();
    //    if (counted)
    //    {
    //        if (counter == 0)
    //        {
    //            FadeIn();

    //            yield return new WaitForSeconds(fadeTime);

    //            player.transform.position = player.GetComponent<Player>().initialPos;

    //            FadeOut();
    //        }
    //    }
    //}
}
