using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsUi : MonoBehaviour
{
    public RectTransform objUI;
    public float EndScale = 1;


    public void OnEnable()
    {
        objUI = GetComponent<RectTransform>();
        objUI.localScale = Vector3.zero;
        objUI.DOScale(EndScale, 2).SetEase(Ease.OutExpo).OnComplete(() => gameObject.SetActive(false));
      
    }

    public void Disable()
    {
        objUI.DOScale(EndScale, 2).OnComplete(() => gameObject.SetActive(false));
    }

}

