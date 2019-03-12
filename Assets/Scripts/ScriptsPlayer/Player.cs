using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed = 4f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;
    public Vector2 initialPos;

    public int hp;
    Image image;
    public bool canHurt;
    float cooldown;
    bool attacking;
    bool isDead;

    // TIRO
    public GameObject pedra;
    public Transform pedraOut;

    //BUFFS
    public bool buffSpd;
    float cooldownSpd;
    float TimeSpd;
    float originalSpd;
    float spdIncrese;


    //GAME OVER
    GameObject welc;


    void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        initialPos = transform.position;

        image = GameObject.Find("HpUI").GetComponent<Image>();
        canHurt = true;
        cooldown = 0;
        isDead = false;

        attacking = false;

        //SPD CANDY STUFF
        buffSpd = false;
        cooldownSpd = 0;
        TimeSpd = GameObject.Find("Doce (Speed)").GetComponent<scriptDoce>().tempoDeAtivacao;
        spdIncrese = GameObject.Find("Doce (Speed)").GetComponent<scriptDoce>().acelera;
        originalSpd = speed;


        welc = GameObject.FindGameObjectWithTag("welc");
    }

    // Update is called once per frame
    void Update () {



        //Morte
        
       

        if (isDead)
        {
            if (cooldown >= 5f)
            {
                SceneManager.LoadScene("menu");
            }
            cooldown += Time.deltaTime;
            return;
        }


        if (hp <= 0)
        {
            isDead = true;
            cooldown = 0;
            anim.SetTrigger("Died");
            StartCoroutine(welc.GetComponent<ScriptWelcome>().ShowWelcome("GAME\nOVER"));
        }


        //BUFF SPD
        Buffs();


        // LIFE UI
        image.fillAmount = (float)hp / 100;

        if (!canHurt)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

            cooldown += Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        if (cooldown > 1)
        {
            canHurt = true;
            cooldown = 0;
        }

        // Movimentação
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        // deixar player parado na posição certa
        // andando
        if (mov != Vector2.zero)
        {
            anim.SetFloat("MovX", mov.x);
            anim.SetFloat("MovY", mov.y);
            anim.SetBool("isWalking", true);
        }
        // parado
        else
        {
            anim.SetBool("isWalking", false);
        }

        // Ataque
        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //bool attacking = stateInfo.IsName("Attack");

        //if (Input.GetKeyDown("space") && !attacking) 
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("attacking");
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }

    // Movimentar Rigid Body
    void FixedUpdate () {
        if (!attacking)
        {
            rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
        }
    }
    public void Shot()
    {
        Instantiate(pedra, pedraOut);
    }
    void Buffs()
    {
        if (buffSpd)
        {
            speed += spdIncrese;
            cooldownSpd = 0;
            buffSpd = false;
        }
        cooldownSpd += Time.deltaTime;
        if (cooldownSpd >= TimeSpd)
        {
            speed = originalSpd;
        }
    }
}
