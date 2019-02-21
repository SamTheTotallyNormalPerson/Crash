using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash : MonoBehaviour
{

    public float moveSpeed = 8f;
    public CharacterController controller;
    public float jumpForce;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;

    public bool isCrouching;

    public GameObject PlayerModel;

    public GameObject SpinObject;

    public AudioSource jumpSound;

    public GameObject PunchBox;

    public GameObject jumpBox;

    public int PunchCount;

    public bool wumpFu;

    public GameObject Mask;

    public GameObject MaskSpawn;

    public float health;

    public AudioSource punchNoHit;

    public AudioSource Wine;

    public bool isAlive;

    public AudioSource ded;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        SpinObject.SetActive(false);
        PunchBox.SetActive(false);
        jumpBox.SetActive(false);
        wumpFu = true;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if  (isAlive == true){


            // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            float yStore = moveDirection.y;

            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {

                moveDirection.y = 0f;
                jumpBox.SetActive(false);
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                    jumpSound.Play();
                    jumpBox.SetActive(true);

                }



            }

            // Crouch

            if (controller.isGrounded && Input.GetButton("Fire1"))
            {
                anim.SetBool("isDown", true);
                moveSpeed = 4f;

            }

            else
            {
                anim.SetBool("isDown", false);
                moveSpeed = 8f;


            }

            // Spin

            if (Input.GetButtonDown("Spin")) {
                {
                    Invoke("Sppin", 1f);
                    SpinObject.SetActive(true);
                    PlayerModel.SetActive(false);
                    moveSpeed = 6f;
                }

                // Flop

                if (!controller.isGrounded && Input.GetButton("Fire1"))
                {
                    anim.SetBool("Flop", true);
                }
                else
                {
                    anim.SetBool("Flop", false);
                }
            }

            if (wumpFu == true)
            {



                //Punch

                if (Input.GetButtonDown("Fire4") && controller.isGrounded)
                {
                    anim.SetInteger("Punch", 1);
                    PunchCount += 1;
                    PunchBox.SetActive(true);
                    Invoke("Punchy", .5f);
                    punchNoHit.Play();
                }

                if (Input.GetButtonDown("Fire4") && controller.isGrounded && PunchCount == 2)
                {
                    anim.SetInteger("Punch", 2);
                    PunchCount += 1;
                    PunchBox.SetActive(true);
                    Invoke("Punchy", .5f);
                }

                if (Input.GetButtonDown("Fire4") && controller.isGrounded && PunchCount == 3)
                {
                    anim.SetInteger("Punch", 3);
                    PunchCount += 1;
                    PunchBox.SetActive(true);
                    Invoke("Punchy", .5f);
                }

                if (Input.GetButtonDown("Fire4") && controller.isGrounded && PunchCount == 4)
                {
                    anim.SetInteger("Punch", 2);
                    PunchCount = 0;
                    PunchBox.SetActive(true);
                    Invoke("Punchy", .5f);
                }
            }
            //

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
            controller.Move(moveDirection * Time.deltaTime);

            //Move The Player In Diffrent Directions Base On The Camera Look

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            //Animations
            anim.SetBool("isGrounded", controller.isGrounded);
            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));

            //Aku Aku Spawn
            if (health >= 2)
            {
                AkuSpawn();
            }

            else
            {
                bye();

            }
        }

        if (health <= 0)
        {
            isAlive = false;
            

        }
        if (isAlive == false)
        {
            anim.SetBool("Dead", true);
            ded.Play();
        }
    }

    void Sppin()
    {
        SpinObject.SetActive(false);
        PlayerModel.SetActive(true);
        moveSpeed = 8f;

    }

    void Punchy()
    {
        PunchBox.SetActive(false);
        anim.SetInteger("Punch", 0);

    }

    void Jumper()
    {
        jumpBox.SetActive(true);
    }

    void AkuSpawn()
    {

        Mask.SetActive(true);

    }

    void bye()
    {
        Mask.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            health -=1;
            Wine.Play();
        }
        
       
        
    }

    public void SavePlayer()
    {
        SaveSystem.savePlayer(this);
    }

    public void LoadPlayer()
    {
        CrashData data = SaveSystem.LoadPlayer();

        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Crate")
        {
            moveDirection.y = jumpForce;
            jumpSound.Play();
            jumpBox.SetActive(true);
        }
    }

}
