using UnityEngine;
using UnityEngine.EventSystems;

public class WireCreating : MonoBehaviour, IPointerDownHandler
{
    public bool WireCreationStarted = false;
    public Wire CurrentWire;
    public GameManager myGameManager;
    public GameObject WireToInstantiate;
    public GameObject PointToInstantiate;
    public GameObject HighWire;
    public GameObject MidWire;
    public GameObject LowWire;
    public Point CurrentStartPoint;
    public Point CurrentEndPoint; 
    public Transform PointParent;
    public Transform wireParent;

    public bool isCreate = false;

    //обработка нажатий кнопок 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (WireCreationStarted == false)
        {
            if (isCreate)
            {
                WireCreationStarted = true;
                StartWireCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)));
            }
        }
        else
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {             
                if (myGameManager.CanPlaceItem(CurrentWire.actualCost) == true)
                {
                    FinishWireCreation();                    
                    DeleteCurrentWire();
                    WireCreationStarted = false;
                }               
            }
        }
    }

    //начало появление провода
    void StartWireCreation(Vector2 StartPosition)
    {
        CurrentWire = Instantiate(WireToInstantiate, wireParent).GetComponent<Wire>();
        CurrentWire.StartPosition = StartPosition;

        if (GameManager.AllPoints.ContainsKey(StartPosition))
        {
            if (CurrentStartPoint != CurrentEndPoint)
            {
                CurrentStartPoint = GameManager.AllPoints[StartPosition];
            }
            else
            {
                CurrentStartPoint = null;
            }
        }
        else 
        {
            CurrentStartPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent).GetComponent<Point>();
            GameManager.AllPoints.Add(StartPosition, CurrentStartPoint);
        }

        CurrentStartPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent).GetComponent<Point>();
        CurrentEndPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent).GetComponent<Point>();             
    }

    //непосредственное создание провода 
    void FinishWireCreation()
    {
        if (GameManager.AllPoints.ContainsKey(CurrentEndPoint.transform.position))
        {
            Destroy(CurrentEndPoint.gameObject);
            CurrentEndPoint = GameManager.AllPoints[CurrentEndPoint.transform.position];
        }
        else
        {
            GameManager.AllPoints.Add(CurrentEndPoint.transform.position, CurrentEndPoint);
        }

        CurrentStartPoint.ConnectedWires.Add(CurrentWire);
        CurrentEndPoint.ConnectedWires.Add(CurrentWire);
        
        myGameManager.UpdateBudget(CurrentWire.actualCost);                       
        
        StartWireCreation(CurrentEndPoint.transform.position);
    }

    //уничтожение проводов
    void DeleteCurrentWire()
    {
        Destroy(CurrentWire.gameObject);
        if (CurrentStartPoint.ConnectedWires.Count == 0 && CurrentStartPoint.Runtime == true) Destroy(CurrentStartPoint.gameObject);
        if (CurrentEndPoint.ConnectedWires.Count == 0 && CurrentEndPoint.Runtime == true) Destroy(CurrentEndPoint.gameObject);
    }

    //обновление позиции
    private void Update()
    {
        if (WireCreationStarted == true)
        {
            Vector2 EndPosition = (Vector2)Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 Direction = EndPosition - CurrentWire.StartPosition;
            Vector2 ClampedPosition = CurrentWire.StartPosition + Vector2.ClampMagnitude(Direction, CurrentWire.maxLenght);

            CurrentEndPoint.transform.position = (Vector2)Vector2Int.FloorToInt(ClampedPosition);
            CurrentEndPoint.PointID = CurrentEndPoint.transform.position;
            CurrentWire.UpdateCreatingWire(CurrentEndPoint.transform.position);
        }
    }
}