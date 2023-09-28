using UnityEngine;

public class Wire : MonoBehaviour
{
    public float Cost = 1f;
    public float actualCost;
    public float maxLenght = 1f;
    public Vector2 StartPosition;
    public SpriteRenderer wireSpriteRender;
    
    //удлинение провода 
    public void UpdateCreatingWire(Vector2 ToPosition)
    {
        transform.position = (ToPosition + StartPosition) / 2;

        Vector2 direction = ToPosition - StartPosition;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float Length = direction.magnitude;
        wireSpriteRender.size = new Vector2(Length, wireSpriteRender.size.y);

        actualCost = Cost;    
    }
}