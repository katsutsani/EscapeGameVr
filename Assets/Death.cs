using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIA : MonoBehaviour
{

    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject IA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IA.transform.position = SpawnPoint.position;
        IA.transform.rotation = SpawnPoint.rotation;
        IA.GetComponent<AI_Follow>().m_Speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
