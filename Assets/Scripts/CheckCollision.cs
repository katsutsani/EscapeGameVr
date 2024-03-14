using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private FruitLaunch _launcher;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_launcher.StartGame)
        {
            _launcher.StartGame = true;
        }
    }
}

