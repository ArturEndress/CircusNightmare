using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponentInParent<Animator>().GetFloat("MoveX") > .5f)
        {

            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (GetComponentInParent<Animator>().GetFloat("MoveX") < -.5f)
        {

            transform.eulerAngles = new Vector3(0, 0, -90);

        }
        else if (GetComponentInParent<Animator>().GetFloat("MoveY") > .5f)
        {

            transform.eulerAngles = new Vector3(0, 0, 180);

        }
        else
        {

            transform.eulerAngles = new Vector3(0, 0, 0);


        }

    }

}
