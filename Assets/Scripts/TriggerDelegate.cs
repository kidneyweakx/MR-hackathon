using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDelegate : MonoBehaviour
{
    public delegate void TriggerEvent(bool isTrigger);
    public TriggerEvent onTriggerPlayer;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onTriggerPlayer != null) { onTriggerPlayer(true); }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onTriggerPlayer != null) { onTriggerPlayer(false); }
        }
    }
}
