using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    public Player player;       // Party leader
    public int followerIndex;   // Place in line
    public Animator animator;
    public Follower follower;   // follower

    private Vector3 pos;
    private Vector3 lastMove;
    private Vector3 recievedDir;
    private float speed = 3f;
    private RaycastHit2D hit;

    public bool debug = false;
    bool move = false;

    int count = 0;

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position;
        pos = transform.position;
        recievedDir = Vector3.zero;
        lastMove = Vector3.zero;
    }
	
    public void sendMove(Vector3 dir){
        recievedDir = dir;
        move = true;

        if(debug){
            print("Recieved: " + dir.ToString());
        }
    }

    void FixedUpdate()
    {
        if(transform.position == pos && move){

            if(recievedDir.Equals(Vector3.right))
            {
                animator.SetTrigger("PlayerRight");    
            }
            if (recievedDir.Equals(Vector3.left))
            {
                animator.SetTrigger("PlayerLeft");    
            }
            if (recievedDir.Equals(Vector3.up))
            {
                animator.SetTrigger("PlayerUp");    
            }
            if (recievedDir.Equals(Vector3.down))
            {
                animator.SetTrigger("PlayerDown");    
            }

            pos += recievedDir;

            if (follower != null && move)
            {
                follower.sendMove(lastMove);
                lastMove = recievedDir; //update
            }

            move = false;
        }

        if (transform.position == pos && !move)
        {
            animator.SetBool("PlayerStop", true);
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move to pos


    }
}
