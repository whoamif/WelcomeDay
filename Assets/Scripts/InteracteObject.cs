using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteracteObject : MonoBehaviour
{
    public UnityEvent InteractEvent;
    public UnityEvent EndInteractEvent;
    public Cinemachine.CinemachineVirtualCamera InteractVCam;
    public Transform EndInteractionPos;
    public string InteractionName;
    private bool CanInteract;
    private bool Interacting;

    private void Update()
    {
        if(!Interacting && CanInteract ) {
            GUIController.Instance.OneFrameText(GUIController.Instance.InteractionText, "Press E to " + InteractionName);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interacting = true;
                PlayerController.Instance.PlayerStat = PlayerController.PlayerStats.Intracting;
                InteractEvent.Invoke();
                CameraController.Instance.MainVCam.Priority = 0;
                InteractVCam.Priority = 1;
            }
        }
        if(Interacting && Input.GetKeyDown(KeyCode.Escape)){
            Interacting = false;

            PlayerController.Instance.PlayerStat = PlayerController.PlayerStats.Normal;
            PlayerController.Instance.transform.position = EndInteractionPos.position;
            EndInteractEvent.Invoke();
            InteractVCam.Priority = 0;
            CameraController.Instance.MainVCam.Priority = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == PlayerController.Instance.transform)
            CanInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform == PlayerController.Instance.transform)
            CanInteract = false;
    }
}
