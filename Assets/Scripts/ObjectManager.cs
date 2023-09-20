using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> collectedObjects = new List<GameObject>();
    public Transform GamepadPos;
    public Transform WirePos;
    public Transform DiscPos;
    public Transform WhateverPos;
    public int objectsToCollect = 4; 
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode placeKey = KeyCode.P;
    public LayerMask objectLayer; 

    bool inRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
           
            TryPickUpObject();
        }

        if (Input.GetKeyDown(placeKey))
        {
           
            TryPlaceObject();
        }
    }

    private void TryPickUpObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, objectLayer);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;

            
            if (!collectedObjects.Contains(obj))
            {
                
                collectedObjects.Add(obj);
                obj.SetActive(false); 
                Debug.Log("Picked up: " + obj.name);

               
                if (collectedObjects.Count == objectsToCollect)
                {
                    Debug.Log("You have collected all items");
                }

                break; 
            }
        }
    }

    private void TryPlaceObject()
    {
        if (collectedObjects.Count > 0 && inRange)
        {
            GameObject objToPlace = collectedObjects[collectedObjects.Count - 1];

            string objectName = objToPlace.name;

            switch (objectName)
            {
                case "Gamepad":
                    objToPlace.transform.position = GamepadPos.position;
                    Destroy(GamepadPos.gameObject);
                    break;

                case "Wire":
                    objToPlace.transform.position = WhateverPos.position;
                    Destroy(WirePos.gameObject);
                    break;

                case "Disc":
                    objToPlace.transform.position = DiscPos.position;
                    Destroy(DiscPos.gameObject);
                    break;

                case "Whatever":
                    objToPlace.transform.position = WhateverPos.position;
                    Destroy(WhateverPos.gameObject);
                    break;

            }
            objToPlace.SetActive(true); 

            
            collectedObjects.Remove(objToPlace);
            Debug.Log("Placed: " + objToPlace.name);
        }
        else
        {
            Debug.Log("No objects to place.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Atari"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Atari"))
        {
            inRange = false;
        }
    }
}
