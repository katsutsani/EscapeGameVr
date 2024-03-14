using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalPlacementRight : MonoBehaviour
{
    public InputActionProperty Rightfire;
    bool RightAlreadyFire = false;

    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Crosshair crosshair;

    private CameraMoveRight cameraMoveRight;

    private void Awake()
    {
        cameraMoveRight = GetComponent<CameraMoveRight>();
    }

    // Update is called once per frame
    void Update()
    {

        bool RightfireValue = Rightfire.action.IsPressed();

        if (!RightfireValue && RightAlreadyFire)
        {
            RightAlreadyFire = false;
        }
        else if (RightfireValue && !RightAlreadyFire)
        {
            FirePortal(0, transform.position, transform.forward, 500.0f);
            FindObjectOfType<Audio_Manager>().Play("PortalSound2");
            RightAlreadyFire = true;
        }
    }

    private void FirePortal(int portalID, Vector3 pos, Vector3 dir, float distance)
    {
        RaycastHit hit;

        Physics.Raycast(pos, dir, out hit, distance, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Portal")
            {
                //var inPortal = hit.collider.GetComponent<Portal>();

                //if (inPortal == null)
                //{
                //    return;
                //}

                //var outPortal = inPortal.OtherPortal;

                //Vector3 relativePos = inPortal.transform.InverseTransformPoint(hit.point + dir);
                //relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
                //pos = outPortal.transform.TransformPoint(relativePos);

                //Vector3 relativeDir = inPortal.transform.InverseTransformDirection(dir);
                //relativeDir = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeDir;
                //dir = outPortal.transform.TransformDirection(relativeDir);

                //distance -= Vector3.Distance(pos, hit.point);

                //FirePortal(portalID, pos, dir, distance);

                return;

            }

            var cameraRotationLeft = cameraMoveRight.TargetRotation;
            var portalRight = cameraRotationLeft * Vector3.right;

            if (Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
            {
                portalRight = (portalRight.x >= 0) ? Vector3.right : -Vector3.right;

            }
            else
            {
                portalRight = (portalRight.z >= 0) ? Vector3.forward : -Vector3.forward;

            }

            var portalForward = -hit.normal;

            var portalUp = -Vector3.Cross(portalRight, portalForward);

            var portalRotation = Quaternion.LookRotation(portalForward, portalUp);

            bool wasPlaced = portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRotation);

            if (wasPlaced)
            {
                crosshair.SetPortalPlaced(portalID, true);
            }
        }

    }
}
