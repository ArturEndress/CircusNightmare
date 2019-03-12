using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {



    Rigidbody2D rb;
    Animator anim;
   Player playerScript;
    float cooldown;
  
	// Use this for initialization
	void Start () {
        cooldown = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        rb.gravityScale = 0;


        if (anim.GetFloat("MovX") == 1 && anim.GetFloat("MovY") == 1)
        {
            rb.AddForce(new Vector2(0, 800));


                transform.position = new Vector2(transform.position.x - 0.35f, transform.position.y + 0.35f);
        }
        else if (anim.GetFloat("MovX") == -1 && anim.GetFloat("MovY") == 1)
        {
            rb.AddForce(new Vector2(0, 800));
            transform.position = new Vector2(-0.35f, transform.position.y + 0.35f);
        }
        else if (anim.GetFloat("MovX") == 1 && anim.GetFloat("MovY") == -1)
        {
            rb.AddForce(new Vector2(0, -800));

           
            transform.position = new Vector2(transform.position.x - 0.37f, transform.position.y - 0.07f);
        }
        else if (anim.GetFloat("MovX") == -1 && anim.GetFloat("MovY") == -1)
        {
            rb.AddForce(new Vector2(0,-800));

            transform.position = new Vector2(transform.position.x - 0.37f, transform.position.y - 0.07f);
        }
        else if (anim.GetFloat("MovX") == 1 && anim.GetFloat("MovY") == 0)
        {
            rb.AddForce(new Vector2(800, 0));

            transform.position = new Vector2(transform.position.x + 0.91f, transform.position.y + 0.35f);

            rb.gravityScale = 1;
        }
        else if (anim.GetFloat("MovX") == -1 && anim.GetFloat("MovY") == 0)
        {
            rb.AddForce(new Vector2(-800, 0));

            transform.position = new Vector2(transform.position.x - 0.37f, transform.position.y + 0.35f);

            rb.gravityScale = 1;
        }
        else if (anim.GetFloat("MovY") == 1 && anim.GetFloat("MovX") == 0)
        {
            rb.AddForce(new Vector2(0, 800));
            transform.position = new Vector2(transform.position.x - 0.35f, transform.position.y + 0.35f);
        }
        else if (anim.GetFloat("MovY") == -1 && anim.GetFloat("MovX") == 0)
        {
            rb.AddForce(new Vector2(0, -800));
            transform.position = new Vector2(transform.position.x - 0.37f, transform.position.y - 0.07f);
        }
        else if (anim.GetFloat("MovX") == 0 && anim.GetFloat("MovY") == 0)
        {
            rb.AddForce(new Vector2(0, -800));
            transform.position = new Vector2(transform.position.x - 0.37f, transform.position.y - 0.07f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        cooldown += Time.deltaTime;
        if (cooldown > 0.25f)
        {
            DestroyObject(gameObject);
        }
       

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //DestroyObject(gameObject);
            //collision.transform.GetComponent<MovementPumpkin>().hp--;
        }
    }
}
