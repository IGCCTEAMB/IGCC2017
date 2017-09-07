using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private int num;
    [SerializeField]
    private float minDist;
    //[SerializeField]
    //private float speed;

    private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
		
        if(dist > minDist)
        {
            Move();
        }
        else
        {
            num = Random.Range(0, waypoints.Length);
        }
	}

    void Move()
    {
        navMeshAgent.SetDestination(waypoints[num].transform.position);
    }
}
