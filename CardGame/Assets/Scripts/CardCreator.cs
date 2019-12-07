using UnityEngine;

// Класс управляющий созданием карт 
public class CardCreator : MonoBehaviour
{
    public Transform playerDeck, enemyDeck;
    public GameObject cardEnemyPref, cardPlayerPref, playerHendObj, enemyHendObj;

    // Функция создания карты для игрока
    public void GiveCardPlayer()
    {
        GameObject cardGO = Instantiate(cardPlayerPref, playerDeck, false);
        cardGO.GetComponent<ViewCard>().cardGO = cardGO;
        PLayerDeck.playerDeck.Add(cardGO);
    }

    // Функция создания карты для противника
    public void GiveCardEnemy()
    {
        GameObject cardGO = Instantiate(cardEnemyPref, enemyDeck, false);
        cardGO.GetComponent<ViewCard>().cardGO = cardGO;
        EnemyDeck.enemyDeck.Add(cardGO);       
    }
}