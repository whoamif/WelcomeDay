using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    public Camera MainCamera;
    public Cinemachine.CinemachineVirtualCamera MainVCam;
    public AudioSource CameraSource;
    public Transform CMHandler;
    public Vector2 Sensitivity = new Vector2(1, 0.5f);
    public Vector3 CamOffset = new Vector3(0, 0, 0);
    public float MaxAngle = 70;
    public float MinAnlge = 10;
    Vector3 angles;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if(PlayerController.Instance.PlayerStat == PlayerController.PlayerStats.Normal)
            RotateCamera();
    }


    private void FixedUpdate()
    {
        CMHandler.position = this.transform.position + CamOffset;

        
    }
    private void RotateCamera()
    {
        Vector3 mouseMovements = Vector3.right * Input.GetAxis("Mouse X") * Sensitivity.x - Vector3.up * Input.GetAxis("Mouse Y") * Sensitivity.y;
        CMHandler.eulerAngles += Vector3.up * mouseMovements.x;

        CMHandler.rotation *= Quaternion.AngleAxis(mouseMovements.y, Vector3.right);
        angles = CMHandler.localEulerAngles;
        angles.z = 0;

        var angle = CMHandler.localEulerAngles.x;

        //clamp
        if (angle > 180 && angle < 360- MinAnlge)
        {
            angles.x = 360 - MinAnlge;
        }
        else if (angle < 180 && angle > MaxAngle)
        {
            angles.x = MaxAngle;
        }
        CMHandler.localEulerAngles = angles;
    }
}
