using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private string end = "Hand";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == end)
        {
            //changer de scene pour mettre la victoire
        }
    }
}
