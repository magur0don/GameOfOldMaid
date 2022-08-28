using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGamePlayer : MonoBehaviour
{
    public List<Card> PlayerCards = new List<Card>();

    public List<Image> PlayerCardImages = new List<Image>();

    public Image Card;

    public RectTransform CardRoot;

    public CardDealer CardDealer;

    public CardGameCPU CardGameCPU;

    public Discard Discard;
    public void GameStartInitialize()
    {
        RemovePair();
        var CardInstantiated = 0;

        foreach (var playerCard in PlayerCards)
        {
            var card = Instantiate(Card, CardRoot);
            PlayerCardImages.Add(card);

            CardInstantiated++;
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(CardInstantiated * 200, 0);
            card.sprite = CardDealer.CardDeck.GetSprite($"Card_{playerCard.Num}");
            card.name = $"Card_{playerCard.Number}";
        }
    }


    public void RemovePair()
    {
        List<Card> iCardNumberList = new List<Card>();
        for (int i = 1; i < 14; i++)
        {
            var count = 0;
            iCardNumberList.Clear();
            foreach (Card card in PlayerCards)
            {
                if (i == card.Number)
                {
                    count++;
                    iCardNumberList.Add(card);
                }
            }

            if (count == 1)
            {
                continue;
            }

            if (count % 2 == 0)
            {
                PlayerCards.RemoveAll(card => card.Number == i);
                continue;
            }

            var removeCount = 0;
            foreach (Card countCard in iCardNumberList)
            {
                PlayerCards.Remove(countCard);
                removeCount++;
                if (removeCount == 2)
                {
                    break;
                }
            }
        }

        foreach (var card in PlayerCards)
        {
            Debug.Log($"{card.CardSuit}:{card.Number}");
        }
    }


    public void CardChoice(Card selectCard)
    {
        CardGameCPU.CPUCards.Remove(selectCard);

        foreach (var cpuCardImage in CardGameCPU.CPUCardImages)
        {
            if (cpuCardImage.name == $"Card_{selectCard.Number}")
            {
                Destroy(cpuCardImage.gameObject);
            }
        }
        CardGameCPU.CPUCardImages.RemoveAll(c => c.name == $"Card_{selectCard.Number}");

        PlayerCards.Add(selectCard);

        var card = Instantiate(Card, CardRoot);
        card.sprite = CardDealer.CardDeck.GetSprite($"Card_{selectCard.Num}");
        card.name = $"Card_{selectCard.Number}";

        PlayerCardImages.Add(card);

        if (card.name == $"Card_0")
        {
            return;
        }

        RemovePair();

        if (PlayerCards.Count == PlayerCardImages.Count)
        {
            return;
        }

        // Imageの配列にNullの情報が含まれてしまっているので、それを消す
        PlayerCardImages.RemoveAll(c => c == null);

        // PlayerCardImagesの中にはNullの情報はないので、実体のみの処理を行うことができる
        foreach (var playerCardImage in PlayerCardImages)
        {
            if (playerCardImage.name == $"Card_{selectCard.Number}")
            {
                //Destroy(playerCardImage.gameObject);
                Discard.DiscardCard(playerCardImage.gameObject);
            }
        }

        PlayerCardImages.RemoveAll(c => c.name == $"Card_{selectCard.Number}");
    }
}
