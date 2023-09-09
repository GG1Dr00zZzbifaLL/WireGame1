using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float LevelBudget = 1000f;
    public float CurrentBudget = 0f;
    public UIManager myUIManager;
    public static Dictionary<Vector2, Point> AllPoints = new Dictionary<Vector2, Point>();

    //очищение поля
    private void Awake()
    {
        AllPoints.Clear();
        Time.timeScale = 0;
        CurrentBudget = LevelBudget;
        myUIManager.UpdateBudgetUI(CurrentBudget, LevelBudget);
    }

    //проверка что стоимость проложения провода стоит больше чем у нас есть в запасе
    public bool CanPlaceItem(float itemCost)
    {
        if (itemCost > CurrentBudget) return false;
        else return true;
    }

    //отнимание из бюджета сумму на которую мы проложили провод 
    public void UpdateBudget(float itemCost)
    {
        CurrentBudget -= itemCost;
        myUIManager.UpdateBudgetUI(CurrentBudget, LevelBudget);
    }
}