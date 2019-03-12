using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    public int counter;
    bool counted;
    public GameObject[] portas;
    public GameObject[] baus;
    GameObject entrada;
    // Use this for initialization
    void Start()
    {
        counter = 0;

        entrada = GameObject.Find("PortaEntrada");

        entrada.GetComponent<Collider2D>().enabled = false;
        entrada.GetComponent<SpriteRenderer>().enabled = false;

        for (int i = 0; i < portas.Length; i++)
        {
            portas[i].GetComponent<Collider2D>().enabled = false;
            portas[i].GetComponent<SpriteRenderer>().enabled = false;
            if (portas[i].name == "PortaCima")
            {
                portas[i].GetComponent<Collider2D>().enabled = true;
                portas[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            if (portas[i].name == "PortaCima2")
            {
                portas[i].GetComponent<Collider2D>().enabled = true;
                portas[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        for (int i = 0; i < baus.Length; i++)
        {
            baus[i].GetComponent<Collider2D>().enabled = false;
            baus[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (counted)
        {
            if (counter == 0)
            {
                for (int i = 0; i < portas.Length; i++)
                {
                    Destroy(portas[i].gameObject);
                }
                for (int i = 0; i < baus.Length; i++)
                {
                    baus[i].GetComponent<Collider2D>().enabled = true;
                    baus[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

        if (gameObject.name == "Sala D2 Manager (4)" && counter == 0)
        {
            Destroy(GameObject.Find("BossDoor"));
        }
        if (gameObject.name == "Sala E Manager (3)" && counter == 0)
        {
            Destroy(GameObject.Find("BossDoor2"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            counter++;
            counted = true;

            Debug.Log("Contagem Ini: " + counter);
        }

        if (collision.gameObject.tag == "Player")
        {
            entrada.GetComponent<Collider2D>().enabled = true;
            entrada.GetComponent<SpriteRenderer>().enabled = true;

            for (int i = 0; i < portas.Length; i++)
            {
                portas[i].GetComponent<Collider2D>().enabled = true;
                portas[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            counter--;

            Debug.Log("Contagem Ini: " + counter);
        }
    }
}
