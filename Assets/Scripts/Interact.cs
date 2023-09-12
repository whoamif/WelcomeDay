using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interact : MonoBehaviour
{
    public string InteractionName;
    public UnityEvent InteractEvent;
    private bool CanInteract;
    private bool Interacted;
    public AudioSource SoundEffect;
    void Update()
    {
        if(CanInteract && !Interacted)
        {
            GUIController.Instance.InteractionText.gameObject.SetActive(true);
            GUIController.Instance.InteractionText.text = "Press E to " + InteractionName;
            if (Input.GetKeyDown(KeyCode.E))
            {
                GUIController.Instance.InteractionText.gameObject.SetActive(false);
                Interacted = true;
                InteractEvent.Invoke();
                if(SoundEffect!=null)
                    SoundEffect.Play();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerController.Instance.gameObject)
        {
            CanInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerController.Instance.gameObject)
        {
            GUIController.Instance.InteractionText.gameObject.SetActive(false);
            CanInteract = false;
        }

    }
}
