using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateByAngle : MonoBehaviour
{
    [Header("AquirItem")]
    public float RotationAngle = 20;
    public float NextFrameTime = 0.2f;
    private RectTransform SpinPanel;
    public float EndScale = 1;
    float NextFrameTimer;

    void Start()
    {
        
        NextFrameTimer = NextFrameTime;
    }

    void Update()
    {

        NextFrameTimer -= Time.deltaTime;
        if (NextFrameTimer < 0)
        {
            SpinPanel.eulerAngles += Vector3.forward * RotationAngle;
            NextFrameTimer = NextFrameTime;
        }

    }

    public void OnEnable()
    {
        SpinPanel = GetComponent<RectTransform>();
        SpinPanel.localScale = Vector3.zero;
        SpinPanel.DOScale(EndScale,2).SetEase(Ease.OutExpo);
    }

    public void Disable()
    {
        SpinPanel.DOScale(EndScale, 2).OnComplete(() => gameObject.SetActive(false));
    }
}
