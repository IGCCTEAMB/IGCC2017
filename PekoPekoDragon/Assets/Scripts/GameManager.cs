using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Start,
    Prepare,
    Playing,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 現在の状態
    private GameState currentGameState;

    public Text label;

    public GameObject[] players = new GameObject[4];
    public GameObject item;
    public GameObject dragon;
    public GameObject dragonMoodValue;
    public GameObject dragonIconUI;

    public Sprite[] dragonIcons;

    private DragonBehaviour db;

	void Awake ()
    {
        Instance = this;
        SetCurrentState(GameState.Start);
	}
    void Start()
    {
        db = dragon.GetComponent<DragonBehaviour>();
    }

    void Update()
    {
        Debug.Log(db.DragonMoodState);
        Debug.Log(db.MoodValue);

        db.MoodValue -= 0.1f;

        float amount = db.MoodValue / db.MAX_MOOD_VALUE;
        dragonMoodValue.GetComponent<Image>().fillAmount = amount;


        if (db.DragonMoodState == DragonBehaviour.MoodState.NORMAL)
        {
            if(db.MoodValue < 0 )
            {
                db.DragonMoodState = DragonBehaviour.MoodState.BAD;
                db.MoodValue = db.MAX_MOOD_VALUE;
                dragonMoodValue.GetComponent<Image>().color = Color.red;
                dragonMoodValue.GetComponent<Image>().fillAmount = 1;
            }
        }else if(db.DragonMoodState == DragonBehaviour.MoodState.BAD)
        {
            if(db.MoodValue < 0)
            {
                db.DragonMoodState = DragonBehaviour.MoodState.NORMAL;
                db.MoodValue = 30;
                dragonMoodValue.GetComponent<Image>().color = Color.cyan;
                dragonMoodValue.GetComponent<Image>().fillAmount = 1;
            }
        }

    }

    public void ChangeIcon(int num)
    {
        dragonIconUI.GetComponent<Image>().sprite = dragonIcons[num];
    }


    public void SetCurrentState(GameState state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                StartAction();
                break;
            case GameState.Prepare:
                StartCoroutine(PrepareCoroutine());
                break;
            case GameState.Playing:
                PlayingAction();
                break;
            case GameState.End:
                EndAction();
                break;
            default:
                break;
        }
    }
	
    void StartAction()
    {
    }

    IEnumerator PrepareCoroutine()
    {
        label.text = "3";
        yield return new WaitForSeconds(1);
        label.text = "2";
        yield return new WaitForSeconds(1);
        label.text = "1";
        yield return new WaitForSeconds(1);
        label.text = "";
        SetCurrentState(GameState.Playing);
    }

    void PlayingAction()
    {
    }

    void EndAction()
    {
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }
}
