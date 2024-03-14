using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelovityFruit : MonoBehaviour
{
    private Vector3 dir;
    public string PlayersTag = "Player";

    private void Start()
    {
        GameObject players = GameObject.FindGameObjectWithTag(PlayersTag);
        dir = (players.transform.position + Vector3.up) - transform.position;
        GetComponent<Rigidbody>().velocity = dir.normalized* 5f;
    }
}
