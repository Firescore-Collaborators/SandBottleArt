using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;

[System.Serializable]
public class SandStep{

    public SkinnedMeshRenderer rend;
    public Color sandColor;
    public Color emissionColor;
    public float fillTimeDelay;

    public bool toDraw;

    public GameObject outline;
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

    [Foldout("References")]
    public GameObject sandFillPanel;

    [Foldout("References")]
    public GameObject cork;

    [Foldout("Transform")]
    public Transform corklerpTransform;


    [Foldout("Events")]
    public DefaultGameEvent fillOver;
    [Foldout("Events")]
    public DefaultGameEvent corkPlaced;

    [Foldout("Floats")]
    public float increaseSpeed;

    [Foldout("Bools")]
    public bool mousedown;
    [Foldout("Bools")]
    public bool toFill;

    [Foldout("EFX")]
    public ParticleSystem sandParticles;

    ObjectFollowMouse corkFollow;

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

    void OnEnable() {

        if(currentStepIndex >= gameSteps.Length) {return;}

        sandFillPanel.SetActive(true);
        SetOutline();
        SetSandColor();
    }

    public void Update()
    {
        //Check if we are at the last step
        if(currentStepIndex >= gameSteps.Length) {
            CorkInput();    
            return;
        }

        SetInput();
        FillSand();
    }

    void SetInput()
    {

        if(EventSystem.current.IsPointerOverGameObject()) {return;}
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

    void CorkInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hit, 100, LayerMask.GetMask("Cork")))
        {
            if(Input.GetMouseButtonDown(0))
            {
                corkFollow = hit.transform.GetComponent<ObjectFollowMouse>();
                corkFollow.enabled = true;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(corkFollow != null)
            {
                corkFollow.enabled = false;
            }
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

    }

    [Button]
    void SetSandColor()
    {
        //Assing Mesh Color
        //currentStep.rend.material.SetColor("_BaseColor", currentStep.sandColor);
        currentStep.rend.material.SetColor("_BaseColor", currentStep.sandColor);
        currentStep.rend.material.SetColor("_EmissionColor", currentStep.emissionColor);

        // Assign particle Color
        ParticleSystem.MainModule main = sandParticles.main;
        main.startColor = currentStep.sandColor;

    }

    void SetOutline()
    {
        if(currentStep.outline != null)
        {
            currentStep.outline.SetActive(true);
        }
        if(currentStepIndex==0) {return;}

        //gameSteps[currentStepIndex-1].outline.SetActive(false);


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
        /*currentStep.rend.gameObject.SetActive(false);

        //Assign Color
        Material[] mats = paintObject[currentStepIndex].materials;
        for(int i = 0; i < mats.Length; i++)
        {
            mats[i].color = currentStep.sandColor;
        }
        paintObject[currentStepIndex].materials = mats;
        paintObject[currentStepIndex].gameObject.SetActive(true);
        */

        //Check if we should draw 

        if(currentStep.toDraw)
        {
            currentStep.outline.SetActive(false);
            this.enabled = false;
            GetComponent<SandPaintManager>().enabled = true;
            CameraController.instance.SetCurrentCamera(Cameras.camera2);
            currentStepIndex++;
        }
        else{
            gameSteps[currentStepIndex].outline.SetActive(false);
            currentStepIndex++;

            if(currentStepIndex >= gameSteps.Length) {
                fillOver.Raise();    
                return;
            }
            sandFillPanel.SetActive(true);
            SetSandColor();
            SetOutline();
        }
        
        
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

    public void LerpCork()
    {
        cork.tag = "Untagged";
        LerpObjectPosition.instance.LerpObject(cork.transform,corklerpTransform.position,0.75f,()=>
        {
            corkPlaced.Raise();
        });
    }
}
