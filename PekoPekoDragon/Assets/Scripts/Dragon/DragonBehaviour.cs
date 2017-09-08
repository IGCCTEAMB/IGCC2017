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
    [SerializeField]
    private float _minDistWithPlayer = 5;
    [SerializeField]
    private GameObject[] dragonAttacks;

    [SerializeField]
    private GameObject _targetObject;

    private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
       
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        _targetObject = gameObject.transform.GetChild(0).GetComponent<DragonDetector>().targetObject;

        if (dist > minDist)
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
        if(!_targetObject)
        {
            navMeshAgent.SetDestination(waypoints[num].transform.position);
        }else{

            float dist = Vector3.Distance(gameObject.transform.position, _targetObject.transform.position);

            if (dist < _minDistWithPlayer)
            {
                navMeshAgent.isStopped = true;
                GameObject par = Instantiate(dragonAttacks[Random.Range(0, dragonAttacks.Length)],gameObject.transform.GetChild(1).transform.position,gameObject.transform.GetChild(1).transform.rotation);
                gameObject.transform.LookAt(_targetObject.transform.position);
                
            }
            else
            {
               navMeshAgent.isStopped = false;

               navMeshAgent.SetDestination(_targetObject.transform.position);
            }

        }

    }
}
