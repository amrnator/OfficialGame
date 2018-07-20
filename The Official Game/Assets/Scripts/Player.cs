using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;      //Allows us to use SceneManager

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float speed = 3.0f;                  // Speed of movement
    public Follower follower;                   // closest follower
    public Vector3 orientation;

    private Vector3 pos;
    private Vector3 lastMove;
    private bool isInteracting = false;

    void Start () {
        pos = transform.position;          // Take the initial position
        lastMove = Vector3.zero;
        orientation = Vector3.down;         //initial direction
    }

    void FixedUpdate () {
        //movement

        Vector3 vel = new Vector3();

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            animator.SetBool("PlayerStop", false);
        }else {
            animator.SetBool("PlayerStop", true);
            //follower.sendMove(Vector3.zero);
        }

        if(Input.GetKey(KeyCode.A) && transform.position == pos) {        // Left
            animator.SetTrigger("FaceLeft");
            orientation = Vector3.left;
            vel = Move(Vector3.left, "PlayerLeft");

            follower.sendMove(lastMove); 
            lastMove = vel; //update
        }
        if(Input.GetKey(KeyCode.D) && transform.position == pos) {        // Right
            animator.SetTrigger("FaceRight");
            orientation = Vector3.right;
            vel = Move(Vector3.right, "PlayerRight");

            follower.sendMove(lastMove);    //send info to closest follower
            lastMove = vel; //update
        }
        if(Input.GetKey(KeyCode.W) && transform.position == pos) {        // Up
            animator.SetTrigger("FaceUp");
            orientation = Vector3.up;
            vel = Move(Vector3.up, "PlayerUp");

            follower.sendMove(lastMove);    //send info to closest follower
            lastMove = vel; //update
        }
        if(Input.GetKey(KeyCode.S) && transform.position == pos) {        // Down
            animator.SetTrigger("FaceDown");
            orientation = Vector3.down;
            vel = Move(Vector3.down, "PlayerDown");

            follower.sendMove(lastMove);    //send info to closest follower
            lastMove = vel;                 //update
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move to pos   

        // Interaction
        if(Input.GetKeyDown(KeyCode.F)){
            Interact();
        }
    }


    Vector3 Move(Vector3 dir, string animTrigger)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f);

        // check for obstacles
        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("Obstacle"))
            {
                animator.SetBool("PlayerStop", true);

                return Vector3.zero;
            }
        }

        pos += dir;

        animator.SetTrigger(animTrigger);

        return dir;
    }

    //activate interactable objects in front player
    void Interact(){

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, orientation, 1f);

        if(hit2d.collider != null){
            print(hit2d.collider.tag);
        }


    }

 

}