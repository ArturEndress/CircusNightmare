using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//enum Direction {walkDown, walkUp, walkLeft, walkRight}
public class MovementPumpkin : MonoBehaviour {

    Animator anim;
    public int direction;
    float changeDir;
    public float spd;
    public float distanciaPlayer;
    GameObject player;
    public int hp;
    public bool attacking;
    public float rayDistance;
    bool isAlive;

    NavMeshAgent agent;
    Transform parent;

    float cooldown;
	// Use this for initialization
	void Start () {
        direction = 0;
        changeDir = 0;
        cooldown = 0;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Boy");
        hp = 5;
        agent = GetComponentInParent<NavMeshAgent>();

        parent = GetComponentInParent<Transform>();

        attacking = false;

        isAlive = true;
            }
	
	// Update is called once per frame
	void Update () {

        if (!isAlive) return;
       
        transform.eulerAngles = new Vector3(90, 90, 90);
        // transform.localPosition = new Vector3(parent.transform.localPosition.x, transform.localPosition.y, parent.transform.localPosition.z);

        anim.SetBool("Attacking", attacking);

       
        if (agent.velocity.x > .5f)
        {

            Ray ray = new Ray(transform.position, Vector3.right);

            RaycastHit hit;


            Debug.DrawRay(transform.position, Vector3.right * rayDistance);

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }

            }
            if (!attacking)
            {
                  anim.SetInteger("Direction", (int)Direction.walkRight);
            }
        }
        else if (agent.velocity.x < -.5f)
        {

            Ray ray = new Ray(transform.position, -Vector3.left);

            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.left * rayDistance);
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }

            }
            if (!attacking)
            {
                anim.SetInteger("Direction", (int)Direction.walkLeft);
            }
        }
        else if (agent.velocity.z > .5f)
        {
           
            Ray ray = new Ray(transform.position, Vector3.forward);

            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.forward * rayDistance);
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }
            }
            if (!attacking)
            {
                 anim.SetInteger("Direction", (int)Direction.walkUp);
            }

        }
        else if (agent.velocity.z < -.5f)
        {
            
            Ray ray = new Ray(transform.position, Vector3.back);

            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.back * rayDistance);
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }
            }

            if (!attacking)
            {
                anim.SetInteger("Direction", (int)Direction.walkDown);

                cooldown += Time.deltaTime;
            }

            if (cooldown > 1)
            {
                attacking = false;
            }
        }


        //else
        //{
        //    anim.SetBool("Attacking", true);

        //    Ray ray = new Ray(transform.position, Vector3.left);

        //    if ((int)Direction.walkLeft == anim.GetInteger("Direction"))
        //    {
        //        ray = new Ray(transform.position, Vector3.left);
        //    }
        //    else if ((int)Direction.walkRight == anim.GetInteger("Direction"))
        //    {
        //        ray = new Ray(transform.position, Vector3.right);
        //    }
        //    else if ((int)Direction.walkUp == anim.GetInteger("Direction"))
        //    {
        //        ray = new Ray(transform.position, Vector3.forward);
        //    }
        //    else if ((int)Direction.walkDown == anim.GetInteger("Direction"))
        //    {
        //        ray = new Ray(transform.position, Vector3.back);
        //    }


        //    RaycastHit hit;

        //    Debug.DrawRay(transform.position, Vector3.right * rayDistance);

        //    if (Physics.Raycast(ray, out hit, rayDistance))
        //    {
        //        if (hit.collider.name != "Boy")
        //        {
        //            attacking = false;
        //        }

        //    }
        //}
        //distanciaPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (hp <= 0)
        {
            //DestroyObject(gameObject);
            anim.SetTrigger("Died");

            isAlive = false;
        }

        //anim.SetInteger("Direction", direction);

        //cooldown += Time.deltaTime;

        //if (cooldown >= 0.8)
        //{
        //    direction = Random.Range(0, 3);
        //    cooldown = 0;
        //}
        //if (distanciaPlayer > 2)
        //{
        //    switch (direction)
        //    {
        //        case 0:

        //            GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - spd * Time.deltaTime);

        //            break;
        //        case 1:

        //            GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y + spd * Time.deltaTime);

        //            break;

        //        case 2:

        //            GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x - spd * Time.deltaTime, GetComponent<Transform>().position.y);

        //            break;

        //        case 3:

        //            GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x + spd * Time.deltaTime, GetComponent<Transform>().position.y);

        //            break;
        //    }

        //}
        //else
        //{
        //    if (player.GetComponent<Transform>().position.y > GetComponent<Transform>().position.y)
        //    {
        //        direction = 1;


        //        GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y + spd * Time.deltaTime);
        //    }
        //    else if (player.GetComponent<Transform>().position.y < GetComponent<Transform>().position.y)
        //    {
        //        direction = 0;


        //        GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - spd * Time.deltaTime);
        //    }

        //    if (player.GetComponent<Transform>().position.x > GetComponent<Transform>().position.x)
        //    {
        //        direction = 3;


        //        GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x + spd * Time.deltaTime, GetComponent<Transform>().position.y);
        //    }
        //    else if (player.GetComponent<Transform>().position.x < GetComponent<Transform>().position.x)
        //    {
        //        direction = 2;


        //        GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x - spd * Time.deltaTime, GetComponent<Transform>().position.y);
        //    }
        //}
        
    }

    //private void LateUpdate()
    //{
        //    transform.eulerAngles = new Vector3(90,90,90);
        //    // transform.localPosition = new Vector3(parent.transform.localPosition.x, transform.localPosition.y, parent.transform.localPosition.z);
        //    if (!attacking)
        //    {
        //        anim.SetBool("Attacking", false);
        //        if (agent.velocity.x > .1f)
        //        {
        //            anim.SetInteger("Direction", (int)Direction.walkRight);

        //            Ray ray = new Ray(transform.position, Vector3.right);

        //            RaycastHit hit;


        //            Debug.DrawRay(transform.position, Vector3.right * rayDistance);

        //            if (Physics.Raycast(ray, out hit, rayDistance))
        //            {
        //                if (hit.collider.tag == "Player")
        //                {
        //                    attacking = true;
        //                }

        //            }

        //        }
        //        else if (agent.velocity.x < -.1f)
        //        {
        //            anim.SetInteger("Direction", (int)Direction.walkLeft);

        //            Ray ray = new Ray(transform.position, -Vector3.left);

        //            RaycastHit hit;
        //            Debug.DrawRay(transform.position, Vector3.left * rayDistance);
        //            if (Physics.Raycast(ray, out hit, rayDistance))
        //            {
        //                if (hit.collider.tag == "Player")
        //                {
        //                    attacking = true;
        //                }

        //            }
        //        }
        //        else if (agent.velocity.z > .1f)
        //        {
        //            anim.SetInteger("Direction", (int)Direction.walkUp);

        //            Ray ray = new Ray(transform.position, Vector3.forward);

        //            RaycastHit hit;
        //            Debug.DrawRay(transform.position, Vector3.forward * rayDistance);
        //            if (Physics.Raycast(ray, out hit, rayDistance))
        //            {
        //                if (hit.collider.tag == "Player")
        //                {
        //                    attacking = true;
        //                }

        //            }
        //        }
        //        else if (agent.velocity.z < -.1f)
        //        {
        //            anim.SetInteger("Direction", (int)Direction.walkDown);

        //            Ray ray = new Ray(transform.position, Vector3.back);

        //            RaycastHit hit;
        //            Debug.DrawRay(transform.position, Vector3.back * rayDistance);
        //            if (Physics.Raycast(ray, out hit, rayDistance))
        //            {
        //                if (hit.collider.tag == "Player")
        //                {
        //                    attacking = true;
        //                }

        //            }
        //        }

        //    }
        //    else
        //    {
        //        anim.SetBool("Attacking", true);

        //        Ray ray = new Ray(transform.position, Vector3.left);

        //        if ((int)Direction.walkLeft == anim.GetInteger("Direction"))
        //        {
        //            ray = new Ray(transform.position, Vector3.left);
        //        }
        //        else if ((int)Direction.walkRight == anim.GetInteger("Direction"))
        //        {
        //            ray = new Ray(transform.position, Vector3.right);
        //        }
        //        else if ((int)Direction.walkUp == anim.GetInteger("Direction"))
        //        {
        //            ray = new Ray(transform.position, Vector3.forward);
        //        }
        //        else if ((int)Direction.walkDown == anim.GetInteger("Direction"))
        //        {
        //            ray = new Ray(transform.position, Vector3.back);
        //        }


        //        RaycastHit hit;

        //        Debug.DrawRay(transform.position, Vector3.right * rayDistance);

        //        if (Physics.Raycast(ray, out hit, rayDistance))
        //        {
        //            if (hit.collider.tag != "Player")
        //            {
        //                attacking = false;
        //            }

        //        }
        //    }
        //}
    
    public void Attack()
    {
        //player.GetComponent<Movement>().hp--;

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Wall")
    //    {
    //        if (collision.transform.position.x > transform.position.x)
    //        {
    //            direction = (int)Direction.walkRight;
    //        }
    //        else if (collision.transform.position.x < transform.position.x)
    //        {
    //            direction = (int)Direction.walkLeft;
    //        }
    //        if (collision.transform.position.y > transform.position.y)
    //        {
    //            direction = (int)Direction.walkUp;
    //        }
    //        else if (collision.transform.position.y < transform.position.y)
    //        {
    //            direction = (int)Direction.walkDown;
    //        }



    //        //direction = Random.Range(0, 3);
    //    }

    //    if (collision.transform.tag == "Player")
    //    {
    //        if (player.GetComponent<Movement>().canHurt)
    //        {
    //            player.GetComponent<Movement>().hp -= 10;
    //            player.GetComponent<Movement>().canHurt = false;




    //            //if (player.transform.position.x > transform.position.x)
    //            //{
    //            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 0));
    //            //}
    //            //else if (player.transform.position.x < transform.position.x)
    //            //{
    //            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 0));
    //            //}
    //            //if (player.transform.position.y > transform.position.y)
    //            //{
    //            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
    //            //}
    //            //else if (player.transform.position.y > transform.position.y)
    //            //{
    //            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -50));
    //            //}
    //        }

    //    }

    //    Debug.Log("Colidiu com" + collision.transform.tag);
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Wall")
    //    {
    //        if (collision.transform.position.x > transform.position.x)
    //        {
    //            direction = (int)Direction.walkRight;
    //        }
    //        else if (collision.transform.position.x < transform.position.x)
    //        {
    //            direction = (int)Direction.walkLeft;
    //        }
    //        if (collision.transform.position.y > transform.position.y)
    //        {
    //            direction = (int)Direction.walkUp;
    //        }
    //        else if (collision.transform.position.y < transform.position.y)
    //        {
    //            direction = (int)Direction.walkDown;
    //        }



    //        //direction = Random.Range(0, 3);
    //    }

    //    if (collision.transform.tag == "Player")
    //    {
    //        if (player.GetComponent<Movement>().canHurt)
    //        {
    //            player.GetComponent<Movement>().hp -= 10;
    //            player.GetComponent<Movement>().canHurt = false;
    //        }

    //    }

    //    Debug.Log("Colidiu com" + collision.transform.tag);
    //}
}
