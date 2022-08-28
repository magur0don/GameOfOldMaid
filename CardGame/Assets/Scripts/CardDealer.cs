using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class CardDealer : MonoBehaviour
{
    public Cards Cards;

    public SpriteAtlas CardDeck;
    public CardGamePlayer CardGamePlayer;
    public CardGameCPU CardGameCPU;


    public void CardDeal()
    {
        for (int i = 0; i < 26; i++)
        {
            CardGamePlayer.PlayerCards.Add(Cards.GetCard());
            CardGameCPU.CPUCards.Add(Cards.GetCard());
        }
        if (Random.Range(0, 2) == 0)
        {
            CardGamePlayer.PlayerCards.Add(Cards.GetCard());
        }
        else
        {
            CardGameCPU.CPUCards.Add(Cards.GetCard());
        }
    }

}
