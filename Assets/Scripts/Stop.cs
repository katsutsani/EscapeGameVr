using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    [SerializeField] AI_Follow IA;
    [SerializeField] GameObject glassCase;
    [SerializeField] GameObject SwordDeco;
    [SerializeField] GameObject Sword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IA.m_Speed = 0;
        glassCase.SetActive(false);
        SwordDeco.SetActive(false);
        Sword.SetActive(true);
    }
}
