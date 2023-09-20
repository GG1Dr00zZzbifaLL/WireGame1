using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Point : MonoBehaviour
{
    public bool Runtime = true;
    public Vector2 PointID;
    public List<Wire> ConnectedWires;

    //проверка точек на игровом поле
    private void Start()
    {
        if (Runtime == false)
        {
            PointID = transform.position;
            if (GameManager.AllPoints.ContainsKey(PointID) == false) GameManager.AllPoints.Add(PointID, this);
        }   
    }

    //перемещение по клеточам если Runtime отключен
    private void Update()
    {
        if (Runtime == false)
        {
            if (transform.hasChanged == true)
            {
                transform.hasChanged = false;
                transform.position = Vector3Int.RoundToInt(transform.position);
            }
        }
    }

    //смена цвета на чёрный при навидении курсора на точку
    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    //смена цвета на белый при навидении курсора на точку
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}