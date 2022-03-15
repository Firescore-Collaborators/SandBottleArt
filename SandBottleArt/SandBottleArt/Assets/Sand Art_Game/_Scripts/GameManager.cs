using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class SandStep{

    public SkinnedMeshRenderer rend;
    public Color sandColor;
    public float fillTimeDelay;
}

public class GameManager : MonoBehaviour
{
    public GameObject paintParent;
    [Foldout("Arrays")]
    public SandStep[] gameSteps;
    [Foldout("Arrays")]
    public List<MeshRenderer> paintObject = new List<MeshRenderer>();
    public SandStep currentStep
    {
        get
        {
            if(currentStepIndex<gameSteps.Length)
            {
                return gameSteps[currentStepIndex];
            }
            else
            {
                return null;
            }
        }
    }

    [Foldout("Floats")]
    public float increaseSpeed;

    [Foldout("Bools")]
    public bool mousedown;
    [Foldout("Bools")]
    public bool toFill;

    [Foldout("EFX")]
    public ParticleSystem sandParticles;

    bool MouseDown{
        set{
            ToggleParticles(value);
            mousedown = value;
        }

        get{
            return mousedown;
        }
    }

    int currentStepIndex;

    void Start() {
        SetSandColor();
    }

    public void Update()
    {
        //Check if we are at the last step
        if(currentStepIndex >= gameSteps.Length) {return;}

        SetInput();
        FillSand();
    }

    void SetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
            Timer.Delay(currentStep.fillTimeDelay, () => {
                toFill = true;
            });
        }

        if(Input.GetMouseButtonUp(0))
        {
            MouseDown = false;
            Timer.Delay(currentStep.fillTimeDelay, () => {
                toFill = false;
            });
        }
    }

    void FillSand()
    {
        if(toFill)
        {
            
            
            //Get weight of first blendShape
            float weight = currentStep.rend.GetBlendShapeWeight(0);

            //Check if first blendshape is over
            if(weight > 0)
            {   
                //Reduce weight
                weight -= increaseSpeed * Time.deltaTime;
                weight = Mathf.Clamp(weight, 0, 100);
                currentStep.rend.SetBlendShapeWeight(0, weight);
            }
            else{

                weight = currentStep.rend.GetBlendShapeWeight(1);
                //Check if second blendshape is over
                if(weight < 100)
                {
                    //Increase weight
                    weight += increaseSpeed * Time.deltaTime;
                    weight = Mathf.Clamp(weight, 0, 100);
                    currentStep.rend.SetBlendShapeWeight(1, weight);
                }
                else
                {
                    StepComplete();
                }
            }
        }
    }

    void StepComplete()
    {
        toFill = false;
        MouseDown = false;
        ToggleParticles(false);

        SwitchMesh();
        
        if(currentStepIndex >= gameSteps.Length) {return;}

        SetSandColor();
    }

    void SetSandColor()
    {
        //Assing Mesh Color
        currentStep.rend.material.color = currentStep.sandColor;

        // Assign particle Color
        ParticleSystem.MainModule main = sandParticles.main;
        main.startColor = currentStep.sandColor;
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

    void SwitchMesh()
    {
        //Switching Mesh to Paint
        currentStep.rend.gameObject.SetActive(false);

        //Assign Color
        Material[] mats = paintObject[currentStepIndex].materials;
        for(int i = 0; i < mats.Length; i++)
        {
            mats[i].color = currentStep.sandColor;
        }
        paintObject[currentStepIndex].materials = mats;
        paintObject[currentStepIndex].gameObject.SetActive(true);
        currentStepIndex++;
    }

    [Button]
    void InitPaintObject()
    {
        paintObject.Clear();
        for(int i = 0; i < paintParent.transform.childCount; i++)
        {
            paintObject.Add(paintParent.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }
    }
}