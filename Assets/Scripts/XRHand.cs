using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    

    [Header("Input Bindings")]
    [SerializeField] private string GrabButton;
    [SerializeField] private string TriggerButton;


    private GrabInteractable hoveredObject;
    private GrabInteractable grabbedObject;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(GrabButton))
        {
            //Grab this!
            if (hoveredObject != null)
            {
                grabbedObject = hoveredObject;
                grabbedObject.OnGrabbed(this);
                hoveredObject = null;
            }
        }

        if (Input.GetButtonUp(GrabButton))
        {
            //Release this!
            if(grabbedObject != null)
            {
                grabbedObject.OnRelease();
                grabbedObject = null;
            }
        }

        if (Input.GetButtonDown(TriggerButton))
        {
            if(grabbedObject != null)
            {
                grabbedObject.OnTriggerStart();
            }
        }

        if (Input.GetButtonUp(TriggerButton))
        {
            if (grabbedObject != null)
            {
                grabbedObject.OnTriggerEnd();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabInteractable>();
        if(grab != null)
        {
            hoveredObject = grab;
            grab.OnHoverStart();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<GrabInteractable>();
        if (grab != null)
        {
            hoveredObject = null;
            grab.OnHoverEnd();
        }
    }
}
