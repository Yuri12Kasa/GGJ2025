using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCanvas : MonoBehaviour
{
    [SerializeField]private Button _button;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bubble")
        {
            _button.enabled = true;
        }
    }
}
