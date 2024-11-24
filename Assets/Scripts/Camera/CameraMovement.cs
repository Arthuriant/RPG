using Unity.Mathematics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{     
    public Transform target;
    public float something;
    public Vector2 maxPotition;
    public Vector2 minPotition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x,target.position.y,transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,minPotition.x,maxPotition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,minPotition.y,maxPotition.y);
            transform.position = Vector3.Lerp(transform.position,targetPosition, something);
        }
    }
}
