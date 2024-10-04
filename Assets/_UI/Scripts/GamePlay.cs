using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public Text textTotalEnemy;
    private void Update() {
        textTotalEnemy.text ="Total Enemy: "+ GameManager.Ins.enemies.Count.ToString();
    }
}
