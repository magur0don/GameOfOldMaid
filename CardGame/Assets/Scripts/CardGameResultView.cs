using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameResultView : MonoBehaviour
{
    public CardGameStateManager CardGameStateManager;
    public TextMeshProUGUI ResultText;

    private void Start()
    {
        ResultText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (CardGameStateManager.CardGameState == CardGameStateManager.GameState.GameResult)
        {
            if (string.IsNullOrEmpty(ResultText.text))
            {
                if (CardGameStateManager.PlayerIsWin)
                {
                    ResultText.text = "Player Win!";
                    ResultText.color = Color.red;
                }
                else
                {
                    ResultText.text = "Player Lose...";
                    ResultText.color = Color.blue;
                }
            }
        }
    }
}
