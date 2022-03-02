using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractable : MonoBehaviour
{

    [SerializeField] private Material hoveredMaterial;
    [SerializeField] private MeshRenderer rend;

    private Material tempMaterial;
    private Collider colliderBox;
    private Rigidbody grabBody;

    // Start is called before the first frame update
    public virtual void Start()
    {
        tempMaterial = rend.material;
        grabBody = GetComponent<Rigidbody>();
        colliderBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnHoverStart()
    {
        rend.material = hoveredMaterial;
    }

    public virtual void OnHoverEnd()
    {
        rend.material = tempMaterial;
    }

    public virtual void OnGrabbed(XRHand hand)
    {
        colliderBox.enabled = false;
        OnHoverEnd();
        grabBody.useGravity = false;
        grabBody.isKinematic = true;
        transform.SetParent(hand.transform);
        
    }

    public virtual void OnRelease()
    {
        colliderBox.enabled = true;
        grabBody.useGravity = true;
        grabBody.isKinematic = false;
        transform.SetParent(null);
    }

    public virtual void OnTriggerStart()
    {

    }

    public virtual void OnTriggerEnd()
    {

    }
}
