using System;
using System.Collections;
using RunRun3;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float initialPlayerSpeed = 4f;
    [SerializeField] private float maximumPlayerSpeed = 30f;
    [SerializeField] private float playerSpeedIncreaseRate = .1f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float initialGravityValue = -9.8f;
    [SerializeField] private LayerMask tileLayer;

    private float playerSpeed;
    private Vector3 movementDirection = Vector3.forward;

    private PlayerInput playerInput;
    private InputAction jumpAction;

    private CharacterController controller;

    private int currentlyOn;
    public TileSpawner tileSpawner;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, tileLayer);
        Debug.Log(hitColliders.Length);
        Debug.Log(hitColliders);
        if (hitColliders.Length != 0)
        {
            Debug.Log("test");
            Tile tile = hitColliders[0].transform.GetComponent<Tile>();
            if (tile.index != currentlyOn)
            {
                tileSpawner.AddNewTiles();
                currentlyOn = tile.index;
            }
        }
    }
}