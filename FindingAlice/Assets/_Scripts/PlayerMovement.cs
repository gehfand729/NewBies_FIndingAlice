using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    private float moveDirX;
    private Vector3 inputDir;
    

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private Vector3 j;
    private GameObject player;

    [SerializeField]
    private bool isGround;

    private Rigidbody playerRigidbody;
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();
        j = new Vector3(0,jumpForce,0);
    }
    private void Update() {
        Move();
        Jump();
    }

    private void Move(){
        moveDirX = Input.GetAxisRaw("Horizontal");
        inputDir = new Vector3(moveDirX,0,0).normalized;

        transform.position += inputDir * speed * Time.deltaTime;
        if(moveDirX == 0)
            return;
        transform.rotation = Quaternion.LookRotation(new Vector3(0,0,moveDirX));
    }
    
    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)&&isGround){
            playerRigidbody.velocity = transform.up * jumpForce;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Platform"&&other.contacts[0].normal.y>0.7f){
            isGround = true;
        }
    }
     private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Platform"){
            isGround =false;
        }
    }
}
