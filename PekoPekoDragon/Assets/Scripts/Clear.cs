using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    int playerID;
    public bool clearFlag = false;
    public Image clearImage;
    public Sprite[] sprite = new Sprite[5];

    // Use this for initialization
    void Start ()
    {
        clearImage = GetComponent<Image>();
        playerID = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if(clearFlag)
        {
            clearImage.sprite = sprite[playerID - 1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerID = other.gameObject.GetComponent<Player>().PlayerID;
            if (other.gameObject.name == ("Player" + playerID.ToString()))
            {
                clearFlag = true;
            }
        }
    }


}
