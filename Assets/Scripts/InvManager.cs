using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvManager : MonoBehaviour
{
    public RectTransform hiddenTransform; // The hidden position of the UI element
    public RectTransform screenTransform; // The on-screen position of the UI element
    public float moveDuration = 0.5f;    // The duration of the movement animation

    private bool isMovingToScreen = false;
    private bool isMovingToHidden = false;
    private float startTime;

    private RectTransform rectTransform;  // Reference to the UI element's RectTransform

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = hiddenTransform.anchoredPosition; // Set the initial position to hidden
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveToScreenPosition();
            StartCoroutine(MoveToHiddenPosition());
        }
        if (isMovingToScreen)
        {
            float t = (Time.time - startTime) / moveDuration;
            rectTransform.anchoredPosition = Vector3.Lerp(hiddenTransform.anchoredPosition, screenTransform.anchoredPosition, t);

            if (t >= 1.0f)
            {
                isMovingToScreen = false;
            }
        }
        else if (isMovingToHidden)
        {
            float t = (Time.time - startTime) / moveDuration;
            rectTransform.anchoredPosition = Vector3.Lerp(screenTransform.anchoredPosition, hiddenTransform.anchoredPosition, t);

            if (t >= 1.0f)
            {
                isMovingToHidden = false;
            }
        }

    }

    public void MoveToScreenPosition()
    {
        if (!isMovingToScreen)
        {
            startTime = Time.time;
            isMovingToScreen = true;
        }
    }

    IEnumerator MoveToHiddenPosition()
    {
        yield return new WaitForSeconds(4f);
        if (!isMovingToHidden)
        {
            startTime = Time.time;
            isMovingToHidden = true;
        }
    }
}
