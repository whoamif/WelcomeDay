using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class MainMenuController : MonoBehaviour
{
    public Animator FadeAnimator;
    public CinemachineVirtualCamera CreditVCam;
    public CinemachineVirtualCamera StartVCam;
    public Animator VCamsAnimator;
    public GameObject[] Buttons;
    private bool InCredit;

    private void Start()
    {
        InCredit = false;
    }
    private void Update()
    {
        if(InCredit && Input.GetKeyDown(KeyCode.Escape))
        {
            StopCredit();
        }
    }
    public void Play()
    {
        StartCoroutine(IPlay());
    }

    IEnumerator IPlay()
    {
        FadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Spot");
    }

    public void Exit()
    {
        StartCoroutine(IExit());
    }
    public IEnumerator IExit()
    {
        FadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.1f);
        Application.Quit();
    }


    public void Credit()
    {
        InCredit = true;
        CreditVCam.Priority = 10;

        for(int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
    }

    public void StopCredit()
    {
        StartVCam.Priority = 1;
        CreditVCam.Priority = 0;
        VCamsAnimator.SetTrigger("Reset");

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(true);
        }
    }
}
