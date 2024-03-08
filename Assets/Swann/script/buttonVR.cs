using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class buttonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
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
