using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BoardController : MonoBehaviour
{
    public CinemachineVirtualCamera BoardVCam;
    public GameObject VirtualBrush;
    public bool Interacting;
    [Header("Debug")]
    public bool StartUse;
    [Header("Soundeffect")]
    public AudioSource brush;

    public void Start()
    {
        VirtualBrush.SetActive(false);
  
        if (StartUse)
            UseBoard();
    }
    public void Update()
    {
        if (Interacting)
        {
            GUIController.Instance.OneFrameText(GUIController.Instance.InputsText, "Left click and drag the mouse to clean the Board");
            if(Input.GetKeyDown(KeyCode.Escape))
                EndBoard();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                brush.Play();
            if (Input.GetKeyUp(KeyCode.Mouse0))
                brush.Stop();
            GetComponent<BoxCollider>().enabled = false;
        }
        else
            GetComponent<BoxCollider>().enabled = true;
    }
    public void UseBoard()
    {
        PlayerController.Instance.PlayerStat = PlayerController.PlayerStats.Intracting;
        CameraController.Instance.MainVCam.Priority = 0;
        BoardVCam.Priority = 1;
        VirtualBrush.gameObject.SetActive(true);
        Interacting = true;
    }

    public void EndBoard()
    {
        Interacting = false;
        VirtualBrush.SetActive(false);
        BoardVCam.Priority = 0;
        CameraController.Instance.MainVCam.Priority = 1;
        PlayerController.Instance.PlayerStat = PlayerController.PlayerStats.Normal;
    }
}
