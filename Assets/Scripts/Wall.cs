using UnityEngine;

public class Wall : MonoBehaviour
{
    public WireCreating wireCreating;
    
    //уничтожение провода при навидении курсора на BoxCollider2D
    private void OnMouseEnter()
    {
        wireCreating.DeleteCurrentWire();
        wireCreating.WireCreationStarted = false;
    }

    //уничтожение провода при нажатии мышкой на BoxCollider2D
    private void OnMouseDown()
    {
        wireCreating.DeleteCurrentWire();
        wireCreating.WireCreationStarted = false;
    }
} 