using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;


namespace RunRun3
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
        public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask tileLayer;

        public Leaderboard leaderboard;
        private int score;
        
        public float distanceTravelled = 0f;
        public TextMeshProUGUI distance;
        private Vector3 startPos;
        private string distanceString;
        private int distanceInt;

        private float timerDisplay = 0f;
        public int timeScore;

        private float playerSpeed;
        private Vector3 movementDirection = Vector3.forward;

        private PlayerInput playerInput;
        private InputAction jumpAction;

        private CharacterController controller;

        private int currentlyOn;
        public TileSpawner tileSpawner;

        private Vector3 direction;
        public float forwardSpeed;
        public float originalForwardSpeed;

        public float jumpForce;

        public float dashTry;
        public float Gravity = -20;
        public int flag = 0;

        public float dashSpeed;

        public float dashTime;

        public int end = 0;
        public int doesItDash = 0;

        public int isSprinting = 0;
        public Vector3 MoveSpeed;

        public Animator anim;

        public CountdownTimer timer;
        public GameObject timerObject;
        public GameObject playerDeath;

        private bool cooldownDash = false;

        public Trying spinFlag;
        //public Rigidbody self;


        void Start()
        {
            startPos = transform.position;
            controller = GetComponent<CharacterController>();
            originalForwardSpeed = forwardSpeed;
        }
        
        void Update()
        {
            //if(controller.isGrounded == true && spinFlag.rotateFlag == 1)
            //{   
                //spinFlag.rotateFlag = 0;
                //direction.y = 10;
                //direction.x += 5;
            //}
            distanceTravelled = Vector3.Distance(transform.position, startPos);
            distanceInt = Convert.ToInt32(distanceTravelled);
            // distance.text = distanceInt.ToString();

            timerDisplay += Time.deltaTime;
            timeScore = Convert.ToInt32(timerDisplay);
            
            direction.z = forwardSpeed;
            //controller.velocity == MoveSpeed;
            //MoveSpeed = new Vector3(controller.velocity.x, controller.velocity.y, 0);

            

            if(end == 1)
            {
                //StartCoroutine(DieRoutine());
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
                scoreCalc();
                SceneManager.LoadScene("Death");
            }


            if(playerDeath.transform.position.y < -150)
            {
                scoreCalc();
                SceneManager.LoadScene("Death");
            }
            if(doesItDash == 0)
            {
                direction.y += Gravity * Time.deltaTime;
            }
            else if(doesItDash == 1)
            {
                direction.y -= Gravity * Time.deltaTime;
            }
            //direction.y += Gravity * Time.deltaTime;

            if(controller.isGrounded == false && doesItDash == 0)
            {
                anim.SetFloat("State", 1);
            }
            if(controller.isGrounded && isSprinting == 0)
            {
                anim.SetFloat("State", 0);
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.7f, tileLayer);
            if (hitColliders.Length != 0)
            {
                if(hitColliders[0].transform.TryGetComponent<Tile>(out var tile))
                {
                    if (tile.index != currentlyOn)
                    {
                        tileSpawner.AddNewTiles();
                        currentlyOn = tile.index;
                        /*for (int i = 0; i < 50; i++ )
                        {
                            tileSpawner.DeletePreviousTiles();
                        }*/
                    }
                }
            }
            
            if(controller.velocity.z < 0.1f && controller.isGrounded)
            {
                end = 1;
            }

        }

        private void FixedUpdate()
        {
            if(!controller.transform.hasChanged)
            {
                end = 1;
            }

            if(end == 1)
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
            }
            controller.Move(direction * Time.fixedDeltaTime);
            
        }

        public void JumpButton()
        {
            if(controller.isGrounded)
            {
                //flag = 1;
                Jump();   
            }
        }

        private void Jump()
        {
            //if(isSprinting == 0)
            //{
            direction.y = jumpForce;
            Gravity = -20;
            //}
            //else if (isSprinting == 1)
            //{
                //direction.y = jumpForce * 2;
            //}
        }
        public void RotateJump()
        {
            direction.y = jumpForce/2;
        }

        public void Sprint()
        {
            if(controller.isGrounded)
            {
                forwardSpeed = forwardSpeed * 2;
                isSprinting = 1;
                anim.SetFloat("State", 0.5f);
            }
        
        }
        public void SprintStop()
        {
            if(controller.isGrounded)
            {
                forwardSpeed = originalForwardSpeed;
                isSprinting = 0;
                anim.SetFloat("State", 0);
            }
        
        }

        public void DashButton()
        {
            if(cooldownDash == false && controller.isGrounded) 
            {
                //Do somet$$anonymous$$ng
                timerObject.SetActive(true);
                timer.currentTime = 15.0f;
                StartCoroutine(Dash());
                Invoke("ResetCooldown",15.0f);
                cooldownDash = true;
            }

        }

        void scoreCalc()
        {
            score = 3*distanceInt / timeScore * 12;
            PlayerPrefs.SetInt("score", score);
        }
        
        private void OnGUI()
        {
            GUILayout.Label(distanceString);
        }
        IEnumerator Dash()
        {
            float startTime = Time.time;

            if(controller.isGrounded)
            {
                while(Time.time < startTime + dashTime)
                {
                    doesItDash = 1;
                    controller.Move(direction * dashSpeed * Time.deltaTime);
                    anim.SetFloat("State", 0.75f);
                    direction.y = 0;
                    /*if(controller.isGrounded == false)
                    {
                        direction.y = direction.y;
                    }*/
                    //Gravity = 5;
                    //controller.Move.y();

                    yield return null;
                }
                //Gravity = -20;
                anim.SetFloat("State", 0);
                doesItDash = 0;
            }
        }

        // IEnumerator DieRoutine()
        // {
        //     Time.timeScale = 0f;
        //     scoreCalc();
        //     yield return new WaitForSecondsRealtime(1f);
        //     yield return leaderboard.SubmitScoreRoutine(score);
        //     Time.timeScale = 1f;
        // }

        void ResetCooldown(){
            cooldownDash = false;
        }
}

}
