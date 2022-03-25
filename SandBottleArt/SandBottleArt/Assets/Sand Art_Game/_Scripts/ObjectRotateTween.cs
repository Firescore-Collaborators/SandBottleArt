using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class ObjectRotateTween : MonoBehaviour
{
    public float speed1;
    public float speed2;
    public float lerpSpeed;
    public AnimationCurve curve;    
    public float angle;    

    public Transform target;
    public DefaultGameEvent rotateEnd;

    Vector3 initPos;
    Quaternion initRot;

    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    [Button]
    public void LerpObject()
    {
        Timer.Delay(2.0f,()=>
        {
            CameraController.instance.SetCurrentCamera(4);
            LerpObjectPosition.instance.LerpObject(transform,target.position,lerpSpeed,()=>
            {
                Rotate();
            });
            LerpObjectRotation.instance.LerpObject(transform,target.rotation,lerpSpeed);
        });
        
    }


    [Button]
    void LerpToDefault()
    {
        LerpObjectPosition.instance.LerpObject(transform,initPos,lerpSpeed);
       
        LerpObjectRotation.instance.LerpObject(transform,initRot,lerpSpeed);
    }

    [Button]
    public void Rotate()
    {
        /*transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x,angle,0),speed1,RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

        transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x,angle,0),speed,RotateMode.LocalAxisAdd).SetEase(curve).SetDelay(speed1).onComplete += ()=>
        {
            rotateEnd.Raise();
        };
        */
        transform.LeanRotateAroundLocal(Vector3.up,angle,speed1).setEase(LeanTweenType.linear);
        transform.LeanRotateAroundLocal(Vector3.up,angle,speed2).setEase(curve).setDelay(speed1).setOnComplete(()=>
        {
            rotateEnd.Raise();
            LerpToDefault();
            CameraController.instance.SetCurrentCamera(0);
        });

    }
}
