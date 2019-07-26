using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReticleRaycast : MonoBehaviour
{
    Transform _Transform;
    RaycastHit hit;
    public LayerMask eneterLayer;
	UnityEngine.UI.Image _Image;
	Color currentColor;

    void Awake()
    {
		_Image = GetComponent<UnityEngine.UI.Image>();
        _Transform = Camera.main.transform;
		currentColor = _Image.color;
    }

    void Update()
    {
        if (Physics.Raycast(_Transform.position, _Transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, eneterLayer))
        {
			_Image.color = Color.yellow;
            // hit.collider.GetComponent<EnterTrigger>().OnTrigger();
            return;
        }
		_Image.color = currentColor;
    }
}
