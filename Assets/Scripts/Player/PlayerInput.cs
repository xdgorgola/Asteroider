using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Manager { get; private set; }

    public static float SteerInput
    {
        get { return Manager.steer; }
    }

    [SerializeField]
    private string steerAxis = "Horizontal";

    private float steer = 0F;

    public static bool ShootInput
    {
        get { return Manager.shoot; }
    }

    [SerializeField]
    private string shootButton = "Fire1";

    private bool shoot = false;

    public static float VelocityInput
    {
        get { return Manager.velocity; }
    }

    [SerializeField]
    private string velocityAxis = "Vertical";

    private float velocity = 0f;

    public static bool InteractionDown
    {
        get { return Manager.interactDown; }
    }

    [SerializeField]
    private string interactionButton = "Interact";

    private bool interactDown = false;

    public static bool InventoryDown
    {
        get { return Manager.inventoryDown; }
    }

    [SerializeField]
    private string inventoryButton = "Inventory";

    private bool inventoryDown = false;

    public static bool WeaponInventoryDown
    {
        get { return Manager.weaponInvDown; }
    }

    [SerializeField]
    private string weaponInvButton = "Weapon Inventory";

    private bool weaponInvDown = false;

    private void Awake()
    {
        Manager = this;
    }

    private void Update()
    {
        steer = Input.GetAxisRaw("Horizontal");
        velocity = Input.GetAxisRaw("Vertical");

        shoot = Input.GetButton(shootButton);
        interactDown = Input.GetButtonDown("Interact");
        inventoryDown = Input.GetButtonDown("Inventory");

        weaponInvDown = Input.GetButtonDown(weaponInvButton);
    }
}

