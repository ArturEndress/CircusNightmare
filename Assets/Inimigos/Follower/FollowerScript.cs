using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    Animator anim;
    GameObject player;
    public float visionRadius, aoeDistance, minRadius, speed;
    Rigidbody2D rb2d;

    public int hp;

    bool isDead;
    bool atingiu;
    float cooldownAtingiu;
    float cooldown;
    bool movePos;

    public Vector3 target, initialPosition;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        isDead = false;
        cooldown = 0;
        movePos = false;


	}
	
	// Update is called once per frame
	void Update () {

        if (cooldown >= 2f)
        {
            Destroy(gameObject);
        }
        // Morte Temporária - Chamar Animação e Botar Cooldown pra Destruir
        if (isDead)
        {
            if (!movePos)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - .8f);
                movePos = true;
                //GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            GetComponent<SpriteRenderer>().enabled = true;
            cooldown += Time.deltaTime;
            return;
        }

        if (hp <= 0)
        {
            anim.SetTrigger("Died");

           
            isDead = true;
        }


        if (atingiu)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            cooldownAtingiu += Time.deltaTime;
            if (cooldownAtingiu > 1)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        target = initialPosition;

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
                target = player.transform.position;
            }
        }


        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        ///////////////
        if (target != initialPosition && distance <= minRadius)
        {
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("MoveX", dir.x);
            anim.SetFloat("MoveY", dir.y);
            anim.SetBool("isMoving", false);
            //anim.Play("isMoving", -1, 0);  // Congela la animación de andar

            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            //if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        // En caso contrario nos movemos hacia él
        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            //transform.Translate(transform.position + dir);

            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("MoveX", dir.x);
            anim.SetFloat("MoveY", dir.y);
            anim.SetBool("isMoving", true);
        }

        if (target == initialPosition && distance < 0.05f)
        {
            transform.position = initialPosition;
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("isMoving", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);


        // DANO NO INIMIGO DENTRO DA ÁREA
        if (target != initialPosition && distance < aoeDistance)
        {
            if (player.GetComponent<Player>().canHurt)
            {
                player.GetComponent<Player>().hp -= 10;
                player.GetComponent<Player>().canHurt = false;
            }
        }


    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.DrawWireSphere(transform.position, aoeDistance);


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
}
