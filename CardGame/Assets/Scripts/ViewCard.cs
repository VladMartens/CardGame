using UnityEngine;
using UnityEngine.UI;

// Класс управляющий изображением карты
public class ViewCard : MonoBehaviour
{
    public Card viewCard;
    public GameObject cardGO;
    public Image imageCard;
    private Sprite spriteCard;

    // функция задающая изображение карты игрока
    public void ShowCard()
    {
        spriteCard = viewCard.sprite;
        imageCard.sprite = viewCard.sprite;
    }

    // функция задающая изображение карты врага
    public void HideCard()
    {
        spriteCard = viewCard.sprite;
        imageCard.sprite = Resources.Load<Sprite>("Sprites/Cards/backCard"); ;
    }

    private void Start()
    {
        viewCard = Deck.allCards[0];
        if (imageCard == null)
            imageCard = GetComponent<Image>();

        if (GetComponent<CardMoveScript>().isEnemy == true)
            HideCard();
        else
            ShowCard();
        Deck.allCards.RemoveAt(0);
    }
}