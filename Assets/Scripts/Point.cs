using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Point : MonoBehaviour
{
    public WireCreating wireCreating;
    public List<Wire> ConnectedWires;
    public Vector2 PointID;
    public bool Runtime = true;
    
    //��������� ������� �����
    private void Start()
    {
        if (Runtime == false)
        {
            PointID = transform.position;
        }   
    }

    //����������� �� �������� ���� Runtime ��������
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

    //����� ����� �� ������ ��� ��������� ������� �� �����
    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    //����� ����� �� ����� ��� ��������� ������� �� �����
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}