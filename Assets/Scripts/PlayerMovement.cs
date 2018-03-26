using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80f;
    public AudioClip pickupSound;
    public FlashScreen flash;

    float forwardMovement;
    float sidewaysMovement;

    float verticalVelocity;

    float verticalRotation = 0;
    CharacterController cc;
    AudioSource source;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rozglądanie się na boki
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Rozglądanie się góra dół
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Poruszanie graczem
        //Tylko jeśli ma kontakt z podłożem
        if (cc.isGrounded)
        {
            forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
            //Bieganie jeśli gracz wcisnął Lewy Shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    DynamicCrosshair.spread = DynamicCrosshair.RUN_SPREAD;
                } else
                {
                    DynamicCrosshair.spread = DynamicCrosshair.WALK_SPREAD;
                }
            }
        } else
        {
            DynamicCrosshair.spread = DynamicCrosshair.JUMP_SPREAD;
        }
        // Sprawienie, aby na gracza działała grawitacja
        // A więc, żeby podłoże go przyciągało
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        //Skakanie po wciśnięciu przycisku odpowiedzialnego za skok
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);
        //Poruszanie bohaterem
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HpBonus"))
        {
            GetComponent<PlayerHealth>().AddHealth(20);
        } else if (other.CompareTag("ArmorBonus"))
        {
            GetComponent<PlayerHealth>().AddArmor(50);
        } else if (other.CompareTag("AmmoBonus"))
        {
            transform.Find("Weapons").Find("PistolHand").GetComponent<Pistol>().AddAmmo(15);
        }

        if(other.CompareTag("HpBonus") || other.CompareTag("ArmorBonus") || other.CompareTag("AmmoBonus"))
        {
            flash.PickedUpBonus();
            source.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        }
    }
}
