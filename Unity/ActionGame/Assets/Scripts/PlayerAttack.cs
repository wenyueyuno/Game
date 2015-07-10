using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// @Intro: 
/// @Binding to: Null 
/// @Author: wenyueyun
/// </summary>
public class PlayerAttack : MonoBehaviour {
    private Animator animator;

    private GameObject normal_btn;

    private GameObject range_btn;

    private GameObject super_btn;

    //是否释放B攻击
    private bool isCanB;
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();

        normal_btn = GameObject.Find("NormalAttack");
        normal_btn.GetComponent<Button>().onClick.AddListener(delegate()
        {
            OnNormalAttack();
        });

        range_btn = GameObject.Find("RangeAttack");
        range_btn.GetComponent<Button>().onClick.AddListener(delegate()
        {
            OnRangeAttack();
        });

        super_btn = GameObject.Find("SuperAttack");
        super_btn.GetComponent<Button>().onClick.AddListener(delegate()
        {
            OnSuperAttack();
        });
        super_btn.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnNormalAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorTigger.ATTACK_A) && isCanB)
        {
            animator.SetTrigger(AnimatorTigger.ATTACK_B);
        }
        else
        {
            animator.SetTrigger(AnimatorTigger.ATTACK_A);
        }
    }

    public void OnRangeAttack()
    {
        animator.SetTrigger(AnimatorTigger.ATTACK_RANGE);
    }

    public void OnSuperAttack()
    {
        animator.SetTrigger(AnimatorTigger.ATTACK_A);
    }

    public void AttackBEvent1()
    {
        isCanB = true;
    }

    public void AttackBEvent2()
    {
        isCanB = false;
    }
}
