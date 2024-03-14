using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private FruitLaunch _launcher;
    [SerializeField] private FruitLaunch _launcher1;
    [SerializeField] private FruitLaunch _launcher2;
    [SerializeField] private AudioSource _MainTheme;
    [SerializeField] private AudioSource _Theme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_launcher.StartGame)
        {
            _Theme.Stop();
            _MainTheme.Play();
            _launcher.StartGame = true;
        }
    }
}

