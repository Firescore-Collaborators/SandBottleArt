using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIElements : MonoBehaviour
{
    public static UIElements instance;

    public SandPaintManager sandPaint;
    public CorkManager corkManager;
    public AccessoriesManager accManager;
    public Transform sandFillPanel;
    public Transform corkPanel;
    public Transform accPanel;
    public Button AccessoryButton;
    List<Button> buttons = new List<Button>();
    List<Button> corks = new List<Button>();
    List<Button> accessories = new List<Button>();


    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(this);
        }    
    }

    private void Start() {
        SandInit();
        AccInit();
        CorkInit();
    }

    void SandInit()
    {   
        buttons.Clear();
        for(int i = 0; i < sandFillPanel.transform.childCount; i++)
        {
            Button button = sandFillPanel.transform.GetChild(i).GetComponent<Button>();
            buttons.Add(button);
            button.onClick.AddListener(()=>GameManager.instance.SetCurrentColor(button.transform.GetSiblingIndex()));
            Color color = sandPaint.GetComponent<GameManager>().sandColors[i].color;
            button.onClick.AddListener(()=>sandPaint.SetColor(color));

        }
        SetButtonState(true);
    }

    void CorkInit()
    {
        corks.Clear();
        for(int i = 0; i < corkPanel.transform.childCount; i++)
        {
            Button button = corkPanel.transform.GetChild(i).GetComponent<Button>();
            corks.Add(button);
            button.onClick.AddListener(()=>corkManager.OffAllCork());
            button.onClick.AddListener(()=>corkManager.OnCork(button.transform.GetSiblingIndex()));
        }
    }

    void AccInit()
    {
        accessories.Clear();
        for(int i = 0; i < accPanel.transform.childCount; i++)
        {
            Button button = accPanel.transform.GetChild(i).GetComponent<Button>();
            accessories.Add(button);
            button.onClick.AddListener(()=>accManager.OffAllAcc());
            button.onClick.AddListener(()=>accManager.OnAccessory(button.transform.GetSiblingIndex()));
            button.onClick.AddListener(()=>
            {
                AccessoryButton.gameObject.SetActive(true);
            });
        }
    }

    public void SetButtonState(bool status)
    {   
        foreach(Button b in buttons)
        {
            b.interactable = status;
        }
    }
}
