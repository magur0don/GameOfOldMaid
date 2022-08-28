using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Cards : MonoBehaviour
{
    // Card�̃��X�g(���̎��_�ł͂܂������g�����v�͂Ȃ�)
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
        // int�̍ő�l21�����炢�Ȃ̂ŁA�������I�[�o�[
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

