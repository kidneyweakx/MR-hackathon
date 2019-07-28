using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLoser : MonoBehaviour
{
    public GameObject loser;
    public GameObject MR;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(loser, 8);
        Resources.UnloadAsset(MR);
    }
}
