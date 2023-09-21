using UnityEngine;
using UnityEngine.EventSystems;

public class WireCreating : MonoBehaviour, IPointerDownHandler
{
    bool WireCreationStarted = false;
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

    //обработка нажатий кнопок 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (WireCreationStarted == false)
        {
            WireCreationStarted = true;
            StartWireCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)));
        }
        else
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {             
                if (myGameManager.CanPlaceItem(CurrentWire.actualCost) == true)
                {
                    FinishWireCreation();
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
            CurrentStartPoint = GameManager.AllPoints[StartPosition];
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