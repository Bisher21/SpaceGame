using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    float BackGroundImageWidth;
    
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        BackGroundImageWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    
    void Update()
    {
        float movex = GameManager.Instance.worldSpeed  * Time.deltaTime;
        transform.position+= new Vector3(movex, 0);
        if (Mathf.Abs(transform.position.x)- BackGroundImageWidth > 0)
        {
            transform.position = new Vector3(0,transform.position.y);
        }
    }
}
