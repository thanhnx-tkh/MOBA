using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    Movement movement;

    [SerializeField] private Animator anim;
    public Bullet prefabBullet1;

    public IEnumerator Skill1Fire(Vector3 direction, Transform pointFire)
    {
        yield return new WaitForSeconds(1.5f);

        Bullet bullet = Instantiate(prefabBullet1);
        bullet.transform.position = pointFire.position;

        bullet.OnInit(direction);

        yield return new WaitForSeconds(1f);
        anim.SetBool("isSkill1", false);
        movement = GetComponent<Movement>();
        movement.ActiveMoving();
    }
}
