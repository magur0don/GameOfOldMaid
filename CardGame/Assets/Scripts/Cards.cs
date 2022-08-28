using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Cards : MonoBehaviour
{
    // Cardのリスト(今の時点ではまだ何もトランプはない)
    public List<Card> DeckOfCards = new List<Card>();

    private void Start()
    {
        DeckOfCards.Clear();

        for (int i = 0; i < 52; i++)
        {
            Card card = new Card(CardSuitJudge(i), CardNumJudge(i), i);

            DeckOfCards.Add(card);
        }

        SetOneJoker();

        ShuffleCards();
    }


    public void SetOneJoker()
    {
        DeckOfCards.Add(new Card(Card.Suit.Invalid, 0, 52));
    }

    public void ShuffleCards()
    {
        DeckOfCards = DeckOfCards.OrderBy(card => Guid.NewGuid()).ToList();
        // intの最大値21億くらいなので、桁数がオーバー
    }

    public Card GetCard()
    {
        // 
        var card = DeckOfCards.FirstOrDefault();
        DeckOfCards.Remove(card);
        return card;
    }

    public int CardNumJudge(int num)
    {

        for (int i = 0; i < 13; i++)
        {
            if (num % 13 == i)
            {
                return i + 1;
            }
        }
        return 0;
    }

    public Card.Suit CardSuitJudge(int num)
    {
        for (int i = 0; i < (int)Card.Suit.Max; i++)
        {
            if (num / 13 == i)
            {
                return (Card.Suit)i;
            }
        }
        return Card.Suit.Invalid;
    }
}

public class Card
{
    public enum Suit
    {
        Invalid = -1,
        Club,
        Dia,
        Heart,
        Spade,
        Max
    }

    public Suit CardSuit = Suit.Invalid;
    public int Number = 0;
    public int Num = 0;

    public Card(Suit suit, int number, int num)
    {
        CardSuit = suit;
        Number = number;
        Num = num;
    }
}

