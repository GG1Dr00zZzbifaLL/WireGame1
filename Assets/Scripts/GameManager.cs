using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float LowBudget = 10f;
    public float CurrentBudget = 0f;
    public UIManager myUIManager;
    public static Dictionary<Vector2, Point> AllPoints = new Dictionary<Vector2, Point>();

    //�������� ����
    private void Awake()
    {
        AllPoints.Clear();
        Time.timeScale = 0;

        CurrentBudget = LowBudget;                                     //
        myUIManager.UpdateBudgetUI(CurrentBudget, LowBudget);         //
    }

    //�������� ��� ��������� ���������� ������� ����� ������ ��� � ��� ���� � ������   //
    public bool CanPlaceItem(float itemCost)
    {
        if (itemCost > CurrentBudget) return false;
        else return true;
    }

    //��������� �� ������� ����� �� ������� �� ��������� ������ //
    public void UpdateBudget(float itemCost)
    {
        CurrentBudget -= itemCost;
        myUIManager.UpdateBudgetUI(CurrentBudget, LowBudget);
    }
}