using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMouse : MonoBehaviour
{
    private Camera cam;
    Ray ray;
    public float distance;
    public Vector3 cameraAxis;

    Vector3 newPoint;
    Vector3 defValue;

    RaycastHit hit;

    public Vector3 offset;

    private void OnEnable() {

        Start();
        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        newPoint = ray.GetPoint(distance);

        offset = newPoint - transform.position ;
    }
    void Start()
    {
        cam = Camera.main;
        if(distance == 0)
            distance = Vector3.Distance(cam.GetComponent<Transform>().position,transform.position);
        defValue = transform.position ;
    }
    private void Update() {
        
        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        newPoint = ray.GetPoint(distance);

        if(cameraAxis.x == 1)
        {
            newPoint.x = defValue.x;
        }
        else if(cameraAxis.z == 1) {
            newPoint.z = defValue.z;
        }
        else if(cameraAxis.y ==1){
            newPoint.y = defValue.y;
        }

        transform.position = newPoint - offset;
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset,0.01f);    
    }

}
