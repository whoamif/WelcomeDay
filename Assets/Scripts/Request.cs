using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Request : MonoBehaviour
{
    public UnityEvent UseEvent;
    public string ItemRequest;
    private bool Inrange;


    public void Update()
    {
        if (PlayerController.Instance.GetComponent<PlayerController>().CurentItem != null)
        {

            if (ItemRequest == PlayerController.Instance.GetComponent<PlayerController>().CurentItem.ItemName && Inrange && Input.GetKeyDown(KeyCode.K))

            {
                UseEvent.Invoke();
            }


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Inrange = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inrange = false;
        

        }

    }
}
