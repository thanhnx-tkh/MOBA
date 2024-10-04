using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    [SerializeField] private float timeDesSpawn = 2f;
    [SerializeField] private float damage = 10;
    
    public virtual void OnInit()
    {
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
        }
    }
}
