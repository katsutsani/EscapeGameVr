using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [field: SerializeField]
    public Portal OtherPortal { get; private set; }

    [SerializeField]
    private Renderer outlineRenderer;

    [field: SerializeField]
    public Color PortalColour { get; private set; }

    [SerializeField]
    private LayerMask placementMask;

    [SerializeField]
    private Transform testTransform;

    private List<PortalableObject> portalObjects = new List<PortalableObject>();
    [field: SerializeField]

    public bool IsPlaced { get; private set; }

    private Collider wallCollider;

    public Renderer Renderer { get; private set; }

    private new BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        Renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        outlineRenderer.material.SetColor("_OutlineColour", PortalColour);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Renderer.enabled = OtherPortal.IsPlaced;

        for (int i = 0; i < portalObjects.Count; i++)
        {
            Vector3 objPos = transform.InverseTransformPoint(portalObjects[i].transform.position);

            if (objPos.z > 0.0f)
            {
                portalObjects[i].Warp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<PortalableObject>();

        if (obj != null)
        {
            if (other.tag == "IAPlatformer")
            {
                other.transform.position = OtherPortal.transform.position + new Vector3(2f,0.0f,0.0f);            
            }
            else
            {
                portalObjects.Add(obj);
                obj.SetIsInPortal(this, OtherPortal, wallCollider);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.GetComponent<PortalableObject>();

        if (other.tag == "IAPlatformer")
        {

        }

        if (portalObjects.Contains(obj))
        {
            portalObjects.Remove(obj);
            obj.ExitPortal(wallCollider);

        }
    }

    public bool PlacePortal(Collider wallCollider, Vector3 pos, Quaternion rot)
    {
        testTransform.position = pos;
        testTransform.rotation = rot;
        testTransform.position -= testTransform.forward * 0.001f;

        FixOverhangs();

        FixIntersects();

        if (CheckOverlap())
        {
            this.wallCollider = wallCollider;
            transform.position = testTransform.position;
            transform.rotation = testTransform.rotation;

            gameObject.SetActive(true);
            IsPlaced = true;
            FindObjectOfType<Audio_Manager>().Play("PortalSound1");
            return true;
        }

        return false;
    }

    private void FixOverhangs()
    {
        var testPoints = new List<Vector3>
        {
            new Vector3(-0.6f, 0.0f, 0.1f),
            new Vector3(0.6f, 0.0f, 0.1f),
            new Vector3(0.0f,-1.1f,0.1f),
            new Vector3(0.0f,1.1f,0.1f)
        };

        var testDirs = new List<Vector3>
        {
            Vector3.right,
            -Vector3.right,
            Vector3.up,
            -Vector3.up
        };

        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;

            Vector3 raycastPos = testTransform.TransformPoint(testPoints[i]);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            if (Physics.CheckSphere(raycastPos, 0.05f, placementMask))
            {
                break;
            }

            else if (Physics.Raycast(raycastPos, raycastDir, out hit, 2.1f, placementMask))
            {
                var offset = hit.point - raycastPos;
                testTransform.Translate(offset, Space.World);
            }
        }

    }

    // Ensure the portal cannot intersect a section of wall.
    private void FixIntersects()
    {
        var testDirs = new List<Vector3>
        {
             Vector3.right,
            -Vector3.right,
             Vector3.up,
            -Vector3.up
        };

        var testDists = new List<float> { 0.6f, 0.6f, 1.1f, 1.1f };

        for (int i = 0; i < 4; ++i)
        {
            RaycastHit hit;
            Vector3 raycastPos = testTransform.TransformPoint(0.0f, 0.0f, -0.1f);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            if (Physics.Raycast(raycastPos, raycastDir, out hit, testDists[i], placementMask))
            {
                var offset = (hit.point - raycastPos);
                var newOffset = -raycastDir * (testDists[i] - offset.magnitude);
                testTransform.Translate(newOffset, Space.World);
            }
        }
    }

    private bool CheckOverlap()
    {
        var checkExtents = new Vector3(0.4f, 0.9f, 0.05f);

        var checkPositions = new Vector3[]
        {
            testTransform.position + testTransform.TransformVector(new Vector3(0.0f,0.0f,-0.1f)),

            testTransform.position + testTransform.TransformVector(new Vector3(-0.5f,-1.0f,-0.1f)),
            testTransform.position + testTransform.TransformVector(new Vector3(-0.5f,1.0f,-0.1f)),

            testTransform.position + testTransform.TransformVector(new Vector3(0.5f,-1.0f,-0.1f)),

            testTransform.position + testTransform.TransformVector(new Vector3(0.5f,1.0f,-0.1f)),

            testTransform.TransformVector(new Vector3(0.0f,0.0f,0.2f))
        };

        var intersections = Physics.OverlapBox(checkPositions[0], checkExtents, testTransform.rotation, placementMask);

        if (intersections.Length > 1)
        {
            return false;
        }
        else if (intersections.Length == 1)
        {
            if (intersections[0] != collider)
            {
                return false;
            }
        }

        bool isOverlapping = true;

        for (int i = 1; i < checkPositions.Length - 1; i++)
        {
            isOverlapping &= Physics.Linecast(checkPositions[i],
                checkPositions[i] + checkPositions[checkPositions.Length - 1], placementMask);
        }

        return isOverlapping;
    }
}
