using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Структура карты
public struct Card
{
    public string name;
    public int suit;
    public int number;
    public Sprite sprite;

    public Card(string name, int suit, int number, Sprite sprite)
    {
        this.name = name;
        this.suit = suit;
        this.number = number;
        this.sprite = sprite;
    }
}

// Статичный класс хранящий список всех карт в колоде
public static class Deck
{
    public static List<Card> allCards = new List<Card>();
}

// Статичный класс хранящий список всех карт в на игровом поле
public static class Field
{
    public static List<GameObject> cardToField = new List<GameObject>();
}

// Статичный класс хранящий список всех карт в руке игрока
public static class PLayerDeck
{
    public static List<GameObject> playerDeck = new List<GameObject>();
}

// Статичный класс хранящий список всех карт в руке противника
public static class EnemyDeck
{
    public static List<GameObject> enemyDeck = new List<GameObject>();
}

// Класс управлящий картами в колоде
public class DeckOfCards : MonoBehaviour
{
    const int _countSuit = 4;
    const int _countCard = 14;
    const int _minNumberCard = 6;

    private Text countCardsOfDeck;
    private Image imageDeck;
    private new Animation animation;

    public Otboi otboi;

    void Start()
    {
        animation = GetComponent<Animation>();

        StartCoroutine("StartGame");

        countCardsOfDeck = GetComponentInChildren<Text>();
        imageDeck = GetComponent<Image>();

        // Создание всех карт и добавление их в лист "колода"
        for (int i = 1; i <= _countSuit; i++) 
            for (int j = _minNumberCard; j <= _countCard; j++)
                Deck.allCards.Add(new Card(string.Format("{0}.{1}", i, j), i, j, Resources.Load<Sprite>(string.Format("Sprites/Cards/{0}.{1}", i, j))));
        Shuffle();
    }

    // Функция перемешивания карт
    private void Shuffle()
    {
        List<Card> list = new List<Card>();
        var random = new System.Random();
        for (int i = Deck.allCards.Count - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            var temp = Deck.allCards[j];
            Deck.allCards[j] = Deck.allCards[i];
            Deck.allCards[i] = temp;
        }
    }

    // Корутин срабатывающий при старте игры
    // Служит для выдачи карт игрокам
    IEnumerator StartGame()
    {
        for (int i = 12; i > 0;)
        {
            if (animation.IsPlaying("GiveCardToEnemy") == false && i % 2 == 0)
            {
                StrartAnimGiveCard("GiveCardToPlayer");
                i--;
            }
            if (animation.IsPlaying("GiveCardToPlayer") == false && i % 2 != 0)
            {
                StrartAnimGiveCard("GiveCardToEnemy");
                i--;
            }
            yield return 2;
        }
    }

    // Корутин срабатывающий после отбоя
    // Выдает всем игрокам карты 
    IEnumerator GiveCardPlayers()
    {
        for (int i = PLayerDeck.playerDeck.Count; i < 6;)
        {
            if (Deck.allCards.Count == 0)
            {
                imageDeck.color = new Color(imageDeck.color.r, imageDeck.color.g, imageDeck.color.b, 0);
                break;
            }
            if (animation.IsPlaying("GiveCardToPlayer") == false)
            {
                StrartAnimGiveCard("GiveCardToPlayer");
                i++;
            }
            yield return 2;
        }

        for (int i = EnemyDeck.enemyDeck.Count; i < 6;)
        {
            if (Deck.allCards.Count == 0)
            {
                imageDeck.color = new Color(imageDeck.color.r, imageDeck.color.g, imageDeck.color.b, 0);
                break;
            }
            if (animation.IsPlaying("GiveCardToEnemy") == false)
            {
                StrartAnimGiveCard("GiveCardToEnemy");
                i++;
            }
            yield return 2;
        }
    }

    // Функция запускающая анимацию 
    private void StrartAnimGiveCard(string nameAnimation)
    {
        animation.PlayQueued(nameAnimation, QueueMode.PlayNow);
        GetComponentInChildren<Text>().text = string.Format("{0}", int.Parse(GetComponentInChildren<Text>().text) - 1);
    }

    // Фунция меняющая карты игроков местами
    public void SwipePlayers()
    {
        for(int i = 0; i < PLayerDeck.playerDeck.Count; i++)
        {
            PLayerDeck.playerDeck[i].transform.SetParent(GetComponent<CardCreator>().enemyDeck);
            PLayerDeck.playerDeck[i].GetComponent<ViewCard>().HideCard();
            PLayerDeck.playerDeck[i].GetComponent<CardMoveScript>().isEnemy = true;
        }
        for(int i =0;i<EnemyDeck.enemyDeck.Count;i++)
        {
            EnemyDeck.enemyDeck[i].transform.SetParent(GetComponent<CardCreator>().playerDeck);
            EnemyDeck.enemyDeck[i].GetComponent<ViewCard>().ShowCard();
            EnemyDeck.enemyDeck[i].GetComponent<CardMoveScript>().isEnemy = false;
        }
        List<GameObject> tmp = new List<GameObject>();
        tmp.AddRange(PLayerDeck.playerDeck);
        PLayerDeck.playerDeck.Clear();
        PLayerDeck.playerDeck.AddRange(EnemyDeck.enemyDeck);
        EnemyDeck.enemyDeck.Clear();
        EnemyDeck.enemyDeck.AddRange(tmp);
    }    
}