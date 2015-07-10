using UnityEngine;
using System.Collections;
/// <summary>
/// @Intro: 
/// @Binding to: Null 
/// @Author: wenyueyun
/// </summary>
public class SoulBoss : MonoBehaviour {
    private Transform player;
    private CharacterController cc;
    private float speed = 2;
    private Animator animator;
    private const float ATTACK_TIME = 3f;
    private const float ATTACK_DIS = 1.5f;

    private float attackTimer = 0;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        attackTimer = ATTACK_TIME;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        float distance = Vector3.Distance(targetPos, transform.position);

        if (distance <= ATTACK_DIS)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > ATTACK_TIME)
            {
                int num = Random.Range(0, 2);

                if(num == 0)
                {
                    animator.SetTrigger(AnimatorTigger.ATTACK_1);
                }
                else
                {
                    animator.SetTrigger(AnimatorTigger.ATTACK_2);
                }
                attackTimer = 0;
            }
            else
            {
                animator.SetBool(AnimatorTigger.WALK, false);
            }
        }
        else
        {
            attackTimer = ATTACK_TIME;
            animator.SetBool(AnimatorTigger.WALK, true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BossRun01"))
            {
                cc.SimpleMove(transform.forward * speed);
            }
        }
	}
}
