using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitleCameraMovement : MonoBehaviour {

    //[SerializeField]
    //private GameObject[] waypoints;
    //[SerializeField]
    //private int num;
    //[SerializeField]
    //private float minDist;

    float timeCounter;
    public float speed = 2;
    public float width = 4;
    public float height = 7;
    public float yPos;

    public GameObject targetObject;

    //private NavMeshAgent navMeshAgent;
    void Start()
    {
        //navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        timeCounter += Time.deltaTime * speed;

        float x = (Mathf.Cos(timeCounter) * width) + 10.0f;
        float y = yPos;
        float z = (Mathf.Sin(timeCounter) * height) + 5.0f;

        Camera.main.transform.LookAt(targetObject.transform);
        transform.position = new Vector3(x, y, z);
        //float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        //
        //if (dist > minDist)
        //{
        //    Move();
        //}
        //else
        //{
        //    num = Random.Range(0, waypoints.Length);
        //}

    }
    void Move()
    {
         //navMeshAgent.SetDestination(waypoints[num].transform.position);
    }
}
