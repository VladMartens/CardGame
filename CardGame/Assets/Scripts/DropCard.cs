using UnityEngine;
using UnityEngine.EventSystems;

// Класс управляющий картой при перемещении на игровое поле
public class DropCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMoveScript card = eventData.pointerDrag.GetComponent<CardMoveScript>();

        if (card)   // проверка на сущестование карты
            if (Field.cardToField.Count == 0)   // есть ли еще карты на игровом поле
                card.defaultParent = transform;
            else if (GetComponentInChildren<ViewCard>() && transform.childCount < 2)    // есть ли еще в ячейке карта
            {
                if (GetComponentInChildren<ViewCard>().viewCard.suit == card.GetComponent<ViewCard>().viewCard.suit &&  // проверка на совместимость масти
                   GetComponentInChildren<ViewCard>().viewCard.number <= card.GetComponent<ViewCard>().viewCard.number) // проверка на достоинство карты
                    card.defaultParent = transform;
            }
            else  
                for (int i = 0; i < Field.cardToField.Count; i++)
                    // проверка на совместимость достоинства карты с другими картами на поле
                    if (Field.cardToField[i].GetComponent<ViewCard>().viewCard.number == card.GetComponent<ViewCard>().viewCard.number && transform.childCount < 2)
                    {
                        card.defaultParent = transform;
                        break;
                    }
    }
}