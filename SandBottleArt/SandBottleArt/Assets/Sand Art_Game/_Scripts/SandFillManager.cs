using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class SandStep
// {
//     public SkinnedMeshRenderer rend;
//     public float beginValue;
//     public Color color;
//     public bool move;
// }

// public class SandFillManager : MonoBehaviour
// {
//     public SandStep[] gameSteps;
//     public GameObject sandPaint;
//     public float increaseSpeed;

//     public int currentStep;

//     public ParticleSystem sandParticles;

//     bool toFill;
//     bool toMove;

//     Transform lerpObject;
//     public Transform finalPos;
//     Vector3 startPos;


//     public bool ToFill
//     {
//         set{
//             if(gameSteps.Length>currentStep)
//             {
//                 ToggleParticles(value);
//             }
//             else{
//                 ToggleParticles(false);
//             }
//             Timer.Delay(0.75f, () => {
//                 toFill = value;
//             });
//         }

//         get{
//             return toFill;
//         }
        
//     }

//     void Update()
//     {
//         SetInput();

//         if (toFill)
//         {
//             FillSand();
//         }

//         if(toMove)
//         {
//             MoveSand();
//         }
//     }

//     void SetInput()
//     {
//         if(Input.GetMouseButtonDown(0))
//         {
//             ToFill = true;
//         }

//         if(Input.GetMouseButtonUp(0))
//         {
//             ToFill = false;
//         }

        
//     }

//     void FillSand()
//     {
        
//         if(currentStep >= gameSteps.Length)
//         {
//             return;
//         }

//         SkinnedMeshRenderer renderer = gameSteps[currentStep].rend;
//         float weight = renderer.GetBlendShapeWeight(0);
        
//         if(weight == 0)
//         {
//             weight = gameSteps[currentStep].beginValue;
//         }
        
//         weight += 0.1f * increaseSpeed * Time.deltaTime;
//         weight = Mathf.Clamp(weight, 0, 100);
//         renderer.SetBlendShapeWeight(0, weight);

//         if(weight == 100)
//         {
//             currentStep++;
//             toFill = false;
//             ToggleParticles(false);
//             toMove = false;
//             if(currentStep >= gameSteps.Length)
//             {
//                 sandPaint.SetActive(true);
//                 gameSteps[0].rend.gameObject.SetActive(false);
//                 return;
//             }

//             NextStep();
//         }
//     }

//     void NextStep()
//     {
//         gameSteps[currentStep].rend.gameObject.SetActive(true);
        
//         //Assing particle color

//         ParticleSystem.MainModule main = sandParticles.main;
//         main.startColor = gameSteps[currentStep].color;

//         if(gameSteps[currentStep].move)
//         {
//             toMove = true;
//             lerpObject = gameSteps[currentStep].rend.transform;
//             startPos = lerpObject.position;
//         }
        
//     }

//     void ToggleParticles(bool status)
//     {
//         if(status)
//         {
//             sandParticles.Play();
//         }
//         else
//         {
//             sandParticles.Stop();
//         }
//     }


//     void MoveSand()
//     {
//         if(lerpObject == null)
//         {
//             return;
//         }
        
//         lerpObject.position = Vector3.Lerp(startPos,finalPos.position,Remap.remap(gameSteps[currentStep].rend.GetBlendShapeWeight(0),0,100,0,1,false,false,false,false));

//     }
// }
