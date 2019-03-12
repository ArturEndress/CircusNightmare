using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum Direction {walkDown, walkUp, walkLeft, walkRight}
public class MovementEnemy : MonoBehaviour {

    Animator anim;
    public int direction;
    float changeDir;
    public float spd;
    public float distanciaPlayer;
    GameObject player;
    public int hp;

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
            }
	
	// Update is called once per frame
	void Update () {

        //distanciaPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (hp <= 0)
        {
            DestroyObject(gameObject);
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

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(90,90,90);
       // transform.localPosition = new Vector3(parent.transform.localPosition.x, transform.localPosition.y, parent.transform.localPosition.z);

        if (agent.velocity.x > .1f)
        {
            anim.SetInteger("Direction", (int)Direction.walkRight);
        }
        else if (agent.velocity.x < -.1f)
        {
            anim.SetInteger("Direction", (int)Direction.walkLeft);
        }
        else if (agent.velocity.z > .1f)
        {
            anim.SetInteger("Direction", (int)Direction.walkUp);
        }
        else if (agent.velocity.z < -.1f)
        {
            anim.SetInteger("Direction", (int)Direction.walkDown);
        }
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
