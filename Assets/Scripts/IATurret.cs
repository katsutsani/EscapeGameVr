using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATurret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float range = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("SetUp")]
    public string PlayersTag = "Player";
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;
    

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

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletMovement bullet = bulletGo.GetComponent<BulletMovement>();
        
        if (bullet != null )
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
