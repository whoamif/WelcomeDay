using UnityEngine;
using System.Collections;
using Cinemachine;

public class bityReactions : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public float focusDuration = 5.0f; 
    private int originalPriority; 
    private bool isFocusing = false; 
    private Animator animator; 
    private bool isDancing = false; 
    private void Start()
    {
        
        animator = GetComponent<Animator>();
        
        originalPriority = virtualCamera.Priority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFocusing && !isDancing)
        {
            
            StartCoroutine(FocusForDuration());
            PlayEmote("dancing");
        }
    }

    private IEnumerator FocusForDuration()
    {
        virtualCamera.Priority = 1;
        isFocusing = true;

        
        yield return new WaitForSeconds(focusDuration);

        
        virtualCamera.Priority = originalPriority;
        isFocusing = false;
    }

    private void PlayEmote(string emoteName)
    {
        if (animator != null)
        {
            animator.SetBool(emoteName,true);
           
        }
    }

    

}
