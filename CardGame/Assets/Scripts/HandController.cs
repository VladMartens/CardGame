using UnityEngine;
using UnityEngine.UI;

// Класс управляющий картами в руке
public class HandController : MonoBehaviour
{
    private int lastCountCard;  
    private HorizontalLayoutGroup horizontalLG;

    void Start()
    {
        horizontalLG = GetComponent<HorizontalLayoutGroup>();
    }

    void Update()
    {
        if (lastCountCard != transform.childCount)
        {
            lastCountCard = transform.childCount;
            SetSpacing();
        }
    }

    // Функция выравнивающая карты в руке
    public void SetSpacing()
    {
        if (lastCountCard * -8 <= -72)
            horizontalLG.spacing = -72; 
        else
            horizontalLG.spacing = lastCountCard * -8;

        if (lastCountCard > 6)
            horizontalLG.childControlWidth = true;
        else
            horizontalLG.childControlWidth = false;
    }
}