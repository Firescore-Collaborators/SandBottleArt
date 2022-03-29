using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class ScaleSpawn : MonoBehaviour
{
    public float endValue;
    public float duration = 0.4f;

    void OnEnable()
    {
        Bounce();
    }

    [Button]
    public void Bounce()
    {
        transform.DOScale(endValue,duration).SetEase(Ease.InOutBounce);
    }

}
