using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttackEnemy : MonoBehaviour
{
    public GameObject player;
    public float disPlayerToAttack;
    public GameObject target;
    
    private void Start() {
        target = null;
    }

    private void Update() {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        if(player == null) return;
        if (Vector3.Distance(transform.position, player.transform.position) <= disPlayerToAttack)
            target = player;
        else {
            target = null;
        }
    }
}
