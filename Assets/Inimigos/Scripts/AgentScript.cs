using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour {


    NavMeshAgent agent;
    GameObject player;
    MovementPumpkin pumpkinScript;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Boy");
        pumpkinScript = GetComponentInChildren<MovementPumpkin>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!pumpkinScript.attacking)
        {
            agent.SetDestination(new Vector3(player.transform.localPosition.x, transform.localPosition.y, player.transform.localPosition.z));
        }
	}
}
