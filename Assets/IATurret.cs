using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATurret : MonoBehaviour
{
    private Transform target;
    public float range = 10f;
    public string PlayersTag = "Player";
    public Transform partToRotate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayersTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;

        foreach (GameObject Player in players) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceToPlayer < shortestDistance)  
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = Player;
            }
        }

        if(nearestPlayer != null && shortestDistance <= range)
        {
            target = nearestPlayer.transform;

        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y ,0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
