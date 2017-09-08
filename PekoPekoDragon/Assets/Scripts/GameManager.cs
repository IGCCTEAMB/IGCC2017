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

	void Awake ()
    {
        Instance = this;
        SetCurrentState(GameState.Start);
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
}
