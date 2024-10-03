using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttackEnemy : MonoBehaviour
{
    public GameObject player;

    public GameObject target;
    
    private void Start() {
        target = null;
    }

    private void Update() {
        if(player == null) return;
        if (Vector3.Distance(transform.position, player.transform.position) <= 10f)
            target = player;
        else {
            target = null;
        }
    }
}
