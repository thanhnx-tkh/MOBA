using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 3;
    [SerializeField] private float timeDesSpawn = 0.5f;
    [SerializeField] private float damage = 10;
    
    public virtual void OnInit(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
        StartCoroutine(DesSpawn());
    }

    private IEnumerator DesSpawn(){
        yield return new WaitForSeconds(timeDesSpawn);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")){
            Stats stats = other.GetComponent<Stats>();  
            stats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
