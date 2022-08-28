using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameCPU : MonoBehaviour
{
    public List<Card> CPUCards = new List<Card>();

    public List<Image> CPUCardImages = new List<Image>();

    public Image Card;

    public RectTransform CardRoot;

    public CardDealer CardDealer;

    public CardGamePlayer CardGamePlayer;

    public Discard Discard;

    public bool IsCPUWaiting = false;

    List<Card> RemovedCards = new List<Card>();

    public void GameStartInitialize()
    {
        RemovePair();
        var CardInstantiated = 0;

        foreach (var cpuCard in CPUCards)
        {
            var card = Instantiate(Card, CardRoot);
            CPUCardImages.Add(card);

            var cardButton = card.gameObject.AddComponent<Button>();

            cardButton.onClick.AddListener(() =>
            {
                if (IsCPUWaiting)
                {
                    return;
                }
                CardGamePlayer.CardChoice(cpuCard);
                CardGameStateManager.Instance.NextState();
            });

            CardInstantiated++;
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(CardInstantiated * 200, 0);
            card.sprite = CardDealer.CardDeck.GetSprite($"Card_54");
            card.name = $"Card_{cpuCard.Number}";
        }
    }

    public void RemovePair()
    {
        List<Card> iCardNumberList = new List<Card>();

        RemovedCards.Clear();

        for (int i = 1; i < 14; i++)
        {
            var count = 0;
            iCardNumberList.Clear();
            foreach (Card card in CPUCards)
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
                RemovedCards.AddRange(iCardNumberList);
                CPUCards.RemoveAll(card => card.Number == i);
                continue;
            }

            var removeCount = 0;
            foreach (Card countCard in iCardNumberList)
            {
                CPUCards.Remove(countCard);
                removeCount++;
                if (removeCount == 2)
                {
                    break;
                }
            }
        }

        foreach (var card in CPUCards)
        {
            Debug.Log($"{card.CardSuit}:{card.Number}");
        }
    }

    public void CardChoice(Card selectCard)
    {
        CardGamePlayer.PlayerCards.Remove(selectCard);
        CardGamePlayer.PlayerCards.RemoveAll(c => c == null);

        foreach (var playerCardImage in CardGamePlayer.PlayerCardImages)
        {
            if (playerCardImage.name == $"Card_{selectCard.Number}")
            {
                Destroy(playerCardImage.gameObject);
            }
        }

        CPUCards.Add(selectCard);

        var card = Instantiate(Card, CardRoot);
        card.name = $"Card_{selectCard.Number}";
        card.sprite = CardDealer.CardDeck.GetSprite($"Card_54");

        var cardButton = card.gameObject.AddComponent<Button>();
        cardButton.onClick.AddListener(() =>
        {
            CardChoice(selectCard);
            CardGameStateManager.Instance.NextState();
        });

        CPUCardImages.Add(card);

        RemovePair();

        if (CPUCards.Count == CPUCardImages.Count)
        {
            return;
        }

        CPUCardImages.RemoveAll(c => c == null);

        var discardCount = 0;

        foreach (var cpuCardImage in CPUCardImages)
        {
            if (cpuCardImage.name == $"Card_{selectCard.Number}")
            {
                cpuCardImage.sprite = CardDealer.CardDeck.GetSprite($"Card_{RemovedCards[discardCount].Num}");
                discardCount++;
                Discard.DiscardCard(cpuCardImage.gameObject);
            }
        }
        CPUCardImages.RemoveAll(c => c.name == $"Card_{selectCard.Number}");
    }
}
