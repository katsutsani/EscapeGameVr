using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDestroy : MonoBehaviour
{
    private int fruits = 0;
    [SerializeField]
    private GameObject stair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            fruits++;
        }
    }

    private void Update()
    {
        if (fruits > 20)
        {
            stair.SetActive(true);
        }
    }
}
