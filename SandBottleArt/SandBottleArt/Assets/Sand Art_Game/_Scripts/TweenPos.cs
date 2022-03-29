using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class TweenPos : MonoBehaviour
{
    public float donwValue;

    void OnEnable()
    {
        TweenDownLoop();
    }

    [Button]
    void TweenDownLoop()
    {
        GetComponent<RectTransform>().DOMoveY(transform.position.y - donwValue, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
