using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private Rigidbody rb;
    private Animator PlayerAnim;
    private bool Playing;
    public enum PlayerStats
    {
        Normal,Intracting
    }
    public PlayerStats PlayerStat;
    [Header("Movements")]
    private Vector3 Direction;
    private Vector3 velocity;
    public float speed, lerpspeed;
    [Header("Drage")]
    public Transform Handpos;
    [HideInInspector]public Item CurentItem;

    [Header("Visuals")]
    public SkinnedMeshRenderer[] PlayerRenderers;
    [Header("SoundEffects")]
    public AudioSource Walk;



    void Start()
    {
        //Singletone
        Instance = this;

        rb = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
    }
    void Update()
    {
        if(PlayerStat == PlayerStats.Normal)
        {
            Movements();
            foreach(SkinnedMeshRenderer Renderer in PlayerRenderers)
                Renderer.enabled = true;
        }
        else
        {
            foreach (SkinnedMeshRenderer Renderer in PlayerRenderers)
                Renderer.enabled = false;
            Walk.Stop();
        }

        //Sync Animations
        Direction = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        PlayerAnim.SetBool("Move", Direction != Vector3.zero);
        PlayerAnim.SetFloat("MoveX",Direction.x);
        PlayerAnim.SetFloat("MoveY", Direction.z);

    }
    private void FixedUpdate()
    {
        if (PlayerStat == PlayerStats.Normal)
            FixedMovements();
    }

    private void Movements()
    {
        //Movements
        velocity = transform.right * Direction.x * speed + Vector3.up * rb.velocity.y + transform.forward * Direction.z * speed;
        //Drage


        if(Direction != Vector3.zero)
        {
            this.transform.eulerAngles = Vector3.up * CameraController.Instance.CMHandler.eulerAngles.y;
            if (!Playing)
            {
            Walk.Play();
                Playing = true;
            }
        }
        else
        {
            Walk.Stop();
            Playing = false;

        }


        if (CurentItem != null)
        {
            CurentItem.transform.position = Handpos.transform.position;
            CurentItem.transform.rotation = Handpos.transform.rotation;
        }
    }
    private void FixedMovements()
    {
        rb.velocity = velocity;
    }

    public enum SurfaceType
    {
        Opaque,
        Transparent
    }
}

