using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LerpObjectPosition : MonoBehaviour
{
    public static LerpObjectPosition instance;
    bool toLerp = false;
    float lerpSpeed;
    float lerpTime;
    
    Vector3 initPos;
    Vector3 finalPos;
    Transform currentObject;

    System.Action lerpComplete;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        if(toLerp == false)
            return;

        currentObject.transform.position = Vector3.Lerp(initPos,finalPos,lerpTime);

        if(lerpTime < 1.0f)
        {
            lerpTime += Time.deltaTime/lerpSpeed;
        }
        else{
            toLerp = false;
            lerpTime = 0;
            if(lerpComplete != null)
            {
                lerpComplete.Invoke();
            }
        }
        
    }
    public void LerpObject(Transform lerpObject, Vector3 _finalPos, float speed, System.Action _lerpComplete = null)
    {
        currentObject = lerpObject;
        initPos = currentObject.transform.position;
        finalPos = _finalPos;
        lerpSpeed = speed;
        lerpTime = 0;
        if(_lerpComplete != null)
            lerpComplete = _lerpComplete;
        else
            lerpComplete = null;

        toLerp = true;
    }
}
