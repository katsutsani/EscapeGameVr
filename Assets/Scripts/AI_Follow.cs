using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Follow : MonoBehaviour
{

    NavMeshAgent agent;
    Rigidbody rb;
    Animator animator;
    public float m_Speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * m_Speed) * Time.deltaTime;
        //rb.velocity = transform.forward * m_Speed;
        //animator.SetFloat("Speed", rb.velocity.magnitude);
    }

}
