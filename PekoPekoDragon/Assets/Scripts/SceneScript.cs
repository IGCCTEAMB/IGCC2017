using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
