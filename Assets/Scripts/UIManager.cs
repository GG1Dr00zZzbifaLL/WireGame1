using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button MidButton;
    public Button LowButton;
    public Button HighButton;
    public WireCreating wireCreating;
    public GameManager gameManager;

    public Slider BudgetSlider;
    public Slider BudgetSlider2;
    public Slider BudgetSlider3;
    public Text BudgetText;
    public Text BudgetText2;
    public Text BudgetText3;
    public Gradient myGradient;
    public Gradient myGradient2;
    public Gradient myGradient3;

    private int ChangeProvod = 0;


    //изначально выбран малый провод
    private void Start()
    {
        LowButton.onClick.Invoke();
    }

    //перезапуск сцены при нажатии на кнопку 
    public void Restart()
    {
        SceneManager.LoadScene("Level_1");
    }

    //смена проводов при нажатии на их иконки 
    public void ChangeWire(int myWireType)
    {
        ChangeProvod = myWireType;

        if (myWireType == 0)
        {
            LowButton.GetComponent<Outline>().enabled = true;
            MidButton.GetComponent<Outline>().enabled = false;
            HighButton.GetComponent<Outline>().enabled = false;
            wireCreating.WireToInstantiate = wireCreating.LowWire;
            gameManager.AssignWire(myWireType);
        }
        if (myWireType == 1)
        {
            LowButton.GetComponent<Outline>().enabled = false;
            MidButton.GetComponent<Outline>().enabled = true;
            HighButton.GetComponent<Outline>().enabled = false;
            wireCreating.WireToInstantiate = wireCreating.MidWire;
            gameManager.AssignWire(myWireType);
        }
        if (myWireType == 2)
        {
            LowButton.GetComponent<Outline>().enabled = false;
            MidButton.GetComponent<Outline>().enabled = false;
            HighButton.GetComponent<Outline>().enabled = true;
            wireCreating.WireToInstantiate = wireCreating.HighWire;
            gameManager.AssignWire(myWireType);
        }
    }

    //инициализация каждого провода с его слайдером
    public void WiresInitialize(float StartBudget1, float StartBudget2, float StartBudget3)            
    {
        BudgetText.text = Mathf.FloorToInt(StartBudget1).ToString() + "штук";
        BudgetSlider.value = StartBudget1;
        BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);

        BudgetText2.text = Mathf.FloorToInt(StartBudget2).ToString() + "штук";
        BudgetSlider2.value = StartBudget2;
        BudgetSlider2.fillRect.GetComponent<Image>().color = myGradient2.Evaluate(BudgetSlider2.value);

        BudgetText3.text = Mathf.FloorToInt(StartBudget3).ToString() + "штук";
        BudgetSlider3.value = StartBudget3;
        BudgetSlider3.fillRect.GetComponent<Image>().color = myGradient3.Evaluate(BudgetSlider3.value);
    }

    //обновление текущего бюджета у выбраного провода 
    public void UpdateBudgetUI (float CurrentBudget, float LevelBudget)
    {
        if (ChangeProvod == 0)
        {
            BudgetText.text = Mathf.FloorToInt(CurrentBudget).ToString() + "штук";
            BudgetSlider.value = CurrentBudget / LevelBudget;
            BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);
        }
        if (ChangeProvod == 1)
        {
            BudgetText2.text = Mathf.FloorToInt(CurrentBudget).ToString() + "штук";
            BudgetSlider2.value = CurrentBudget / LevelBudget;
            BudgetSlider2.fillRect.GetComponent<Image>().color = myGradient2.Evaluate(BudgetSlider2.value);
        }
        if (ChangeProvod == 2)
        {
            BudgetText3.text = Mathf.FloorToInt(CurrentBudget).ToString() + "штук";
            BudgetSlider3.value = CurrentBudget / LevelBudget;
            BudgetSlider3.fillRect.GetComponent<Image>().color = myGradient3.Evaluate(BudgetSlider3.value);
        }
    }
}