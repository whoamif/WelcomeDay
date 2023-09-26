using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform SitPosition;

    public void SitOnChair()
    {
        StartCoroutine(ISitOnChair());
    }

    private IEnumerator ISitOnChair()
    {
        yield return PlayerController.Instance.IMoveToPosition(SitPosition.position, SitPosition.forward);
        PlayerController.Instance.SetStat(PlayerController.PlayerStats.Controlled);
        PlayerController.Instance.PlayerAnim.SetTrigger("Sit");
    }

    public void CancelSit()
    {
        PlayerController.Instance.PlayerAnim.SetTrigger("CancelSit");
    }
}
