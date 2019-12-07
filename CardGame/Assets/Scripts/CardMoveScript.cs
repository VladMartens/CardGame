using UnityEngine;
using UnityEngine.EventSystems;

// Скрипт управляющий движением карты
public class CardMoveScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private new Camera camera;
    private Vector3 offset;

    public bool isDraggable = true;
    public bool isEnemy;
    public Transform defaultParent;

    void Awake()
    {
        camera = Camera.allCameras[0];
    }

    // Функция срабатывающая при старте передвижения карты
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable == false || isEnemy==true)
            return;
        offset = transform.position - camera.ScreenToWorldPoint(eventData.position);

        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent);
    }

    // Функция срабатывающая при передвижении карты
    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable == false || isEnemy == true)
            return;
        Vector3 newPos = camera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Функция срабатывающая при завершении передвижении карты
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable == false || isEnemy == true)
            return;
        transform.SetParent(defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (gameObject.transform.parent.gameObject.name != "Canvas" && gameObject.transform.parent.gameObject.name != "HandPlayer")
        {
            Field.cardToField.Add(GetComponent<ViewCard>().cardGO);
            isDraggable = false;
            PLayerDeck.playerDeck.Remove(GetComponent<ViewCard>().cardGO);
        }
    }
}