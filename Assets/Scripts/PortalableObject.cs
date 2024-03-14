using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalableObject : MonoBehaviour
{

    private GameObject cloneObject;

    private int inPortalCount = 0;

    private Portal inPortal;

    private Portal outPortal;

    private new Rigidbody body;
    protected new Collider collider;

    public float Cooldown = 5;
    public float ActualCooldown;
    private bool InCooldown = false;

    private static readonly Quaternion halfTurn = Quaternion.Euler(0f, 180.0f, 0f);

    private void Awake()
    {
        cloneObject = new GameObject();
        cloneObject.SetActive(false);

        cloneObject.transform.localScale = transform.localScale;

        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        if (inPortal == null || outPortal == null) return;

        if (cloneObject.activeSelf && inPortal.IsPlaced && outPortal.IsPlaced)
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;
        }
        else
        {
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        Physics.IgnoreCollision(collider, wallCollider);

        cloneObject.SetActive(false);

        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        Physics.IgnoreCollision(collider, wallCollider, false);

        --inPortalCount;

        if (inPortalCount == 0)
        {
            cloneObject.SetActive(false);
        }
    }

    public virtual void Warp()
    {
        if (!InCooldown)
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            transform.position = outTransform.TransformPoint(relativePos);

            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            transform.rotation = outTransform.rotation * relativeRot;
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);

            Vector3 relativeVel = inTransform.InverseTransformDirection(body.velocity);
            relativeVel = halfTurn * relativeVel;
            body.velocity = outTransform.TransformDirection(relativeVel);

            var tmp = inPortal;
            inPortal = outPortal;
            outPortal = tmp;

            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);

            ActualCooldown = Cooldown;
            InCooldown = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ActualCooldown < 4)
        {
            if (inPortal != null && outPortal != null)
            {
                inPortal.GetComponent<BoxCollider>().isTrigger = false;
                outPortal.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
        if (ActualCooldown > 0)
        {
            ActualCooldown -= Time.deltaTime;
        }
        else
        {
            InCooldown = false;
            if(inPortal != null && outPortal != null)
            {
                inPortal.GetComponent<BoxCollider>().isTrigger = true;
                outPortal.GetComponent<BoxCollider>().isTrigger = true;
            }

        }

    }
}
