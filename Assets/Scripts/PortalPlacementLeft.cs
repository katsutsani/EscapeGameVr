using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalPlacementLeft : MonoBehaviour
{
    public InputActionProperty Leftfire;
    bool LeftAlreadyFire = false;

    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Crosshair crosshair;

    private CameraMoveLeft cameraMoveLeft;

    private void Awake()
    {
        cameraMoveLeft = GetComponent<CameraMoveLeft>();
    }

    // Update is called once per frame
    void Update()
    {

        bool LeftfireValue = Leftfire.action.IsPressed();

        if (!LeftfireValue && LeftAlreadyFire)
        {
            LeftAlreadyFire = false;
        }
        else if (LeftfireValue && !LeftAlreadyFire)
        {
            FirePortal(1, transform.position, transform.forward, 250.0f);
            LeftAlreadyFire = true;
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
                var inPortal = hit.collider.GetComponent<Portal>();

                if (inPortal == null)
                {
                    return;
                }

                var outPortal = inPortal.OtherPortal;

                Vector3 relativePos = inPortal.transform.InverseTransformPoint(hit.point + dir);
                relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
                pos = outPortal.transform.TransformPoint(relativePos);

                Vector3 relativeDir = inPortal.transform.InverseTransformDirection(dir);
                relativeDir = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeDir;
                dir = outPortal.transform.TransformDirection(relativeDir);

                distance -= Vector3.Distance(pos, hit.point);

                FirePortal(portalID, pos, dir, distance);

                return;

            }

            var cameraRotationLeft = cameraMoveLeft.TargetRotation;
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
