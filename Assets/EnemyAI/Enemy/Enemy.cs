using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 100;
    public GameObject projectile;
    public Transform projectilePoint;

    public Animator animator;

    public GameObject arrowToSpawn;
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 7, ForceMode.Impulse);
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        if(enemyHP <= 0)
        {
            animator.SetTrigger("Death");
            // GetComponent<CapsuleCollider>().enabled = false;
            spawnArrows();
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Arrow(Clone)")
        {
            TakeDamage(50);
            _navMeshAgent.speed /= 2;
        }
    }

    private void spawnArrows()
    {
        Instantiate(arrowToSpawn, transform.position, transform.rotation);
    }
    
}
