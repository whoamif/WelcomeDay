using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private Rigidbody rb;
    [HideInInspector]public Animator PlayerAnim;
    private bool Playing;
    public enum PlayerStats
    {
        Normal,Controlled,Intracting
    }
    public PlayerStats PlayerStat;
    [Header("Movements")]
    private Vector3 velocity;
    public float speed;
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
        if(PlayerStat != PlayerStats.Normal)
            Walk.Stop();
    }



    private void FixedUpdate()
    {
        if (PlayerStat == PlayerStats.Normal)
            FixedMovements();
    }

    private void FixedMovements()
    {
        Vector3 MovementDirection = Vector3.ClampMagnitude(Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical"),1f);
        Vector3 RawDirection = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");

        //Movements
        velocity = transform.right * MovementDirection.x * speed + transform.forward * MovementDirection.z * speed + Vector3.up * rb.velocity.y;
        if (RawDirection == Vector3.zero)
            velocity = Vector3.zero + Vector3.up * rb.velocity.y;
        rb.velocity = velocity;

        //Camera + Sounds
        if (MovementDirection != Vector3.zero)
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


        //Animations
        Vector3 AnimationDirection = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        PlayerAnim.SetBool("Move", RawDirection != Vector3.zero);
        PlayerAnim.SetFloat("MoveX", AnimationDirection.x);
        PlayerAnim.SetFloat("MoveY", AnimationDirection.z);


        //Items
        if (CurentItem != null)
        {
            CurentItem.transform.position = Handpos.transform.position;
            CurentItem.transform.rotation = Handpos.transform.rotation;
        }
    }

    public void SetStat(PlayerStats stat)
    {
        PlayerStat = stat;
        if (PlayerStat == PlayerStats.Intracting)
        {
            foreach (SkinnedMeshRenderer Renderer in PlayerRenderers)
                Renderer.enabled = false;

        }
        else
        {
            foreach (SkinnedMeshRenderer Renderer in PlayerRenderers)
                Renderer.enabled = true;
        }
    }

    public void MoveToPosition(Vector3 position,Vector3 finalForward)
    {
        StartCoroutine(IMoveToPosition(position,finalForward));
    }

    public IEnumerator IMoveToPosition(Vector3 position, Vector3 finalForward)
    {
        SetStat(PlayerStats.Controlled);
        rb.velocity = Vector3.zero;
        position.y = transform.position.y;

        while (Vector3.Distance(transform.position,position)>0.05f)
        {
            Vector3 Direction = Vector3.ClampMagnitude(position - transform.position,1);

            transform.position = Vector3.MoveTowards(transform.position,position,speed*Time.deltaTime);

            Vector3 AnimatonDirection = transform.InverseTransformVector(Direction);

            //Rotation
            Quaternion lookRotation = Quaternion.LookRotation((position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            PlayerAnim.SetBool("Move",true);
            PlayerAnim.SetFloat("MoveX", AnimatonDirection.x);
            PlayerAnim.SetFloat("MoveY", AnimatonDirection.z);
            yield return null;
        }

        PlayerAnim.SetBool("Move", false);

        Vector3 StartForward = transform.forward;
        float LerpValue = 0;
        while (Vector3.Angle(finalForward, transform.forward) > 1f)
        {
            LerpValue += 5 * Time.deltaTime;
            transform.forward = Vector3.Lerp(StartForward, finalForward, LerpValue);
            yield return null;
        }

        SetStat(PlayerStats.Normal);
    }

    public enum SurfaceType
    {
        Opaque,
        Transparent
    }

    public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}