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

	void Awake ()
    {
        Instance = this;
        SetCurrentState(GameState.Start);
	}

    void Update()
    {
        var db = dragon.GetComponent<DragonBehaviour>();

        db.MoodValue += 0.1f;

        float amount =db.MoodValue / db.MAX_MOOD_VALUE;
        dragonMoodValue.GetComponent<Image>().fillAmount = amount;
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
