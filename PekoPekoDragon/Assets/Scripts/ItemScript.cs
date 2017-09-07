using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    [SerializeField]
    private float destroyIn;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyIn);
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }
}
