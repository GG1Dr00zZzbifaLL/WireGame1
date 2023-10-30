using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireCreating : MonoBehaviour, IPointerDownHandler
{  
    public GameObject WireToInstantiate;
    public GameObject PointToInstantiate;
    public GameObject HighWire;
    public GameObject MidWire;
    public GameObject LowWire;
    public GameObject EndLogo;
    public GameObject EndButton;
    public GameObject EndRestart;
    public GameObject ResButton;
    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;
    private GameObject StartPoint;
    private GameObject EndPoint;  

    public Transform PointParent;
    public Transform wireParent;
    public List<Vector2> spisokPoint;
    public List<Vector2> spisokPointEnd;
    public Wire CurrentWire;
    public GameManager myGameManager;
    public UIManager myUImanager;
    public Point CurrentEndPoint; 
    public Text ChangeWireText;
    public Text Text;
    public Slider Slider;
    public Gradient Gradient;
    private Point selectedPoint;

    public float tok = 320f;
    public float tok1 = 320f;
    public float MinusCharge = 0f;
    public float maxDistance = 0.3f;
    private int PointsList = 0;

    public bool WireCreationStarted = false;
    public bool isCreate = false;
    public bool isNoProvod = false;
    private bool isOk = false;


    //� ��������� ������� �� ������� ��������� � �������� ����� �� ����� � ��������� �� ������� � ������,
    //� ����� ��������� ���������� ������� � ������������ ������� � ����������� ��������� tok
    public void Start()
    {
        StartPoint = GameObject.FindGameObjectWithTag("StartPoint");
        EndPoint = GameObject.FindGameObjectWithTag("EndPoint");
        spisokPoint.Add(StartPoint.transform.position);
        spisokPointEnd.Add(StartPoint.transform.position);
        Text.text = Mathf.FloorToInt(tok).ToString() + "�";
        Slider.value = tok;
    }

    //������� ��� ������ �������� ������� 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (WireCreationStarted == false)
        {
            if (isCreate)
            {
                WireCreationStarted = true;
                StartWireCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)), 0);
            }
        }
    }

    //������ ��������� �������
    void StartWireCreation(Vector2 StartPosition, int num)
    {
        CurrentWire = Instantiate(WireToInstantiate, wireParent).GetComponent<Wire>();
        CurrentWire.StartPosition = StartPosition;

        //���� num ����� ���� �� �� ������ �������� ����� �� ������ ����� 
        if (num == 0)
        {
            //�������� �� ����������� ������� ����� ���� �� ����� 
            foreach (Vector2 item in spisokPoint)
            {
                if (myGameManager.CurrentBudget > 0 && StartPosition == item)
                {
                    CurrentEndPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent).GetComponent<Point>();
                    isOk = true;
                }
                else 
                {
                    isOk = false;
                }
            }

            if (isOk != true)
            {
                DeleteCurrentWire();
                WireCreationStarted = false;
            }
        }
        //���� num �� ����� ���� �� �� ������ �������� ����� � ��������� � ������
        else
        {
            spisokPointEnd.Add(CurrentEndPoint.transform.position);

            if (myGameManager.CurrentBudget > 0)
            {
                CurrentEndPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent).GetComponent<Point>();
            }
            else
            {
                DeleteCurrentWire();
                WireCreationStarted = false;
            }
        }
        spisokPoint = spisokPointEnd;
    }

    //���������������� �������� �������, ��������� ���� � ������� ����������� ����� ������� �������
    void FinishWireCreation()
    {
        CurrentEndPoint.ConnectedWires.Add(CurrentWire);
        myGameManager.UpdateBudget(CurrentWire.actualCost);
        
        if (tok <= 0)
        {         
            tok = 0;
        }
        else
        {    
            tok -= MinusCharge;
            Text.text = Mathf.FloorToInt(tok).ToString() + "�";

            Slider.value = tok / tok1;
            Slider.fillRect.GetComponent<Image>().color = Gradient.Evaluate(Slider.value);          
        }
        StartWireCreation(CurrentEndPoint.transform.position, 1); 
    }

    //����������� ��������
    public void DeleteCurrentWire()
    {
        if (CurrentWire != null)
        {
            Destroy(CurrentWire.gameObject);  

            if (CurrentEndPoint != null)
            {
                if (CurrentEndPoint.ConnectedWires.Count == 0 && CurrentEndPoint.Runtime == true) Destroy(CurrentEndPoint.gameObject);
            }
        }
    }
    
    //���������� �������
    private void Update()
    {
        //���� ������ ����� ������ �����, �� �� ������ �����
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Collider2D[] hits = Physics2D.OverlapCircleAll(mousePos, maxDistance); 
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Blue") && MinusCharge != 25)
                {
                    DeleteCurrentWire();
                    WireCreationStarted = false;
                }
                if (hit.CompareTag("Yellow") && MinusCharge != 20)
                {
                    DeleteCurrentWire();
                    WireCreationStarted = false;
                }

                if (!isNoProvod) 
                {
                    selectedPoint = hit.GetComponent<Point>(); 
                    break;
                }
            }
        }
        //����� �� ��������� ������, �� ���������� �������� ����� �� ��� ����� � ���� ��� ���, �� �� ����� ������������ ������
        else if (Input.GetMouseButtonUp(0)) 
        {
            if (selectedPoint != null) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                Collider2D[] hits = Physics2D.OverlapCircleAll(mousePos, maxDistance); 
                foreach (Collider2D hit in hits)
                {                  
                    //���� �� ������ �� ���������� ������ ��� �����, �� � ��� ��� �� ��������� 
                    if (hit.CompareTag("Blue") && MinusCharge == 25)
                    {
                        PointsList += 1;
                        Debug.Log(PointsList);
                    }
                    else if (hit.CompareTag("Blue") && MinusCharge != 25)
                    {
                        DeleteCurrentWire();
                        WireCreationStarted = false;
                    }

                    if (hit.CompareTag("Yellow") && MinusCharge == 20)
                    {
                        PointsList += 1;
                        Debug.Log(PointsList);
                    }
                    else if (hit.CompareTag("Yellow") && MinusCharge != 20)
                    {
                        DeleteCurrentWire();
                        WireCreationStarted = false;
                    }
                    
                    //����� ���� ������� ��� �������� ������� 
                    if (!isNoProvod) 
                    {
                        Point point = hit.GetComponent<Point>(); 
                        
                        if (point == selectedPoint) 
                        {
                            if (myGameManager.CanPlaceItem(CurrentWire.actualCost) == true) 
                            {
                                FinishWireCreation();
                                DeleteCurrentWire();
                                WireCreationStarted = false;
                            }
                        }
                        break;
                    }
                }
                selectedPoint = null; 
            }
        }

        //���� ������� �� ��������� �����, �� ������������ �� ���������� ���� �� ���� ������� ����������� ���������� ���� ���� ���������
        if (spisokPoint.Contains(EndPoint.transform.position) && PointsList >= 4)
        {
            if (tok == 0)
            {
                EndRestart.SetActive(true);
                ResButton.SetActive(true);
                isCreate = false;
            }
            if (tok > 0 && tok <= 40)
            {
                EndLogo.SetActive(true);
                EndButton.SetActive(true);
                isCreate = false;
                firstStar.SetActive(true);
            }
            else if (tok > 40 && tok < 90)
            {
                EndLogo.SetActive(true);
                EndButton.SetActive(true);
                isCreate = false;
                firstStar.SetActive(true);
                secondStar.SetActive(true);
            }
            else if (tok >= 90)
            {
                EndLogo.SetActive(true);
                EndButton.SetActive(true);
                isCreate = false;
                firstStar.SetActive(true);
                secondStar.SetActive(true);
                thirdStar.SetActive(true);
            }          
        }
        else if (spisokPoint.Contains(EndPoint.transform.position) && PointsList < 4 || tok == 0)
        {
            EndRestart.SetActive(true);
            ResButton.SetActive(true);
            isCreate = false;
        }

        //��������� ������� ��� ������������ �����
        if (WireCreationStarted == true)
        {
            Vector2 EndPosition = (Vector2)Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 Direction = EndPosition - CurrentWire.StartPosition;
            Vector2 ClampedPosition = CurrentWire.StartPosition + Vector2.ClampMagnitude(Direction, CurrentWire.maxLenght);

            if (Direction == new Vector2(0, 0))
            {
                isNoProvod = true;
            }
            else
            {
                isNoProvod = false;
            }

            if (CurrentWire != null)
            {
                CurrentEndPoint.transform.position = (Vector2)Vector2Int.FloorToInt(ClampedPosition);  
                CurrentEndPoint.PointID = CurrentEndPoint.transform.position;
                CurrentWire.UpdateCreatingWire(CurrentEndPoint.transform.position);  
            }
        }
    }   
}