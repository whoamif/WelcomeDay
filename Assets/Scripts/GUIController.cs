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
    [Header("Inventory")]
    public bool ShowInventory;
    public Image InventoryImage;
    public InventoryItem[] Items;
    [System.Serializable]
    public struct InventoryItem{
        public string Name;
        public Sprite Image;
    }
    void Start()
    {
        Instance = this;
        InteractionText.gameObject.SetActive(false);
        InputsText.gameObject.SetActive(false);
        for (int i = 0; i < Items.Length; i++)
        {
            InventoryImage.sprite = null;
        }
    }

  
    void Update()
    {
        if (PlayerController.Instance.CurentItem == null || !ShowInventory)
        {
            InventoryImage.gameObject.SetActive(false);
        }
        else
        {
            InventoryImage.gameObject.SetActive(true);
            for (int i = 0; i < Items.Length; i++)
            {
                if (PlayerController.Instance.CurentItem.ItemName == Items[i].Name)
                {
                    InventoryImage.sprite = Items[i].Image;
                }
            }
        }

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

<<<<<<< Updated upstream
    public void CanShowInv()
    {
        ShowInventory = true;
    }
    public void HideInv()
    {
        ShowInventory = false;
=======
    public void RotationSpinPanel()
    {
  
>>>>>>> Stashed changes
    }
}
