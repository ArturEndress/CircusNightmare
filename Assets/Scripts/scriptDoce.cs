using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptDoce : MonoBehaviour {
    
    Player scriptPlayer;
    //public int cura;
    public float acelera;
    public float tempoDeAtivacao;
    void Start()
    {
        scriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        acelera = 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //        if (collision.gameObject.GetComponent<Player>().hp + cura > 100)


            scriptPlayer.buffSpd = true;
            
            Debug.Log("Vel Aumentou");
            Debug.Log("Vel atual: " + scriptPlayer.speed);

            
        }
    }
    // colocar temporizador


    //// Update is called once per frame
    void Update()
    {
        //Debug.Log("Vel: " + scriptPlayer.speed);
    }
}
