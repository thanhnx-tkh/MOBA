using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Player_Mobile player_Mobile;
    [SerializeField] private Abilities_Mobile abilities_Mobile;

    [SerializeField] private Movement movement;

    [SerializeField] private Animator anim;
    public Bullet prefabBullet1;
    public Bullet2 prefabBullet2;
    public Bullet3 prefabBullet3;

    public IEnumerator Skill1Fire(Vector3 direction, Transform pointFire)
    {
        yield return new WaitForSeconds(0.75f);

        Bullet bullet = Instantiate(prefabBullet1);
        bullet.transform.position = pointFire.position;

        bullet.OnInit(direction);

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isSkill1", false);
        if (player_Mobile != null)
        {
            player_Mobile.ActiveMoving();
        }
        if (abilities_Mobile != null)
        {
            abilities_Mobile.isUseSkill = false;
        }
        if (movement != null)
        {
            movement.ActiveMoving();
        }
    }

    public IEnumerator Skill2Fire(Vector3 position)
    {
        yield return new WaitForSeconds(1f);

        Bullet2 bullet2 = Instantiate(prefabBullet2);
        bullet2.transform.position = position;
        bullet2.OnInit();

        yield return new WaitForSeconds(0.3f);

        anim.SetBool("isSkill2", false);

        if (player_Mobile != null)
        {
            player_Mobile.ActiveMoving();
        }
        if (abilities_Mobile != null)
        {
            abilities_Mobile.isUseSkill = false;
        }
        if (movement != null)
        {
            movement.ActiveMoving();
        }
    }

    public IEnumerator Skill3Fire(Vector3 direction, Transform pointFire)
    {
        yield return new WaitForSeconds(0.75f);

        Bullet3 bullet3 = Instantiate(prefabBullet3);
        bullet3.transform.position = pointFire.position;

        bullet3.OnInit();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        bullet3.transform.rotation = targetRotation;

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isSkill1", false);
        if (player_Mobile != null)
        {
            player_Mobile.ActiveMoving();
        }
        if (abilities_Mobile != null)
        {
            abilities_Mobile.isUseSkill = false;
        }
        if (movement != null)
        {
            movement.ActiveMoving();
        }
    }
}
