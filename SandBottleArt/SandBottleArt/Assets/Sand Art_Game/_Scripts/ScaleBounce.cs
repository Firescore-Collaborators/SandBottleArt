using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
public class ScaleBounce : MonoBehaviour
{

    public float endValue;

    [Button]
    public void Bounce()
    {
        transform.DOScale(endValue,0.1f).SetLoops(2,LoopType.Yoyo);
    }

    public void BounceTwice()
    {
        transform.DOScale(endValue,0.1f).SetLoops(2,LoopType.Yoyo);
        Timer.Delay(0.65f,()=>{
            transform.DOScale(endValue,0.1f).SetLoops(2,LoopType.Yoyo);
        });
    }

}

//0.1775565