using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SandPaintStep
{
    public float endValue;
    public float timeDelay;
    public bool completed;
}

[System.Serializable]
public class SandPaint
{
    public SkinnedMeshRenderer rend;
    public SandPaintStep fill;
    public SandPaintStep empty;
    public Color color;
    public Transform fillLerpPos;

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

    void Start()
    {
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

    void FillSand()
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
                LerpObjectPosition.instance.LerpObject(currentStep.rend.transform,currentStep.fillLerpPos.position,lerpValue);
                if(weight >= currentStep.fill.endValue)
                {
                    currentStep.fill.completed = true;
                    MouseDown = false;
                    toFill = false;
                }
            }
            else{

                paint.SetActive(true);

                float weight = currentStep.rend.GetBlendShapeWeight(0);
                weight -= increaseSpeed * Time.deltaTime;
                weight = Mathf.Clamp(weight, 0, 100);
                currentStep.rend.SetBlendShapeWeight(0, weight);

                if(weight <= currentStep.empty.endValue)
                {
                    currentStep.empty.completed = true;
                    MouseDown = false;
                    toFill = false;
                }
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
