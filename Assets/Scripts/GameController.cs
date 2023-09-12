using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform InGameUI;
    public Transform PauseUI;
    public Transform Buttons;
    public bool Paused;
    public bool EndingLevel;
    [Header("Volumes")]
    public Volume GlobalVolume;
    public Volume PauseVolume;
    public float BlendSpeed;
    public float BlendVolumes;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Paused = false;
        EndingLevel = false;
    }

    void Update()
    {
        if (PlayerController.Instance.PlayerStat == PlayerController.PlayerStats.Normal && ! Paused)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        if(Input.GetKeyDown(KeyCode.Escape) && PlayerController.Instance.PlayerStat == PlayerController.PlayerStats.Normal && !EndingLevel)
        {
            Paused = true;
        }

        
        if (Paused && !EndingLevel)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
       

        InGameUI.gameObject.SetActive(!Paused);
        PauseUI.gameObject.SetActive(Paused);
        Buttons.gameObject.SetActive(Paused && !EndingLevel);
        GlobalVolume.gameObject.SetActive(!Paused);
        PauseVolume.gameObject.SetActive(Paused);
    }
}
