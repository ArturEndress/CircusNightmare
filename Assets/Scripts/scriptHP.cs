using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptHP : MonoBehaviour {

    Player scriptPlayer;
    public int cura;

	// Use this for initialization
	void Start () {
        scriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //cura = 10;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<Player>().hp + cura > 100)
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().hp = 100;

                Debug.Log("Hp Aumentou");

                Debug.Log("Hp atual: " + scriptPlayer.hp);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().hp += cura;

                Debug.Log("Hp Aumentou");

                Debug.Log("Hp atual: " + scriptPlayer.hp);

            }
        }
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log("HP: " + scriptPlayer.hp);
	}
}
