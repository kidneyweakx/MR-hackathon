using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    public float triggerTime = 1.5f;
    float timer;
    bool isAction;
    bool isTrigger;
    private BoxCollider _Collider;
    void Awake()
    {
        _Collider = GetComponent<BoxCollider>();
    }
    void OnEnable()
    {
        isAction = false;
        isTrigger = false;
        timer = 0;
    }

    void Update()
    {
        if (isAction)
        {
            return;
        }

        if (isTrigger)
        {
            timer += Time.deltaTime;
            if (timer >= triggerTime)
            {
                timer = 0;
                FreezeTrigger();
            }
        }
        else
        {
            timer = 0;
        }
        isTrigger = false;
    }
    public void OnTrigger()
    {
        isTrigger = true;
    }

    void FreezeTrigger()
    {
        _Collider.enabled = false;
        isAction = true;
        ModeTrantitionManager.instance.SetMixedRealityModeAR(false);
        GameManager.instance.StartGame(true);        
    }
}
