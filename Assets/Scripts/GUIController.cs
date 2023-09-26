using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;
    [Header("Helpers Text")]
    public TextMeshProUGUI InteractionText;
    public TextMeshProUGUI InputsText;

    void Start()
    {
        Instance = this;
        InteractionText.gameObject.SetActive(false);
        InputsText.gameObject.SetActive(false);
    }

 
    public void OneFrameText(TextMeshProUGUI TMP, string txt)
    {
        if (gameObject.activeSelf)
        {
            TMP.gameObject.SetActive(true);
            TMP.text = txt;
            StartCoroutine(IDisableText(TMP));
        }
    }
    public IEnumerator IDisableText(TextMeshProUGUI TMP)
    {
        yield return new WaitForEndOfFrame();
        TMP.gameObject.SetActive(false);
    }

    public void MoveValue(ref float value, float Destination, float speed)
    {
        if(value < Destination)
            value = Mathf.Clamp(value + Time.deltaTime, value, Destination);

        if (value > Destination)
            value = Mathf.Clamp(value - Time.deltaTime, Destination, value);
    }

}
