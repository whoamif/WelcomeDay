using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        public GameObject GamepadUiImage;
        public GameObject DiskUiImage;
        public GameObject whateverUiImage;
        public GameObject WireUiImage;
        public GameObject _GamepadUiImage;
        public GameObject _DiskUiImage;
        public GameObject _whateverUiImage;
        public GameObject _WireUiImage;
    public GameObject spinPanel;


    bool inRange = false;

    private void Start()
    {
        GamepadUiImage.SetActive(false);
        whateverUiImage.SetActive(false);
        DiskUiImage.SetActive(false);
        WireUiImage.SetActive(false);
        spinPanel.SetActive(false);
    }

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
                string objName = obj.name;

                if (!collectedObjects.Contains(obj))
                {

                  GameObject objUI = GetTargetUIstring(objName);
                  GameObject _objUI = GetTargetUI(objName);

                if (objUI != null)
                {
                    objUI.SetActive(true);
                    spinPanel.SetActive(true);
                    _objUI.SetActive(true);
                    DisableItem(spinPanel);
                    DisableItem(_objUI);
                  }

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

                Transform targetPos = GetTargetPosition(objectName);

                if (targetPos != null)
                {
                    objToPlace.transform.position = targetPos.position;
                    targetPos.gameObject.SetActive(true);
                    collectedObjects.Remove(objToPlace);
                    Debug.Log("Placed: " + objToPlace.name);
                }
            }
            else
            {
                Debug.Log("No objects to place.");
            }
        }

        private Transform GetTargetPosition(string objectName)
        {
            switch (objectName)
            {
                case "Gamepad":
                    return GamepadPos;

                case "Wire":
                    return WirePos;

                case "Disc":
                    return DiscPos;

                case "Whatever":
                    return WhateverPos;

                default:
                    return null;
            }
        }

    private GameObject GetTargetUIstring(string objectName)
    {
        switch (objectName)
        {
            case "Gamepad":
                return GamepadUiImage;

            case "Wire":
                return WireUiImage;

            case "Disc":
                return DiskUiImage;

            case "Whatever":
                return whateverUiImage;

            default:
                return null;
        }
    }

    private GameObject GetTargetUI(string objectName)
    {
        switch (objectName)
        {
            case "Gamepad":
                return _GamepadUiImage;

            case "Wire":
                return _WireUiImage;

            case "Disc":
                return _DiskUiImage;

            case "Whatever":
                return _whateverUiImage;

            default:
                return null;
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

    IEnumerator DisableItem(GameObject obj)
    {
        yield return new WaitForSeconds(4);
        obj.SetActive(false);
    }
}




