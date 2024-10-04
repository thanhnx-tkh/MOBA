using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    private void LateUpdate() {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        transform.LookAt(transform.position + Camera.transform.forward);
    }
}