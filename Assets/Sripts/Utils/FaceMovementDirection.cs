using UnityEngine;

public class FaceMovementDirection : MonoBehaviour
{
    private Vector3 previousPostion;
    private Vector3 moveDirection;//diffrence bitween current position and previous position
    private Quaternion targetRotation;
    private float rotationSpeed = 200;
    void Start()
    {
        previousPostion = transform.position;
    }

    
    void Update()
    {
        previousPostion -= new Vector3(GameManager.Instance.adjustedworldSpeed,0);
        moveDirection = transform.position- previousPostion;

        targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);

        previousPostion = transform.position;
    }
}
