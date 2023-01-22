using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace RunRun3
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
        public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask tileLayer;

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

        public Animator anim;

        private bool cooldownDash = false;
        //public Rigidbody self;
        
        //float distance;
        //int tileLayer;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            originalForwardSpeed = forwardSpeed;
            
            //Distance is slightly larger than the
            //distance = controller.radius + 0.2f;
 
            //First add a Layer name to all platforms (I used MovingPlatform)
            //Now this script won't run on regular objects, only platforms.
            //tileLayer = LayerMask.NameToLayer("Tile");
        }
        
        void Update()
        {
            direction.z = forwardSpeed;
            //controller.velocity == MoveSpeed;
            //MoveSpeed = new Vector3(controller.velocity.x, controller.velocity.y, 0);

            

            if(end == 1)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
            direction.y += Gravity * Time.deltaTime;

            if(controller.isGrounded == false)
            {
                anim.SetFloat("State", 1);
            }
            if(controller.isGrounded && isSprinting == 0)
            {
                anim.SetFloat("State", 0);
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.6f, tileLayer);
            if (hitColliders.Length != 0)
            {
                Tile tile = hitColliders[0].transform.GetComponent<Tile>();
                Debug.Log(currentlyOn);
                if (tile.index != currentlyOn)
                {
                    tileSpawner.AddNewTiles();
                    tile.index = currentlyOn;
                    /*for (int i = 0; i < 50; i++ )
                    {
                        tileSpawner.DeletePreviousTiles();
                    }*/
                }
            }
            
            if(controller.velocity.z < 0.1f && controller.isGrounded)
            {
                end = 1;
            }
            
            //RaycastHit hit;
 
            /*Bottom of controller. Slightly above ground so it doesn't bump into slanted platforms. (Adjust to your needs)
            Vector3 p1 = transform.position + Vector3.up * 0.25f;
            //Top of controller
            Vector3 p2 = p1 + Vector3.up * controller.height;*/
 
            /*Check around the character in a 360, 10 times (increase if more accuracy is needed)
            for(int i=0; i<360; i+= 36){
                //Check if anything with the platform layer touches this object
                if (Physics.CapsuleCast(p1, p2, 0, new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i)), out hit, distance, 1<<tileLayer)){
                    //If the object is touched by a platform, move the object away from it
                    controller.Move(hit.normal*(distance-hit.distance));
                }
            }*/
 
            //[Optional] Check the players feet and push them up if something clips through their feet.
            //(Useful for vertical moving platforms)
            //if (Physics.Raycast(transform.position+Vector3.up,-Vector3.up, out hit, 1, 1<<tileLayer))
            //{
                //controller.Move(Vector3.up * (1-hit.distance));
                //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.59f, tileLayer);
                //if (hitColliders.Length != 0)
                //{
                    //Tile tile = hitColliders[0].transform.GetComponent<Tile>();
                    //if (tile.index != currentlyOn)
                    //{
                        //tileSpawner.AddNewTiles();
                        //tile.index = currentlyOn;
                        /*for (int i = 0; i < 50; i++ )
                        {
                            tileSpawner.DeletePreviousTiles();
                        }*/
                    //}
                //}
            //}
        }

        private void FixedUpdate()
        {
            if(!controller.transform.hasChanged)
            {
                end = 1;
            }

            if(end == 1)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
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
            if(cooldownDash == false) 
            {
                //Do somet$$anonymous$$ng
                StartCoroutine(Dash());
                Invoke("ResetCooldown",5.0f);
                cooldownDash = true;
            }

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

        void ResetCooldown(){
            cooldownDash = false;
        }
}

}
