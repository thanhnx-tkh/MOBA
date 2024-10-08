using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities_Mobile : MonoBehaviour
{
    public bool isUseSkill = false;
    private Outline highlightOutline;
    [SerializeField] private Animator anim;
    [SerializeField] private SkillManager skillManager;
    public Transform pointFire;
    public GameObject targetEnemy;

    [Header("Ability 1")]
    public Image abilityImage1;
    public GameObject abilityImage1Black;
    public Text abilityText1;
    public Button ability1KeyButton;
    public float ability1Cooldown = 5;
    public float skill1UseDistance = 5;
    public bool isUseSkill1 = false;

    [Header("Ability 2")]
    public Image abilityImage2;
    public GameObject abilityImage2Black;
    public Text abilityText2;
    public Button ability2KeyButton;
    public float ability2Cooldown = 7;
    public float skill2UseDistance = 7;
    public bool isUseSkill2 = false;

    [Header("Ability 3")]
    public Image abilityImage3;
    public GameObject abilityImage3Black;
    public Text abilityText3;
    public Button ability3KeyButton;
    public float ability3Cooldown = 10;
    public float skill3UseDistance = 10;
    public bool isUseSkill3 = false;

    private bool isAbility1Cooldown = false;
    private bool isAbility2Cooldown = false;
    private bool isAbility3Cooldown = false;

    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;

    Player_Mobile player_Mobile;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;

        abilityText1.text = "";
        abilityText2.text = "";
        abilityText3.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.IsState(GameState.GamePlay) != true) return;
        GetNearestEnemy();

        ability1KeyButton.onClick.AddListener(Ability1Input);
        ability2KeyButton.onClick.AddListener(Ability2Input);
        ability3KeyButton.onClick.AddListener(Ability3Input);

        IsSkill1WithinTheAssessmentArea();
        IsSkill2WithinTheAssessmentArea();
        IsSkill3WithinTheAssessmentArea();

        AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
        AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
        AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);
    }

    private void Ability1Input()
    {
        if (!isAbility1Cooldown && isUseSkill1)
        {
            isAbility1Cooldown = true;
            currentAbility1Cooldown = ability1Cooldown;

            player_Mobile = GetComponent<Player_Mobile>();
            player_Mobile.StopMoving();

            // to do : Fire
            anim.SetBool("isSkill1", true);
            isUseSkill = true;
            Vector3 direction = (targetEnemy.transform.position - pointFire.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            direction = (targetEnemy.transform.position - pointFire.position).normalized;
            StartCoroutine(skillManager.Skill1Fire(direction, pointFire));
        }
    }
    private void IsSkill1WithinTheAssessmentArea()
    {
        if (targetEnemy == null) return;
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= skill1UseDistance)
        {
            abilityImage1Black.SetActive(false);
            isUseSkill1 = true;
        }
        else
        {
            abilityImage1Black.SetActive(true);
            isUseSkill1 = false;
        }
    }

    private void Ability2Input()
    {
        if (!isAbility2Cooldown && isUseSkill2)
        {
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;

            anim.SetBool("isSkill2", true);
            isUseSkill = true;
            player_Mobile = GetComponent<Player_Mobile>();
            player_Mobile.StopMoving();
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;

            StartCoroutine(skillManager.Skill2Fire(targetEnemy.transform.position));

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

        }
    }
    private void IsSkill2WithinTheAssessmentArea()
    {
        if (targetEnemy == null) return;
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= skill2UseDistance)
        {
            DeActiveOutline();
            highlightOutline = targetEnemy.GetComponent<Outline>();
            highlightOutline.enabled = true;
            abilityImage2Black.SetActive(false);
            isUseSkill2 = true;
        }
        else
        {
            DeActiveOutline();
            abilityImage2Black.SetActive(true);
            isUseSkill2 = false;
        }
    }

    private void Ability3Input()
    {
        if (!isAbility3Cooldown && isUseSkill3)
        {
            isAbility3Cooldown = true;
            currentAbility3Cooldown = ability3Cooldown;

            anim.SetBool("isSkill1", true);
            isUseSkill = true;
            player_Mobile = GetComponent<Player_Mobile>();
            player_Mobile.StopMoving(); Vector3 direction = (targetEnemy.transform.position - pointFire.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;


            direction = (targetEnemy.transform.position - pointFire.position).normalized;

            StartCoroutine(skillManager.Skill3Fire(direction, pointFire));
        }
    }

    private void IsSkill3WithinTheAssessmentArea()
    {
        if (targetEnemy == null) return;
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= skill3UseDistance)
        {
            abilityImage3Black.SetActive(false);
            isUseSkill3 = true;
        }
        else
        {
            abilityImage3Black.SetActive(true);
            isUseSkill3 = false;
        }
    }


    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }

    public void GetNearestEnemy()
    {
        List<Enemy> enemies = GameManager.Ins.enemies;
        Enemy nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy.gameObject != null)
            targetEnemy = nearestEnemy.gameObject;
    }

    public void DeActiveOutline()
    {
        List<Enemy> enemies = GameManager.Ins.enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            highlightOutline = enemies[i].GetComponent<Outline>();
            if (highlightOutline != null)
            {
                highlightOutline.enabled = false;
            }
        }
    }

}