using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameStateManager : MonoBehaviour
{

    private static CardGameStateManager instance;

    public static CardGameStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (CardGameStateManager)FindObjectOfType(typeof(CardGameStateManager));
                if (instance == null)
                {
                    var obj = new GameObject();
                    obj.AddComponent<CardGameStateManager>();
                }
            }
            return instance;
        }
    }


    public enum GameState
    {
        Invalid = -1,
        GameStart,
        CardDeal,
        PlayerChoiceTurn,
        CPUChoiceTurn,
        GameResult
    }

    public GameState CardGameState = GameState.Invalid;

    public CardDealer CardDealer;

    public CardGamePlayer CardGamePlayer;
    public CardGameCPU CardGameCPU;

    public bool PlayerIsWin = false;

    public float WaitTurnTime = 3f;

    public void NextState()
    {
        if (CardGameState == GameState.GameResult)
        {
            return;
        }

        if (CardGamePlayer.PlayerCards.Count == 1 && CardGameCPU.CPUCards.Count == 0)
        {
            CardGameState = GameState.GameResult;
            return;
        }

        if (CardGameCPU.CPUCards.Count == 1 && CardGamePlayer.PlayerCards.Count == 0)
        {
            PlayerIsWin = true;
            CardGameState = GameState.GameResult;
            return;
        }

        CardGameState++;
    }

    private void Update()
    {
        switch (CardGameState)
        {
            case GameState.Invalid:
                NextState();
                break;

            case GameState.GameStart:
                NextState();
                break;
            case GameState.CardDeal:
                CardDealer.CardDeal();

                CardGamePlayer.GameStartInitialize();
                CardGameCPU.GameStartInitialize();
                NextState();
                break;

            case GameState.PlayerChoiceTurn:

                break;

            case GameState.CPUChoiceTurn:

                WaitTurnTime -= Time.deltaTime;

                CardGameCPU.IsCPUWaiting = true;

                if (WaitTurnTime > 0)
                {
                    return;
                }
                WaitTurnTime = 3f;

                CardGameCPU.IsCPUWaiting = false;

                var random = Random.Range(0, CardGamePlayer.PlayerCards.Count);
                var CPUChoiceCard = CardGamePlayer.PlayerCards[random];
                CardGameCPU.CardChoice(CPUChoiceCard);

                if (CardGamePlayer.PlayerCards.Count > 0 && CardGameCPU.CPUCards.Count > 0)
                {
                    CardGameState = GameState.PlayerChoiceTurn;
                    return;
                }
                NextState();
                break;

            case GameState.GameResult:

                break;
        }
    }
}
