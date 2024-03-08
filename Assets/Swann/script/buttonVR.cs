using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class buttonVR : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject button;
    GameObject presser;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.GetChild(0).gameObject;
        isPressed = false;
    }

    private void Update()
    {
        if (button.transform.localPosition.y < 2.5f)
            button.transform.localPosition = new Vector3(0, 2.5f, 0);

        if (button.transform.localPosition.y > 4f)
            button.transform.localPosition = new Vector3(0, 4f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void spawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
