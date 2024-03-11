using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATurret : MonoBehaviour
{
   private Transform target;
    public float range = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(Player);

        foreach (GameObject Player in players) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceToPlayer < range)  
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
