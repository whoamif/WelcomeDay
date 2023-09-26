using UnityEngine;
using System.Collections;
using Cinemachine;

public class bityReactions : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public float focusDuration = 5.0f; 
    private int originalPriority; 
    private bool isFocusing = false; 
    [HideInInspector]public Animator PlayerAnim;

    private void Start()
    {
        PlayerAnim = GetComponent<Animator>();
        originalPriority = virtualCamera.Priority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFocusing)
        {
            isFocusing = true;
            PlayerAnim.SetBool("isFocusing", true);
            PlayerAnim.SetFloat("EmoteIntensity", 0.5f);
           
            StartFocusing();
        }

        if (Input.GetKeyDown(KeyCode.A) && !isFocusing)
        {
            isFocusing =  true;
            PlayerAnim.SetBool("isFocusing",true) ;
            PlayerAnim.SetFloat("EmoteIntensity", 0.4f);
            StartFocusing();
        }
        
        if (Input.GetKeyDown(KeyCode.B) && !isFocusing)
        {
            PlayerAnim.SetBool("isFocusing", true);
            PlayerAnim.SetFloat("EmoteIntensity", 1.0f);
            StartFocusing();
        }
    }

    private void StartFocusing()
{
    virtualCamera.Priority = 1;
    StartCoroutine(FocusForDuration());
    
}


    private IEnumerator FocusForDuration()
    {
        yield return new WaitForSeconds(focusDuration);
        PlayerAnim.SetFloat("EmoteIntensity", 0.0f); 
        PlayerAnim.SetBool("isFocusing", false); 
        isFocusing = false ;
        virtualCamera.Priority = originalPriority;
    }
}
