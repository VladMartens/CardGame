using UnityEngine;
using UnityEngine.UI;

// Класс управляющий отбоем
public class Otboi : MonoBehaviour
{
    private const int _countCardInHend = 6;

    private bool first = true;
    private new Animation animation;

    public Image imageOtboi;
    public DeckOfCards deckOfCards;
    public CardCreator cardCreator;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Функция запускающая анимацию
    public void StartAnim(string nameAnim)
    {
        animation.PlayQueued(nameAnim, QueueMode.PlayNow);
    }

    // Функция отображения отбоя и выдающащая карты игрокам
    public void ShowOtboiAndGiveCard()
    {
        if (first == true)
        {
            imageOtboi.color = new Color(imageOtboi.color.r, imageOtboi.color.g, imageOtboi.color.b, 1);
            first = false;
        }
        for (int i = 0; i < Field.cardToField.Count; i++)
            Destroy(Field.cardToField[i]);
        Field.cardToField.Clear();
        deckOfCards.StartCoroutine("GiveCardPlayers");
    }

    // Функция убирающая карты из стола в руку игроку
    public void PickUpCardsFromTable()
    {

        for (int i = 0; i < Field.cardToField.Count; i++)
        {
            Field.cardToField[i].transform.SetParent(cardCreator.playerDeck);
            Field.cardToField[i].GetComponent<CardMoveScript>().isDraggable = true;
        }
        PLayerDeck.playerDeck.AddRange(Field.cardToField);
        Field.cardToField.Clear();
        deckOfCards.StartCoroutine("GiveCardPlayers");
    }
}