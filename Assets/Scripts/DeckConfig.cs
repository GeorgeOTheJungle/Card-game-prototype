using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Decks Config", menuName = "Deck config")]
public class DeckConfig : ScriptableObject
{
    [Header("Debug")]
    public int TotalDecksGenerated = 1;

    public Sprite DebugPreviewSprite;
    public Sprite DebugUnitSprite;

    [Space(10)]
    [Header("Cards")]
    public List<CardScriptable> Cards = new List<CardScriptable>();
    [Space(5)]
    public List<Deck> PremadeDecks = new List<Deck>();

    //[ContextMenu("GenerateRandomCards")]
    //public void GenerateRandomCards()
    //{
    //    Cards.Clear();
    //    for (int i = 0; i < TotalCardsGenerated; i++)
    //    {
    //        var card = new CardData();
    //        card.CardName = $"Debug Card {i}";
    //        card.CardDescription = "";

    //        card.CardCost = UnityEngine.Random.Range(0, 5);
    //        card.CardLevel = 1;
    //        card.CardType = (CardData.CardTypes)UnityEngine.Random.Range(0, 4);

    //        float health = UnityEngine.Random.Range(10, 50);
    //        float damage = UnityEngine.Random.Range(1, 15);
    //        float attackSpeed = UnityEngine.Random.Range(0.25f, 1.5f);

    //        float range = UnityEngine.Random.Range(2, 4);
    //        float speed = UnityEngine.Random.Range(1, 5);

    //        float idleTime = UnityEngine.Random.Range(0.25f, 0.75f);
    //        float restTime = UnityEngine.Random.Range(0.35f, 1);
    //        float invokeTime = UnityEngine.Random.Range(0.25f, 1f);

    //        var unitData = new UnitData(TargetTypes.Both, health, damage, attackSpeed, range, speed, idleTime, restTime, invokeTime, card.CardType, card.CardLevel);
    //        card.UnitData = unitData;

    //        card.PreviewSprite = DebugPreviewSprite;
    //        card.CardImage = DebugUnitSprite;
    //        Cards.Add(card.);
    //    }
    //}

    //[ContextMenu("GenerateRandomDeck")]
    //public void GenerateRandomDecks()
    //{
    //    PremadeDecks.Clear();
    //    for (int i = 0; i < TotalDecksGenerated; i++)
    //    {
    //        var cardList = new List<CardData>();
    //        for (int d = 0; d < 8; d++)
    //        {
    //            var card = Cards[UnityEngine.Random.Range(0, Cards.Count)];
    //            cardList.Add(card);
    //        }

    //        var deck = new Deck($"Deck {i}", cardList);
    //        PremadeDecks.Add(deck);
    //    }
    //}

    [ContextMenu("GenerateDeck")]
    public void GenerateDeck()
    {
        PremadeDecks.Clear();
        for (int i = 0; i < TotalDecksGenerated; i++)
        {
            var cardList = new List<CardData>();
            for (int d = 0; d < 8; d++)
            {
                var card = Cards[d];
                cardList.Add(card.CardData);
            }

            var deck = new Deck($"Deck {i}", cardList);
            PremadeDecks.Add(deck);
        }
    }

    public Deck GetDeck(int i = -1)
    {

        if (i == -1)
        {
            return PremadeDecks[UnityEngine.Random.Range(0, PremadeDecks.Count)];
        }
        i = Mathf.Clamp(i, 0, PremadeDecks.Count - 1);

        var deck = PremadeDecks[i].GetDeck();
        return deck;
    }
}

[Serializable]
public struct Deck
{
    public string DeckName;
    public List<CardData> AvailableCards;

    public Deck(string deckName, List<CardData> cards)
    {
        DeckName = deckName;
        AvailableCards = cards;
    }

    public Deck GetDeck()
    {
        var deck = new Deck(DeckName, AvailableCards);
        return deck;
    }
    public void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        AvailableCards = AvailableCards.OrderBy(_ => rng.Next()).ToList();
    }

    public void RemoveCard(CardData cardData)
    {
        AvailableCards.Remove(cardData);
    }

    public void AddCard(CardData cardData)
    {
        AvailableCards.Add(cardData);
    }

    public CardData GetNextCard(CardData exclude = null)
    {
        var card = AvailableCards[0];
        for (int i = 0; i < AvailableCards.Count; i++)
        {
            if (AvailableCards[i] != exclude && AvailableCards[i].InHand == false)
            {
                card = AvailableCards[i];
            }
        }

        RemoveCard(card);
        card.InHand = true;
        return card;
    }
    public List<CardData> GetCards(int amount)
    {
        var cards = new List<CardData>();
        for (int i = 0; i < amount; i++)
        {
            var card = AvailableCards[i];
            card.InHand = true;
            cards.Add(card);

            RemoveCard(card);
        }

        return cards;
    }
}
[Serializable]
public class CardData
{
    public bool InHand;
    public string CardName;
    public string CardDescription;
    [Space]

    public int CardCost;
    public int CardLevel;
    public CardTypes CardType;
    [Space]

    public UnitData UnitData;

    [Space]
    public Sprite CardImage;
    public Sprite PreviewSprite;
    public Animator UnitAnimator;

    public enum CardTypes
    {
        Unit_Light,
        Unit_Medium,
        Unit_Heavy,
        Spell
    }
}

[Serializable]
public class UnitData
{
    public TargetTypes Targets;

    [Space]

    public float Health;
    public float Damage;
    public float AttackSpeed;
    [Space]

    public bool RangedAttacker;
    public float Range;
    public float Speed;
    [Space]

    public float IdleTime;
    public float RestTime;
    public float InvokeTime;

    public UnitData(TargetTypes targets, float health, float damage, float attackSpeed, float range, float speed, float idleTime, float restTime, float invokeTime, CardData.CardTypes cardType, int level)
    {
        Targets = targets;

        float healthPerLevel = level > 1 ? GameManager.Instance.GetHealthPerLevel(cardType) * level : 0;
        Health = health + healthPerLevel;

        float damagePerLevel = level > 1 ? GameManager.Instance.GetDamagePerLevel(cardType) * level : 0;
        Damage = damage + damagePerLevel;
        AttackSpeed = attackSpeed;

        Range = range;
        RangedAttacker = Range >= 1.5f;
        Speed = speed;

        IdleTime = idleTime;
        RestTime = restTime;
        InvokeTime = invokeTime;
    }
}