using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRaycastEnemy : MonoBehaviour
{
    public float shootingTime = 0.5f;
    float timer;
    public Transform soohtPos;
    public LayerMask enemyLayer;
    private AudioSource _AudioSource;
    RaycastHit hit;
    public ParticleSystem ammoParticle;
    private LineRenderer shootLine;
    public AudioClip shootSounds;
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        shootLine = soohtPos.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Physics.Raycast(soohtPos.position, soohtPos.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, enemyLayer))
        {
            UpdateShoot();
            return;
        }

        timer = 0;
        Vector3 linePos = new Vector3(0, 0, 15);
        shootLine.SetPosition(1, linePos);
    }

    void UpdateShoot()
    {
        timer += Time.deltaTime;
        Vector3 linePos = new Vector3(0, 0, Vector3.Distance(soohtPos.position, hit.normal));
        shootLine.SetPosition(1, linePos);
        if (timer >= shootingTime)
        {
            timer = 0;
            ammoParticle.Play();
            _AudioSource.PlayOneShot(shootSounds);
            hit.collider.GetComponent<EnemyController>().UnderAttack();
        }
    }


}