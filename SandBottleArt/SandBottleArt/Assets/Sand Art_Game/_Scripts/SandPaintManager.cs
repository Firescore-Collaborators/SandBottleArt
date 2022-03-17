using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

[System.Serializable]
public class SandPaintStep
{
    public float endValue;
    public float timeDelay;
    public bool completed;
}

[System.Serializable]
public class CounterStep
{
    public  P3dChangeCounter counter;
    public  float count;

    public float percentComplete;

}

[System.Serializable]
public class SandPaint
{
    public SkinnedMeshRenderer rend;
    public SandPaintStep fill;
    public SandPaintStep empty;
    public Color color;
    public Transform fillLerpPos;
    [HideInInspector]
    public Vector3 startPos;
    public  P3dChangeCounter counter;
    public CounterStep[] counters;
    
    public float paintedMax;
    public float timeDelay
    {
        get{
            return fill.completed==false?fill.timeDelay:empty.timeDelay;
        }
    }
    
}

public class SandPaintManager : MonoBehaviour
{
    public SandPaint[] sandPaint;
    public bool mousedown;
    public bool toFill;
    public float increaseSpeed;
    public ParticleSystem sandParticles;

    public GameObject paint;

    public SandPaint currentStep
    {
        get
        {
            if(currentStepIndex<sandPaint.Length)
            {
                return sandPaint[currentStepIndex];
            }
            else
            {
                return null;
            }
        }
    }

    int currentStepIndex;

    bool MouseDown{
        set{
            if(!currentStep.fill.completed)
            {
                ToggleParticles(value);
            }
            else{
                ToggleParticles(false);
            }
            mousedown = value;
        }

        get{
            return mousedown;
        }
    }

    void OnEnable()
    {
        paint.SetActive(false);
        currentStep.startPos = currentStep.rend.transform.position;
        currentStep.rend.gameObject.SetActive(true);
        SetSandColor();
    }

    void Update() {

        if(currentStepIndex >= sandPaint.Length) {return;}

        SetInput();
        FillSand();
    }

    void SetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
            Timer.Delay(currentStep.timeDelay, () => {
                toFill = true;
            });
        }
        else if(Input.GetMouseButtonUp(0))
        {
            MouseDown = false;
            Timer.Delay(currentStep.timeDelay, () => {
                toFill = false;
            });
        }
    }

    async void FillSand()
    {
        if(toFill)
        {
            if(!currentStep.fill.completed)
            {
                float weight = currentStep.rend.GetBlendShapeWeight(0);
                weight += increaseSpeed * Time.deltaTime;
                weight = Mathf.Clamp(weight, 0, 100);
                currentStep.rend.SetBlendShapeWeight(0, weight);
                float lerpValue = Remap.remap(weight, 0, 100, 0, 1,false,false,false,false);
                currentStep.rend.transform.position = Vector3.Lerp(currentStep.startPos, currentStep.fillLerpPos.position, lerpValue);
                if(weight >= currentStep.fill.endValue)
                {
                    currentStep.rend.SetBlendShapeWeight(0, 100);
                    currentStep.fill.completed = true;
                    MouseDown = false;
                    toFill = false;
                    paint.SetActive(true);
                }
            }
            else{
                float sum = 0;
                for(int i = 0; i < currentStep.counters.Length; i++)
                {
                    currentStep.counters[i].percentComplete = Remap.remap(currentStep.counters[i].counter.Count, currentStep.counters[i].counter.Total, currentStep.counters[i].count, 0, 100,false,false,false,false);
                    currentStep.counters[i].percentComplete = Mathf.Clamp(currentStep.counters[i].percentComplete, 0, 100);
                    sum+= currentStep.counters[i].percentComplete;
                }
                
                sum/=3;

                float weight = sum;
                //float weight = Remap.remap(count,currentStep.counter.Total,currentStep.paintedMax,100,0,false,false,false,false);
                float lerpValue = Remap.remap(weight, 0, 100, 0, 1,false,false,false,false);
                currentStep.rend.transform.position = Vector3.Lerp(currentStep.fillLerpPos.position, currentStep.startPos, lerpValue);
                float blendWeight = Remap.remap(weight, 0, 100, 100, 0,false,false,false,false);
                currentStep.rend.SetBlendShapeWeight(0,blendWeight);

                if(weight >=100)
                {
                    currentStep.empty.completed = true;
                    MouseDown = false;
                    toFill = false;
                    this.enabled = false;
                    GetComponent<GameManager>().enabled = true;
                    CameraController.instance.SetCurrentCamera(Cameras.camera1);
                }

                // float weight = currentStep.rend.GetBlendShapeWeight(0);
                // weight -= increaseSpeed * Time.deltaTime;
                // weight = Mathf.Clamp(weight, 0, 100);
                // currentStep.rend.SetBlendShapeWeight(0, weight);

                // if(weight <= currentStep.empty.endValue)
                // {
                //     currentStep.empty.completed = true;
                //     MouseDown = false;
                //     toFill = false;
                // }
            }

            if(currentStep.fill.completed && currentStep.empty.completed)
            {
                paint.SetActive(false);
                currentStepIndex++;
            }
        }
    }

    void SetSandColor()
    {
        //Assing Mesh Color
        //currentStep.rend.material.SetColor("_BaseColor", currentStep.color);
        currentStep.rend.material.color = currentStep.color;


        // Assign particle Color
        ParticleSystem.MainModule main = sandParticles.main;
        main.startColor = currentStep.color;
    }


    void ToggleParticles(bool status)
    {

        if(status)
        {
            sandParticles.Play();
        }
        else
        {
            sandParticles.Stop();
        }
    }
}
