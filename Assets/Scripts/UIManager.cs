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
    public Text wireText;
    public Gradient myGradient;
    public Gradient myGradient2;
    public Gradient myGradient3;

    private int ChangeProvod = -1;


    //���������� ����� ��� ������� �� ������ 
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //������� �� ��������� �������
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //�������� �� ���������� ������� � ui
    public void ChangeWire(int myWireType)
    {
        if (ChangeProvod == myWireType && wireCreating.isCreate == true)
        {
            myWireType = -1;
        }
        else
        {
            ChangeProvod = myWireType;    
        }

        //����� �������� ��� ������� �� �� ������ 
        switch (myWireType)
        {
            case 0:

                if (wireCreating.WireCreationStarted == true)
                {
                    wireCreating.DeleteCurrentWire();
                    wireCreating.WireCreationStarted = false;
                }
                wireText.gameObject.SetActive(false);
                BudgetSlider.gameObject.SetActive(true);
                BudgetSlider2.gameObject.SetActive(false);
                BudgetSlider3.gameObject.SetActive(false);
                LowButton.GetComponent<Outline>().enabled = true;
                MidButton.GetComponent<Outline>().enabled = false;
                HighButton.GetComponent<Outline>().enabled = false;
                wireCreating.isCreate = true;
                wireCreating.WireToInstantiate = wireCreating.LowWire;
                gameManager.AssignWire(myWireType);
                wireCreating.MinusCharge = 30;
                break;

            case 1:

                if (wireCreating.WireCreationStarted == true)
                {
                    wireCreating.DeleteCurrentWire();
                    wireCreating.WireCreationStarted = false;
                }
                wireText.gameObject.SetActive(false);
                BudgetSlider.gameObject.SetActive(false);
                BudgetSlider2.gameObject.SetActive(true);
                BudgetSlider3.gameObject.SetActive(false);
                LowButton.GetComponent<Outline>().enabled = false;
                MidButton.GetComponent<Outline>().enabled = true;
                HighButton.GetComponent<Outline>().enabled = false;
                wireCreating.isCreate = true;
                wireCreating.WireToInstantiate = wireCreating.MidWire;
                gameManager.AssignWire(myWireType);
                wireCreating.MinusCharge = 25;
                break;

            case 2:

                if (wireCreating.WireCreationStarted == true)
                {
                    wireCreating.DeleteCurrentWire();
                    wireCreating.WireCreationStarted = false;
                }
                wireText.gameObject.SetActive(false);
                BudgetSlider.gameObject.SetActive(false);
                BudgetSlider2.gameObject.SetActive(false);
                BudgetSlider3.gameObject.SetActive(true);
                LowButton.GetComponent<Outline>().enabled = false;
                MidButton.GetComponent<Outline>().enabled = false;
                HighButton.GetComponent<Outline>().enabled = true;
                wireCreating.isCreate = true;
                wireCreating.WireToInstantiate = wireCreating.HighWire;
                gameManager.AssignWire(myWireType);
                wireCreating.MinusCharge = 20;
                break;

            default:
                wireText.gameObject.SetActive(true);
                BudgetSlider.gameObject.SetActive(false);
                BudgetSlider2.gameObject.SetActive(false);
                BudgetSlider3.gameObject.SetActive(false);
                LowButton.GetComponent<Outline>().enabled = false;
                MidButton.GetComponent<Outline>().enabled = false;
                HighButton.GetComponent<Outline>().enabled = false;
                wireCreating.isCreate = false;
                wireCreating.WireCreationStarted = false;
                wireCreating.DeleteCurrentWire();
                break;
        }
    }

    //������������� ������� ������� � ��� ���������
    public void WiresInitialize(float StartBudget1, float StartBudget2, float StartBudget3)            
    {  
        BudgetText.text = Mathf.FloorToInt(StartBudget1).ToString() + "��.";     
        BudgetSlider.value = StartBudget1;
        BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);

        BudgetText2.text = Mathf.FloorToInt(StartBudget2).ToString() + "��.";
        BudgetSlider2.value = StartBudget2;
        BudgetSlider2.fillRect.GetComponent<Image>().color = myGradient2.Evaluate(BudgetSlider2.value);

        BudgetText3.text = Mathf.FloorToInt(StartBudget3).ToString() + "��.";
        BudgetSlider3.value = StartBudget3;
        BudgetSlider3.fillRect.GetComponent<Image>().color = myGradient3.Evaluate(BudgetSlider3.value);
    }

    //���������� �������� ������� � ��������� ������� 
    public void UpdateBudgetUI (float CurrentBudget, float LevelBudget)
    {
        if (ChangeProvod == 0)
        {          
            BudgetText.text = Mathf.FloorToInt(CurrentBudget).ToString() + "��.";           
            BudgetSlider.value = CurrentBudget / LevelBudget;
            BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);
        }

        if (ChangeProvod == 1)
        {
            BudgetText2.text = Mathf.FloorToInt(CurrentBudget).ToString() + "��.";          
            BudgetSlider2.value = CurrentBudget / LevelBudget;
            BudgetSlider2.fillRect.GetComponent<Image>().color = myGradient2.Evaluate(BudgetSlider2.value);
        }

        if (ChangeProvod == 2)
        {
            BudgetText3.text = Mathf.FloorToInt(CurrentBudget).ToString() + "��.";     
            BudgetSlider3.value = CurrentBudget / LevelBudget;
            BudgetSlider3.fillRect.GetComponent<Image>().color = myGradient3.Evaluate(BudgetSlider3.value);
        }
    }
}