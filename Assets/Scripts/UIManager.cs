using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button MidButton;
    public Button LowButton;
    public Button HighButton;
    public WireCreating wireCreating;

    public Slider BudgetSlider;
    public Text BudgetText;
    public Gradient myGradient;

    //изначально выбран средний провод
    private void Start()
    {
        LowButton.onClick.Invoke();
    }

    //перезапуск сцены при нажатии на кнопку 
    public void Restart()
    {
        SceneManager.LoadScene("Level_1");
    }

    //смена проводов 
    public void ChangeWire(int myWireType)
    {
        if (myWireType == 0)
        {
            LowButton.GetComponent<Outline>().enabled = true;
            MidButton.GetComponent<Outline>().enabled = false;
            HighButton.GetComponent<Outline>().enabled = false;
            wireCreating.WireToInstantiate = wireCreating.LowWire;
        }
        if (myWireType == 1)
        {
            LowButton.GetComponent<Outline>().enabled = false;
            MidButton.GetComponent<Outline>().enabled = true;
            HighButton.GetComponent<Outline>().enabled = false;
            wireCreating.WireToInstantiate = wireCreating.MidWire;
        }
        if (myWireType == 2)
        {
            LowButton.GetComponent<Outline>().enabled = false;
            MidButton.GetComponent<Outline>().enabled = false;
            HighButton.GetComponent<Outline>().enabled = true;
            wireCreating.WireToInstantiate = wireCreating.HighWire;
        }
    }
    
    //обновление текущего бюджета 
    public void UpdateBudgetUI (float CurrentBudget, float LevelBudget)
    {
        BudgetText.text = Mathf.FloorToInt(CurrentBudget).ToString() + "m";
        BudgetSlider.value = CurrentBudget / LevelBudget;
        BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);
    }
}