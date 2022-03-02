using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventHandler : MonoBehaviour
{
    [SerializeField] private string TagToCompare;

    public UnityEvent OnTriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagToCompare))
        {
            OnTriggerEntered?.Invoke();
        }
    }
    
   
}
