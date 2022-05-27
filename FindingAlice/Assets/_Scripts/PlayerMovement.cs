using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    [Header("Move")]
    [SerializeField]  private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    //Anim
    private Animator playerAnim;

    //Scene - Player 오브젝트
    private GameObject  player;
    private Rigidbody   playerRigidbody;
    //캐릭터 좌우로 이동
    private Vector3     moveDirX;
    //키보드로부터 X축 값 얻음
    private float       inputDir;
    private bool        isGround;

    private void Awake(){
        playerAnim = this.GetComponent<Animator>();
        
    }

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();
        if(GameSceneChange.checkLoad){
            player.transform.position = DataController.Instance.gameData.playerPosition;
        }
    }
    private void Update() {
        Move();
        Jump();
    }

    public void Move(float dir = 0){
        if (!GameManager.instance.clock)
        {
            if (dir == 0) inputDir = Input.GetAxisRaw("Horizontal");
            else inputDir = dir;

            moveDirX = new Vector3(inputDir, 0, 0).normalized;
            Debug.Log($"Current InputDir : {inputDir}");
            Debug.Log("JoyStick Anim Test");

            //test
            transform.position += moveDirX * speed * Time.deltaTime;
            // playerRigidbody.velocity = moveDirX * speed;
            playerAnim.SetBool("isWalk", inputDir != 0);

            if (inputDir == 0) {
                playerAnim.SetBool("isWalk", false);
                return;
            }
            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, inputDir));
        }
    }
    
    public void Jump(bool pressButton = false){
        // z키를 누르거나 점프 버튼이 눌렸을 때 플레이어가 땅에 있을 경우 점프
        if ((Input.GetKeyDown(KeyCode.Z) || pressButton == true) && isGround){
            //playerRigidbody.velocity = transform.up * jumpForce;
            playerAnim.SetTrigger("isJump");
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            return;
        }
    }

    //플랫폼의 기울기에 따라 점프의 여부 판단
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Platform")){
            if(other.contacts[0].normal.y <= 0.7f){
                isGround = false;
                return;
            }
            if(GameManager.instance.clock){
                GameObject.Find("ClockManager").SendMessage("clockReset");
            }
        }
        isGround = true;
    }
    //플랫폼에서 떨어졌을 때 점프 제한
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Platform"))
            isGround = false;
    }


    //사출된 시계와 충돌한 후 감속
    //IEnumerator deceleration()
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        playerRigidbody.velocity *= decelSpeed;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}
}
