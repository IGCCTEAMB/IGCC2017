using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningItem : MonoBehaviour {

    [SerializeField]
    private float _repeatRate; 

    public GameObject[] itemPrefabs;
    
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 1, _repeatRate);
	}

    void Spawn()
    {
        int itemNum = Random.Range(0, itemPrefabs.Length);

        switch(itemNum)
        {
            case 0:
                Instantiate(itemPrefabs[0], new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z), Quaternion.identity);
                break;
            case 1:
                Instantiate(itemPrefabs[1], new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z), Quaternion.identity);
                break;
        }
    }
}
