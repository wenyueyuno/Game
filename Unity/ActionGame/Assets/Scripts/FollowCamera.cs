using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    private Transform player;

    private int speed = 1;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
	}
	
	// Update is called once per frame
	void Update () {
        //位置跟随
        Vector3 tagPos = player.position + new Vector3(-0.2f,6.9f,-10f);
        transform.position = Vector3.Lerp(transform.position, tagPos, speed * Time.deltaTime);
        //镜头注视
        Quaternion tagQuaternion = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, tagQuaternion, speed * Time.deltaTime);
	}
}
