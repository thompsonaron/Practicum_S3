using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 originPosition;
    private string targetToDamage;
    private float damage;
    public BulletType bulletType = BulletType.Regular;
    public float freezeBulletTime = 0.5f;

    private PoolableObject poolable;

    private void Start()
    {
        poolable = GetComponent<PoolableObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(transform.position, originPosition) > 10)
        {
            if (poolable != null)
            {
                poolable.ReturnToPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Activate(Transform originTransform, float projectileSpeed, string target, float damage)
    {
        transform.position = originTransform.position;
        transform.rotation = originTransform.rotation;
        originPosition = transform.position;
        speed = projectileSpeed;
        targetToDamage = target;
        this.damage = damage;
        //more stuff... color, bulletType.... 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetToDamage)
        {
            if (bulletType == BulletType.Regular)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            else if (bulletType == BulletType.Freeze)
            {
                other.gameObject.GetComponent<DashMovement>().FreezeMovement(freezeBulletTime);
            }
        }
    }

    public enum BulletType
    {
        Regular, Freeze
    }
}

public struct Target
{
    public static string Player = "Player";
    public static string Enemy = "Enemy";
}
