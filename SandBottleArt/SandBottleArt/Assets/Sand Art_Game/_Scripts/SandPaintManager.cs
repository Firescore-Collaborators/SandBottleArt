using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.EventSystems;

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
    public Renderer[] outlineRend;

    public Material drawShape;    
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

    [Foldout("Events")]
    public DefaultGameEvent drawStart;
    [Foldout("Events")]
    public DefaultGameEvent drawEnd;

    [Foldout("MeshManipulate")]
    public ManipulateNailBlob meshManipulate;
    public bool mousedown;
    public bool toFill;
    public float increaseSpeed;
    public ParticleSystem sandParticles;

    public GameObject paint;

    public GameObject tool;
    [Foldout("Reference")]
    public GameObject funnel;

    public Color outlineDisappear;
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

    public float currentPercent;
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
    }

    void Update() {

        if(currentStepIndex >= sandPaint.Length) {return;}
        SetInput();
        FillSand();
    }

    void Outline()
    {
        for(int i = 0; i<currentStep.outlineRend.Length; i++)
        {
            // List<Material> mat = currentStep.outlineRend[i].materials.ToList();
            // mat.Add(currentStep.outlineMaterial);
            // currentStep.outlineRend[i].materials = mat.ToArray();
            Color black = Color.white;
            currentStep.outlineRend[i].materials[1].SetColor("_BaseColor", black);
        }
    }
    void SetInput()
    {
        if(EventSystem.current.IsPointerOverGameObject()) {return;}
        if(Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
            Timer.Delay(currentStep.timeDelay, () => {
                toFill = true;
            });
        }
        /*else if(Input.GetMouseButtonUp(0))
        {
            MouseDown = false;
            Timer.Delay(currentStep.timeDelay, () => {
                toFill = false;
            });
        }*/
    }

    public void MouseClick()
    {
        if(!this.enabled) {return;}

        MouseDown = true;
        Timer.Delay(currentStep.timeDelay, () => {
            toFill = true;
        });
    }
    async void FillSand()
    {
        if(toFill)
        {
            if(!currentStep.fill.completed)
            {
                float weight = currentStep.rend.GetBlendShapeWeight(0);
                weight -= increaseSpeed * Time.deltaTime;
                weight = Mathf.Clamp(weight, 0, 100);
                //float remapValue = Remap.remap(weight,0,100,100,0,false,false,false,false);
                currentStep.rend.SetBlendShapeWeight(0, weight);
                //float lerpValue = Remap.remap(weight, 0, 100, 0, 1,false,false,false,false);
                //currentStep.rend.transform.position = Vector3.Lerp(currentStep.startPos, currentStep.fillLerpPos.position, lerpValue);
                if(weight <= currentStep.fill.endValue)
                {
                    currentStep.rend.SetBlendShapeWeight(0, 0);
                    currentStep.fill.completed = true;
                    MouseDown = false;
                    toFill = false;
                    //paint.SetActive(true);
                    funnel.SetActive(false);
                    tool.GetComponent<MeshRenderer>().enabled = true;
                    //Outline();
                    drawStart.Raise();
                }
            }
            else{
                    print("ff");

                /*float sum = 0;
                for(int i = 0; i < currentStep.counters.Length; i++)
                {
                    currentStep.counters[i].percentComplete = Remap.remap(currentStep.counters[i].counter.Count, currentStep.counters[i].counter.Total, currentStep.counters[i].count, 0, 100,false,false,false,false);
                    currentStep.counters[i].percentComplete = Mathf.Clamp(currentStep.counters[i].percentComplete, 0, 100);
                    sum+= currentStep.counters[i].percentComplete;
                }
                
                sum/=3;

                float weight = sum;
                //float weight = Remap.remap(count,currentStep.counter.Total,currentStep.paintedMax,100,0,false,false,false,false);
                //currentStep.rend.transform.position = Vector3.Lerp(currentStep.fillLerpPos.position, currentStep.startPos, lerpValue);
                float blendWeight = Remap.remap(weight, 0, 100, 100, 0,false,false,false,false);
                currentStep.rend.SetBlendShapeWeight(0,blendWeight);

                if(weight >=100)
                {
                    currentStep.rend.transform.position = currentStep.startPos;
                    currentStep.empty.completed = true;
                    MouseDown = false;
                    toFill = false;
                    this.enabled = false;
                    GetComponent<GameManager>().enabled = true;
                    funnel.SetActive(true);
                    tool.GetComponent<MeshRenderer>().enabled = false;
                    for(int i = 0; i<currentStep.outlineRend.Length; i++)
                    {
                        currentStep.outlineRend[i].materials[1].SetColor("_BaseColor", outlineDisappear);
                    }
                    //CameraController.instance.SetCurrentCamera(Cameras.camera1);
                }
                */

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

                currentPercent = meshManipulate.CalculatePercantage();
                float blendWeight = Remap.remap(meshManipulate.CalculatePercantage(), 0, 1, 0, 100,false,false,false,false);
                currentStep.rend.SetBlendShapeWeight(1,blendWeight);
                //currentStep.rend.transform.position = Vector3.Lerp(currentStep.fillLerpPos.position, currentStep.startPos, meshManipulate.CalculatePercantage());

                if(meshManipulate.CalculatePercantage()>=meshManipulate._autocompletePercentage)
                {
                    //currentStep.rend.transform.position = currentStep.startPos;
                    currentStep.rend.SetBlendShapeWeight(0,0);
                    currentStep.empty.completed = true;
                    mousedown = false;
                    toFill = false;
                    GetComponent<GameManager>().enabled = true;
                    funnel.SetActive(true);
                    tool.GetComponent<MeshRenderer>().enabled = false;
                    this.enabled = false;
                    drawEnd.RaiseDelay(1.0f);
                }
            }

            if(currentStep.fill.completed && currentStep.empty.completed)
            {
                paint.SetActive(false);
                currentStepIndex++;
            }
        }
    }

    public void SetColor(Color color)
    {
        if(!this.enabled) {return;}
        currentStep.color = color;
        SetSandColor();
    }
    void SetSandColor()
    {
        //Assing Mesh Color
        //currentStep.rend.material.SetColor("_BaseColor", currentStep.color);
        currentStep.rend.material.color = currentStep.color;
        currentStep.drawShape.color = currentStep.color;    


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
