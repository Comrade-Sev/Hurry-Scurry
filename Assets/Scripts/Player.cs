using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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

        private float timer = 0f;
        private int timeScore;

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

        public int isSprinting = 0;
        public Vector3 MoveSpeed;
        //public Rigidbody self;

        void Start()
        {
            startPos = transform.position;
            controller = GetComponent<CharacterController>();
            originalForwardSpeed = forwardSpeed;
        }
        
        void Update()
        {
            distanceTravelled = Vector3.Distance(transform.position, startPos);
            distanceInt = Convert.ToInt32(distanceTravelled);
            distance.text = distanceInt.ToString();

            timer += Time.deltaTime;
            timeScore = Convert.ToInt32(timer);
            
            direction.z = forwardSpeed;
            //controller.velocity == MoveSpeed;
            //MoveSpeed = new Vector3(controller.velocity.x, controller.velocity.y, 0);

            

            if(end == 1)
            {
                StartCoroutine(DieRoutine());
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
            }
            direction.y += Gravity * Time.deltaTime;



            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.59f, tileLayer);
            if (hitColliders.Length != 0)
            {
                Tile tile = hitColliders[0].transform.GetComponent<Tile>();
                if (tile.index != currentlyOn)
                {
                    tileSpawner.AddNewTiles();
                    currentlyOn = tile.index;
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
            //}
            //else if (isSprinting == 1)
            //{
                //direction.y = jumpForce * 2;
            //}
        }

        public void Sprint()
        {
            if(controller.isGrounded)
            {
                forwardSpeed = forwardSpeed * 2;
                isSprinting = 1;
            }
        
        }
        public void SprintStop()
        {
            if(controller.isGrounded)
            {
                forwardSpeed = originalForwardSpeed;
            }
        
        }

        public void DashButton()
        {
            StartCoroutine(Dash());

        }

        private void OnGUI()
        {
            GUILayout.Label(distanceString);
        }

        void scoreCalc()
        {
            score = distanceInt / timeScore * 10;
        }

        IEnumerator Dash()
        {
            float startTime = Time.time;

            if(controller.isGrounded)
            {
                while(Time.time < startTime + dashTime)
                {
                    controller.Move(direction * dashSpeed * Time.deltaTime);

                    yield return null;
                }
            }
        }

        IEnumerator DieRoutine()
        {
            Time.timeScale = 0f;
            scoreCalc();
            yield return new WaitForSecondsRealtime(1f);
            yield return leaderboard.SubmitScoreRoutine(score);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}

}
