using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyTarget : MonoBehaviour
{
    [SerializeField]
    private string targetName="";
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float distance = 0.5f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
      
      RaycastHit2D hit2D=  Physics2D.Raycast(transform.position,direction==Direction.Left?Vector2.left:Vector2.right, distance, mask);
      if(hit2D)
        targetName = hit2D.collider.name;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
    }
}
public enum Direction
{ 
Left,
Right
}