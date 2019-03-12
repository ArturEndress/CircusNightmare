using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptVampire : MonoBehaviour {
    Animator anim;
    GameObject player;
    public float visionRadius, speed;
    Rigidbody2D rb2d;
    public bool attacking;
    bool isDead;
    public int hp;
    bool turned;
    float wait;
    public float vampSpd;
    public float batSpd;
    public int atk;
    bool atingiu;
    float cooldownAtingiu;

    public GameObject[] postos;
    float cooldown;

    public Vector3 target, currentPosition;

   

    //
    public float distance;
    public Vector3 dir;
    int randPosto;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attacking = false;
        isDead = false;
        cooldown = 0;
        randPosto = 1;
        turned = false;
        vampSpd = speed;
        batSpd = 5;
        atk = 10;

    }

    // Update is called once per frame
    void Update()
    {

        if (atingiu)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            cooldownAtingiu += Time.deltaTime;
            if (cooldownAtingiu > 1)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }


        if (isDead)
        {

            GetComponent<SpriteRenderer>().enabled = true;
            cooldown += Time.deltaTime;

            if (cooldown > 2f)
            {
                Destroy(gameObject);
            }

            return;
        }

        currentPosition = transform.position;

        if (Vector2.Distance(transform.parent.GetComponentInChildren<Transform>().Find("posto" + randPosto).position, transform.position) < 1)
        {
            randPosto++;
            
        }
        if (randPosto > 4)
        {
            randPosto = 1;
        }

        target = transform.parent.GetComponentInChildren<Transform>().Find("posto" + randPosto).position;
        


        Debug.Log("Posto: " + randPosto);

        RaycastHit2D hit = Physics2D.Raycast(
           transform.position,
           player.transform.position - transform.position,
           visionRadius,
          1 << LayerMask.NameToLayer("Default")

       );


        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                attacking = true;

                if (!turned)
                {
                    
                    

                    if (wait>.5f)
                    {
                        anim.SetFloat("Idle", Random.Range(1, 4));
                        turned = true;
                        anim.SetBool("isTurned", true);
                        wait = 0;
                        speed = batSpd;
                        anim.SetFloat("MoveY", 1);
                        anim.SetFloat("MoveX", 0);
                        
                    }
                }

                target = player.transform.position;
                wait += Time.deltaTime;

                if (turned)
                {
                    anim.SetBool("isMoving", true);


                }
                if (anim.GetBool("isMoving") && turned)
                {
                    if (wait > 1.4)
                    {
                        if (wait < 7)
                        {
                            GetComponent<Collider2D>().isTrigger = true;
                            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
                        }
                    }
                }
                if (wait >= 7)
                {

                    wait = 0;
                    turned = false;
                    anim.SetBool("isTurned", false);
                    GetComponent<Collider2D>().isTrigger = false;
                    //anim.SetFloat("Idle", Random.Range(1.0f, 4.0f));

                }
            }

            else
            {

                if (attacking)
                {
                    wait = 0;

                    anim.SetFloat("Idle", 0);
                    anim.SetBool("isTurned", false);
                    attacking = false;
                    //anim.SetBool("isMoving", false);


                    turned = false;

                    speed = vampSpd;

                }
            }
        }
      



        distance = Vector3.Distance(target, transform.position);
        dir = (target - transform.position).normalized;
        ///////////////


       
        if (!attacking && !turned)
        {
            

            wait += Time.deltaTime;
            if (wait > .8f)
            {
                rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            }
            anim.SetBool("isMoving", true);

            //transform.Translate(transform.position + dir);

            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("MoveX", dir.x);
            anim.SetFloat("MoveY", dir.y);
        }
        

        

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);


        //MIRA NO PLAYER
       

        if (hp <= 0)
        {
            anim.SetTrigger("Died");
            isDead = true;
        }

    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
       


    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            hp--;

            Destroy(collision.gameObject);

            atingiu = true;
            cooldownAtingiu = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.GetComponent<Player>().canHurt)
            {
                player.GetComponent<Player>().hp -= atk;

                player.GetComponent<Player>().canHurt = false;
            }
        }
        
    }
}