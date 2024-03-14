using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target; 
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancethisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancethisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancethisFrame, Space.World);

    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
