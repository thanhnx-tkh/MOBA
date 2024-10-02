using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float damage;
    public float attackSpeed;

    //Health Slider Variables 
    public float damageLerpDuration;
    private float currentHealth;
    private float targetHealth;
    private Coroutine damageCoroutine;

    HealthUI healthUI;
    private void Awake()
    {
        healthUI = GetComponent<HealthUI>();
        currentHealth = health;
        targetHealth = health;
        healthUI.Start3DSlider(health);
        healthUI.Update2DSlider(health, currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(gameObject, damage);
        }
    }

    public void TakeDamage(GameObject target, float damageAmount)
    {
        Stats targetStats = target.GetComponent<Stats>();
        targetStats.targetHealth -= damageAmount;
        if (targetStats.targetHealth <= 0)
        {
            Destroy(target.gameObject);
            CheckIfPlayerDead();
        }
        else if (targetStats.damageCoroutine == null)
        {
            targetStats.StartLerpHealth();
        }

    }
    public void TakeDamage(float damageAmount)
    {
        targetHealth -= damageAmount;
        if (targetHealth <= 0)
        {
            Destroy(gameObject);
            CheckIfPlayerDead();
        }
        else if (damageCoroutine == null)
        {
            StartLerpHealth();
        }

    }

    private void CheckIfPlayerDead()
    {
        healthUI.Update2DSlider(health, 0);
    }
    private void StartLerpHealth()
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(LerpHealth());
        }
    }
    private IEnumerator LerpHealth()
    {
        float elapsedTime = 0;
        float initialHealth = currentHealth;
        float target = targetHealth;
        while (elapsedTime < damageLerpDuration)
        {
            currentHealth = Mathf.Lerp(initialHealth, target, elapsedTime / damageLerpDuration);
            UpdateHealthUI();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentHealth = target;
        UpdateHealthUI();
        damageCoroutine = null;

    }
    private void UpdateHealthUI()
    {
        healthUI.Update2DSlider(health, currentHealth); 
        healthUI.Update3DSlider(currentHealth);
    }
}
