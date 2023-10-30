using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Point : MonoBehaviour
{
    public WireCreating wireCreating;
    public List<Wire> ConnectedWires;
    public Vector2 PointID;
    public bool Runtime = true;
    
    //получение позиции точки
    private void Start()
    {
        if (Runtime == false)
        {
            PointID = transform.position;
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