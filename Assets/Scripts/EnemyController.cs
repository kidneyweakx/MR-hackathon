using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public int health = 3; // Enemy health
    private Transform m_Transform;
    private Animator m_Animator;
    private Transform _PlayerTrans;

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 lastPosition;
    private TriggerDelegate _TriggerDelegate;
    private bool isAlive = true;
    private float _Timer;
    public float deadDestoryTime = 1.5f;
    public AudioClip[] idleSounds;
    public AudioClip[] footstepSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] deadSounds;
    public AudioSource m_AudioSource;
    void Awake () {
        m_Transform = GetComponent<Transform> ();
        m_Animator = GetComponent<Animator> ();
        m_AudioSource = GetComponent<AudioSource> ();
        m_NavMeshAgent = GetComponent<NavMeshAgent> ();
        _TriggerDelegate = GetComponentInChildren<TriggerDelegate> ();
        _TriggerDelegate.onTriggerPlayer = OnTriggerPlayer;
    }
    void Start () {
        _PlayerTrans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

        lastPosition = m_Transform.position;

    }
    void Update () {
        if (isAlive) {
            OnIdle ();
            UpdateAnimator ();
            UpdateMovement ();
        } else {
            UpdateDead ();
        }

    }

    void OnIdle () {
        if (!m_AudioSource.isPlaying) {
            int randomIndex = Random.Range (0, idleSounds.Length);
            m_AudioSource.clip = idleSounds[randomIndex];
            m_AudioSource.Play ();
        }

    }
    void UpdateAnimator () {
        float frameSpeed = (m_Transform.position - lastPosition).magnitude / Time.deltaTime;
        m_Animator.SetFloat ("Speed", frameSpeed);
        lastPosition = m_Transform.position;
    }
    void UpdateMovement () {
        m_NavMeshAgent.destination = _PlayerTrans.position;
    }

    void OnTriggerPlayer (bool isTrigger) {
        m_Animator.SetBool ("Attack", isTrigger);
        m_NavMeshAgent.isStopped = isTrigger;
        UnderAttack ();
    }

    void OnDead () {
        isAlive = false;
        m_NavMeshAgent.isStopped = true;
        Destroy (m_NavMeshAgent);
        Destroy (GetComponent<Collider> ());
        m_Animator.SetTrigger ("Dead");
        int randomIndex = Random.Range (0, deadSounds.Length);
        m_AudioSource.PlayOneShot (deadSounds[randomIndex]);
    }
    void UpdateDead () {
        _Timer += Time.deltaTime;
        if (_Timer >= deadDestoryTime) {
            Destroy (gameObject);
        }
    }

    public void OnAttack () {
        int randomIndex = Random.Range (0, attackSounds.Length);
        m_AudioSource.PlayOneShot (attackSounds[randomIndex]);
        GameManager.instance.PlayerUnderAttack();
    }

    public void UnderAttack () {
        health -= 1;
        if (health <= 0) {
            OnDead ();
        }
    }

    public void OnFootstep () {

        int randomIndex = Random.Range (0, footstepSounds.Length);
        m_AudioSource.PlayOneShot (footstepSounds[randomIndex]);
    }
}