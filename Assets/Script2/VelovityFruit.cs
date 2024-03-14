using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelovityFruit : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;
    private Vector3 dir;

    private void Start()
    {
        dir = Target.transform.position - transform.position;
        GetComponent<Rigidbody>().velocity = dir.normalized;
    }
}
