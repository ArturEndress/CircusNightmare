using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPumpkin : MonoBehaviour
{

    public float dirX, dirY;
    Animator anim;
    GameObject player;
    public float visionRadius, shotDistance, minRadius, speed;
    Rigidbody2D rb2d;
    public bool attacking;
    bool isDead;
    public int hp;
    bool atingiu;
    float cooldownAtingiu;

    float cooldown;

    public Vector3 target, currentPosition;

    //TIRO
    public GameObject shot;
    public Transform shotPos;

    //
    public float distance;
    public Vector3 dir;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attacking = false;
        isDead = false;
        cooldown = 0;
        target = currentPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {




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

        if (atingiu)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            cooldownAtingiu += Time.deltaTime;
            if (cooldownAtingiu > 1)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        currentPosition = transform.position;

        target = currentPosition;

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


        distance = Vector3.Distance(target, transform.position);
        dir = (target - transform.position).normalized;
        ///////////////


        if (target != currentPosition && distance < minRadius)
        {
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("MoveX", dir.x);
            anim.SetFloat("MoveY", dir.y);
            //anim.SetBool("isMoving", false);
            //anim.Play("isMoving", -1, 0);  // Congela la animación de andar

            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            //if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        // En caso contrario nos movemos hacia él
        else
        {


            if (!attacking)
            {


                rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
                anim.SetBool("isMoving", true);

                //transform.Translate(transform.position + dir);

                // Al movernos establecemos la animación de movimiento
                anim.speed = 1;
                anim.SetFloat("MoveX", dir.x);
                anim.SetFloat("MoveY", dir.y);
            }
        }

        if (target == currentPosition && distance < 0.05f)
        {
            transform.position = currentPosition;
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("isMoving", false);
        }
        
        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
        //MIRA NO PLAYER



        //if (anim.GetFloat("MoveX") > .5f)
        //{

        //    GetComponentInChildren<Transform>().rotation = new Quaternion(0, 0, 90, 0);
        //}
        //else if (anim.GetFloat("MoveX") < -.5f)
        //{

        //    GetComponentInChildren<Transform>().rotation = new Quaternion(0, 0, -90, 0);

        //}
        //else if (anim.GetFloat("MoveY") > .5f)
        //{

        //    GetComponentInChildren<Transform>().rotation = new Quaternion(0, 0, 180, 0);

        //}
        //else 
        //{

        //    GetComponentInChildren<Transform>().rotation = new Quaternion(0, 0, 0, 0);


        //}

        

        dirX = anim.GetFloat("MoveX");
        dirY = anim.GetFloat("MoveY");

            //if (target != currentPosition && distance < shotDistance)
            //{
            //    //if (player.GetComponent<Player>().canHurt)
            //    //{
            //    //    player.GetComponent<Player>().hp -= 10;
            //    //    player.GetComponent<Player>().canHurt = false;
            //    //}

        //    RaycastHit2D aim = Physics2D.Raycast(
        //      transform.position,
        //      Vector2.down,
        //      visionRadius,
        //     1 << LayerMask.NameToLayer("Default"));


        //    if (anim.GetFloat("MoveX") > .5f)
        //    {

        //        aim = Physics2D.Raycast(
        //        transform.position,
        //        Vector2.right,
        //        visionRadius,
        //       1 << LayerMask.NameToLayer("Default"));



        //    }
        //    else if (anim.GetFloat("MoveX") < -.5f)
        //    {

        //        aim = Physics2D.Raycast(
        //        transform.position,
        //        Vector2.left,
        //        visionRadius,
        //       1 << LayerMask.NameToLayer("Default"));


        //    }
        //    else if (anim.GetFloat("MoveY") > .5f)
        //    {

        //        aim = Physics2D.Raycast(
        //        transform.position,
        //        Vector2.up,
        //        visionRadius,
        //       1 << LayerMask.NameToLayer("Default"));


        //    }
        //    else if (anim.GetFloat("MoveY") < -.5f)
        //    {

        //        aim = Physics2D.Raycast(
        //        transform.position,
        //        Vector2.down,
        //        visionRadius,
        //       1 << LayerMask.NameToLayer("Default"));


        //    }

        //    if (aim.collider.tag == "Player")
        //    {
        //        attacking = true;

        //    }
        //    else
        //    {
        //        attacking = false;
        //    }


        //    anim.SetBool("isAttacking", attacking);

        //}

        anim.SetBool("isAttacking", attacking);

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
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.DrawWireSphere(transform.position, shotDistance);


    }

    public void Attack()
    {
        Instantiate(shot, shotPos);
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attacking = true;
            //anim.SetBool("attacking", true);
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attacking = false;
            //anim.SetBool("attacking", true);
        }

    }

}