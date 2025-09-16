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
    public int TotalCardsGenerated = 8;
    public int TotalDecksGenerated = 1;

    [Space(10)]
    [Header("Cards")]
    public List<CardData> Cards = new List<CardData>();
    [Space(5)]
    public List<Deck> PremadeDecks = new List<Deck>();

    [ContextMenu("GenerateRandomCards")]
    public void GenerateRandomCards()
    {
        Cards.Clear();
        for (int i = 0; i < TotalCardsGenerated; i++)
        {
            var card = new CardData();
            card.CardName = $"Debug Card {i}";
            card.CardDescription = "";

            card.CardCost = UnityEngine.Random.Range(0, 5);
            card.CardLevel = 1;
            card.CardType = (CardData.CardTypes)UnityEngine.Random.Range(0, 4);

            float health = UnityEngine.Random.Range(10, 50);
            float damage = UnityEngine.Random.Range(1, 15);
            float attackSpeed = UnityEngine.Random.Range(0.25f, 1.5f);

            float range = UnityEngine.Random.Range(1, 3);
            float speed = UnityEngine.Random.Range(1, 5);

            float restTime = UnityEngine.Random.Range(0.35f, 1);
            float invokeTime = UnityEngine.Random.Range(0.25f, 1f);

            var unitData = new UnitData(TargetTypes.UnitsOnly, health, damage, attackSpeed, range, speed, restTime, invokeTime, card.CardType, card.CardLevel);
            card.UnitData = unitData;

            Cards.Add(card);
        }
    }

    [ContextMenu("GenerateRandomDeck")]
    public void GenerateRandomDecks()
    {
        PremadeDecks.Clear();
        for (int i = 0; i < TotalDecksGenerated; i++)
        {
            var cardList = new List<CardData>();
            for (int d = 0; d < 8; d++)
            {
                var card = Cards[UnityEngine.Random.Range(0, Cards.Count)];
                cardList.Add(card);
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
        return PremadeDecks[i];
    }
}

[Serializable]
public class Deck
{
    public string DeckName;
    public List<CardData> AvailableCards;

    public Deck(string deckName, List<CardData> cards)
    {
        DeckName = deckName;
        AvailableCards = cards;
    }

    public void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        AvailableCards = AvailableCards.OrderBy(_ => rng.Next()).ToList();
    }
}
[Serializable]
public class CardData
{
    public string CardName;
    public string CardDescription;

    public int CardCost;
    public int CardLevel;
    public CardTypes CardType;

    public UnitData UnitData;

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

    public float Range;
    public float Speed;
    [Space]

    public float RestTime;
    public float InvokeTime;

    public UnitData(TargetTypes targets, float health, float damage, float attackSpeed, float range, float speed, float restTime, float invokeTime, CardData.CardTypes cardType, int level)
    {
        Targets = targets;

        float healthPerLevel = level > 1 ? GameManager.Instance.GetHealthPerLevel(cardType) * level : 0;
        Health = health + healthPerLevel;

        float damagePerLevel = level > 1 ? GameManager.Instance.GetDamagePerLevel(cardType) * level : 0;
        Damage = damage + damagePerLevel;
        AttackSpeed = attackSpeed;

        Range = range;
        Speed = speed;

        RestTime = restTime;
        InvokeTime = invokeTime;
    }
}