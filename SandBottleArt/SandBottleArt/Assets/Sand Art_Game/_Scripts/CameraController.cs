using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;

public enum Cameras
{
    camera1,
    camera2,
    camera3,
    camera4,
    camera5
}

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public DefaultGameEvent gameStart;
    public Cameras currentCam;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    [SerializeField] CinemachineVirtualCamera camera3;
    [SerializeField] CinemachineVirtualCamera camera4;
    [SerializeField] CinemachineVirtualCamera camera5;

    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera> ();

    CinemachineBrain brain;
    public bool isBlending;
    
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }    
    }

    private void Start() {
        
        // Timer.Delay(1.0f,()=>
        // {
        //     SetCurrentCamera(Cameras.zoomIn);
        // });
        brain = Camera.main.GetComponent<CinemachineBrain>();
        Timer.Delay(0.5f,()=>
        {
            SetCurrentCamera(Cameras.camera2);
            gameStart.RaiseDelay(2.0f);
        });
    }

    private void Update() {
        isBlending = brain.IsBlending;

    }

    [Button]
    void CameraInit()
    {
        cameras.Clear();
        for(int i = 0; i < transform.childCount; i++)
        {
            cameras.Add(transform.GetChild(i).GetComponent<CinemachineVirtualCamera>());
        }
    }
    void SetCameraDefault()
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 1;
        }
    }

    public void SetCurrentCamera(Cameras cam)
    {
        SetCameraDefault();
        SetBlendSpeed(2);
        switch(cam)
        {
            case Cameras.camera1:
                camera1.Priority = 2;
                break;
            case Cameras.camera2:
                camera2.Priority = 2;
                break;
            case Cameras.camera3:
                camera3.Priority = 2;
                break;
            case Cameras.camera4:
                camera4.Priority = 2;
                break;
            case Cameras.camera5:
                camera5.Priority = 2;
                break;
        }

        currentCam = cam;
    }

    public void SetCurrentCamera(int index)
    {
        SetCurrentCamera((Cameras)index);
    }

    [Button]
    public void SetCamera()
    {
        SetCurrentCamera(currentCam);
    }

    public void SetBlendSpeed(float speed)
    {
        brain.m_DefaultBlend.m_Time = speed;
    }
}
