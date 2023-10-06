using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float LowBudget = 10f;
    public float MidBudget = 6f;
    public float HighBudget = 3f;
    public float CurrentBudget = 0f;
    public UIManager myUIManager;
    public WireCreating wireCreating;
    public static Dictionary<Vector2, Point> AllPoints = new Dictionary<Vector2, Point>();
    

    private float LowBudgetCurrent;  
    private float MidBudgetCurrent;  
    private float HighBudgetCurrent;
    private int CurrentProvod = 0;


    //�������� ���� � ������������� �������� ������� ��������� ������� 
    private void Awake()
    {
        AllPoints.Clear();
        Time.timeScale = 0;

        LowBudgetCurrent = LowBudget;
        MidBudgetCurrent = MidBudget;
        HighBudgetCurrent = HighBudget;

        myUIManager.WiresInitialize(LowBudget, MidBudget, HighBudget);
    }

    //������������� ������� ����� � ��� �������    
    public void AssignWire(int CurrentProvod)
    {
        this.CurrentProvod = CurrentProvod;
        
        if (CurrentProvod == 0)
        {
            CurrentBudget = LowBudgetCurrent;
        }
        else if (CurrentProvod == 1)
        {
            CurrentBudget = MidBudgetCurrent;
        }
        else if (CurrentProvod == 2)
        {
            CurrentBudget = HighBudgetCurrent;
        }
    }

    //�������� ��� ��������� ���������� ������� ����� ������ ��� � ��� ���� � ������   
    public bool CanPlaceItem(float itemCost)
    {
        if (itemCost > CurrentBudget) return false;
        else return true;
    }

    //��������� �� ������� ����� �� ������� �� ��������� ��� ��� ���� ������ 
    public void UpdateBudget(float itemCost)
    {
        CurrentBudget -= itemCost;
        myUIManager.UpdateBudgetUI(CurrentBudget, LowBudget);
      
        if (CurrentProvod == 0)
        {
            LowBudgetCurrent -= itemCost;
        }
        else if (CurrentProvod == 1)
        {
            MidBudgetCurrent -= itemCost;
        }
        else if (CurrentProvod == 2)
        {
            HighBudgetCurrent -= itemCost;
        }
    }
}