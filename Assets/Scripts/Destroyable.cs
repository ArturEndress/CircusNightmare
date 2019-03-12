using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Destroyable : MonoBehaviour
{

    //guarda estado da destruição
    public string destroyState;

    Animator anim;
    // espera antes de tirar a colisão
    public float timer;
    public bool colidiu;
    public float timeToDesable;

    public GameObject item;
    public Transform itemPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        timer = 0;
        colidiu = false;
    }

    // Detectamos colisão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // GetComponent<Animator>().SetTrigger("isOpen");
            anim.Play(destroyState);

            colidiu = true;
        }
    }

    void Update()
    {
        if (colidiu)
            timer += Time.deltaTime;

        if (timer>timeToDesable) {
            //GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().sortingOrder= 2;
        }
    }
    public void CriarItem()
    {
        Instantiate(item, itemPos);
    }
}