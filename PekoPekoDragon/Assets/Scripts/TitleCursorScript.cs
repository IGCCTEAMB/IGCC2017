using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleCursorScript : MonoBehaviour {

    [SerializeField]
    private GameObject _playButton;
    [SerializeField]
    private GameObject _quitButton;

    [SerializeField]
    private Sprite[] _playSprites;

    [SerializeField]
    private Sprite[] _quitSprites;

    private bool _playflag = true;

    // Use this for initialization
    void Start () {
        _playButton.GetComponent<Image>().sprite = _playSprites[1];
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKey(KeyCode.RightArrow))
        {
            if(_playflag)
            {
                _playflag = false;
                _playButton.GetComponent<Image>().sprite = _playSprites[0];
                _quitButton.GetComponent<Image>().sprite = _quitSprites[1];
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_playflag)
            {
                _playflag = true;
                _playButton.GetComponent<Image>().sprite = _playSprites[1];
                _quitButton.GetComponent<Image>().sprite = _quitSprites[0];
            }
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
        {
            if (_playflag) SceneManager.LoadScene(1);
            else Application.Quit();
        }
	}
}
