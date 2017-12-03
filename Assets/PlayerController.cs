using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float speed = 20f; 
    private Vector3 movement;      
    private Rigidbody playerRigidbody;
	private Animator animator;
	private const string ANIM_IS_WALKING = "isWalking";

	private int groundLayerMask;

    void Awake ()
    {
        // Set up references.
        playerRigidbody = GetComponent <Rigidbody> ();
		animator = GetComponent<Animator>();
		groundLayerMask = LayerMask.GetMask("Ground");
    }

    void FixedUpdate ()
    {
        HandleMove ();
        HandleTurning ();
    }

    void HandleMove ()
    {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical =  Input.GetAxisRaw ("Vertical");
        movement.Set (horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition (transform.position + movement);
        bool walking = horizontal != 0f || vertical != 0f;
        animator.SetBool (ANIM_IS_WALKING, walking);
    }

    void HandleTurning ()
    {
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast (camRay, out floorHit, 1000f, groundLayerMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            playerRigidbody.MoveRotation (Quaternion.LookRotation (playerToMouse));
        }
    }


}
