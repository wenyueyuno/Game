using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    private CharacterController cc;
    private Animator animator;
    public int speed = 3;

    public void Awake()
    {
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            animator.SetBool("Walk", true);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun"))
            {
                Vector3 tagDir = new Vector3(h, 0, v);
                transform.LookAt(transform.position + tagDir);
                cc.SimpleMove(transform.forward * speed);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }
	}
}
