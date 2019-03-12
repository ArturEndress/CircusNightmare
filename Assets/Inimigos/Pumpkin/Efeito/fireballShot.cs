using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballShot : MonoBehaviour {


    Animator anim;
    public float spd;
    public float rot;
    float cooldown;
    public int atk;

    int direction;
	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<getAnim>().father.GetComponent<Animator>();
        //anim = GetComponentInParent<GameObject>().GetComponentInParent<Animator>();
        cooldown = 0;
        spd = 5;
        atk = 5;
        rot = 0;
        direction = 0;


        if (anim.GetFloat("MoveX") > .5f)
        {
            rot = 0;

            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 0));

            direction = (int)Direction.walkRight;
        }
        else if (anim.GetFloat("MoveX") < -.5f)
        {
            rot = 180;

            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 0));

            direction = (int)Direction.walkLeft;
        }
        else if (anim.GetFloat("MoveY") > .5f)
        {
            rot = 90;

            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));

            direction = (int)Direction.walkUp;
        }
        else if (anim.GetFloat("MoveY") < -.5f)
        {
            rot = -90;

            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -600));

            direction = (int)Direction.walkDown;
        }

        transform.eulerAngles = new Vector3(0, 0, rot);

       
    }

    // Update is called once per frame
    void Update () {

        cooldown += Time.deltaTime;

        if (cooldown > 1f)
        {
            Destroy(gameObject);
        }


        if (direction == (int)Direction.walkRight)
        {
            transform.position = new Vector2(transform.position.x + spd * Time.deltaTime, transform.position.y);
        }
        else if (direction == (int)Direction.walkLeft)
        {
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, transform.position.y);
        }
        else if (direction == (int)Direction.walkUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + spd * Time.deltaTime);
        }
        else if (direction == (int)Direction.walkDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - spd * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Player")
        {
             Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canHurt)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canHurt = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hp -= atk;
            }
        }

        Debug.Log("Colidiu com " + collision.gameObject.name);
    }
    
}
