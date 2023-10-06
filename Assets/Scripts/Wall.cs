using UnityEngine;

public class Wall : MonoBehaviour
{
    public WireCreating wireCreating;
    
    //����������� ������� ��� ��������� ������� �� BoxCollider2D
    private void OnMouseEnter()
    {
        wireCreating.DeleteCurrentWire();
        wireCreating.WireCreationStarted = false;
    }

    //����������� ������� ��� ������� ������ �� BoxCollider2D
    private void OnMouseDown()
    {
        wireCreating.DeleteCurrentWire();
        wireCreating.WireCreationStarted = false;
    }
} 