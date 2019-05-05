using System;
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

    public GameObject jumpBox;

    public GameObject Mask;

    public GameObject MaskSpawn;  

    public AudioSource Wine;

    public bool isAlive;

    public AudioSource ded;

    public float KnockBackForce;
    public float KnockBackTime;
    private float KnockBackCounter;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();

        SpinObject.SetActive(false);
        
        jumpBox.SetActive(false);
      
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {


            // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            if (KnockBackCounter <- 0)
            {



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

                if (Input.GetButtonDown("Spin"))
                {
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

            }
            else
            {
                KnockBackCounter -= Time.deltaTime;
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

                
            

           
            
               


            

           
        }
    }

   public void KnockBack(Vector3 direction)
    {
        KnockBackCounter = KnockBackTime;


        moveDirection = direction * KnockBackForce;
        moveDirection.y = KnockBackForce;

        
    }
    void Sppin()
    {
        SpinObject.SetActive(false);
        PlayerModel.SetActive(true);
        moveSpeed = 8f;

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
           Wine.Play();

            
        }
        
       
        
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
