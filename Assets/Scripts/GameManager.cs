using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WireCreating wireCreating;
    public UIManager myUIManager;  

    public float LowBudget = 1f;
    public float MidBudget = 2f;
    public float HighBudget = 3f;
    public float CurrentBudget = 0f; 
    
    private float LowBudgetCurrent;  
    private float MidBudgetCurrent;  
    private float HighBudgetCurrent;

    private int CurrentProvod = 0;

    //������������� �������� ������� ��������� ������� 
    private void Awake()
    {
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

        if (CurrentProvod == 0)
        {
            myUIManager.UpdateBudgetUI(CurrentBudget, LowBudget);
        }
        else if (CurrentProvod == 1)
        {
            myUIManager.UpdateBudgetUI(CurrentBudget, MidBudget);
        }
        else if (CurrentProvod == 2)
        {
            myUIManager.UpdateBudgetUI(CurrentBudget, HighBudget);
        }

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