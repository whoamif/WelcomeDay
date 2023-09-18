using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Params")]

    [SerializeField] int noItemstoCollect = 4;
    [SerializeField] float dropForce = 5.0f;
    public Transform playerRightHand;
    //public Transform Atari;
    public SphereCollider myCollider;

    private GameObject pickedItem = null;
    private List<GameObject> collectedItems = new List<GameObject>();
    bool inRange = false;
    bool inRangeA = false;

    [Header("Positions")]

    public Transform GamepadPos;
    public Transform WirePos;
    public Transform DiskPos;
    public Transform WhateverPos;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && !pickedItem && !IsItemCollected(item))
        {
            Pickup();
        }

        else if (Input.GetKeyDown(KeyCode.E) && inRangeA && pickedItem)
        {
            PlaceItem();
        }

        else if (Input.GetKeyDown(KeyCode.F) && pickedItem)
        {
            DropItem();
        }

        if (collectedItems.Count == noItemstoCollect)
        {
            print("You have succefully collected all of the necessary Items");
            //action to happen here...
        }
    }

    void Pickup()
    {
        pickedItem = item;
        SphereCollider sphereCollider = pickedItem.GetComponent<SphereCollider>();
        Destroy(sphereCollider);
        Rigidbody pickedItemRigidBody = pickedItem.GetComponent<Rigidbody>();
        if (pickedItemRigidBody)
        {
            pickedItemRigidBody.isKinematic = true;
        }
        pickedItem.transform.position = playerRightHand.position;
        pickedItem.transform.SetParent(playerRightHand);
        inRange = false;

        
        print("you have picked " + pickedItem.gameObject.name);
    }

    void PlaceItem()
    {
        pickedItem.transform.SetParent(null);
        switch(pickedItem.transform.name)

        { case "Gamepad":
                pickedItem.transform.position = GamepadPos.position;
                Destroy(GamepadPos.gameObject); break;
          case "Wire":
                pickedItem.transform.position = WirePos.position;
                Destroy(WirePos.gameObject); break;
          case "Disc":
                pickedItem.transform.position = DiskPos.position;
                Destroy(DiskPos.gameObject); break;
          case "Whatever":
                pickedItem.transform.position = WhateverPos.position;
                Destroy(WhateverPos.gameObject); break;
        }

        collectedItems.Add(pickedItem);
        print("You have placed " + pickedItem.transform.name);
        pickedItem = null;

    }

    void DropItem()
    {
        pickedItem.transform.SetParent(null);
        Rigidbody itemRigidBody = pickedItem.GetComponent<Rigidbody>();
        if (itemRigidBody)
        {
            itemRigidBody.isKinematic = false;
            itemRigidBody.AddForce(transform.forward * dropForce, ForceMode.Impulse);
        }

        pickedItem.AddComponent<SphereCollider>();
        print("You have dropped " + pickedItem.transform.name);
        pickedItem = null;
    }

    private bool IsItemCollected(GameObject itemToCheck)
    {
        return collectedItems.Contains(itemToCheck);
    }

    [Header("Item")]

    public GameObject item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable") && !pickedItem && !IsItemCollected(other.gameObject))
        {
            item = other.gameObject;
            inRange = true;
            print("press E to pick up " + item.gameObject.name);
        }
        if (other.gameObject.CompareTag("Atari"))
        {
            inRangeA = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            inRange = false;
        }
        if (other.gameObject.CompareTag("Atari"))
        {
            inRangeA = false;
        }
    }
}
