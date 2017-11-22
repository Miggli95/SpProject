using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour {

	//private Sprite sprite;
	//private Controller2D charController;
	//private string lastState;

	private SpriteRenderer spriteRenderer;
	private Controller2D charController;
	private Animator animator;

	private CharacterController controller;


	void Awake(){
		charController = gameObject.GetComponent<Controller2D>();
		animator = gameObject.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayIdleOrRun();
		PlayDashAnimation ();
		PlayJumpAnimation();
		animator.SetFloat ("MoveDirY", charController.moveDir.y);
	}


	private void PlayIdleOrRun(){
		Vector2 move = Vector2.zero;
		move.x = Input.GetAxis ("Horizontal");
		if (move.x != 0) {
			bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.01f) : (move.x > 0.01f));
			if (flipSprite)
				spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetFloat ("Run", Mathf.Abs (move.x));
	}

	private void PlayDashAnimation(){
		animator.SetBool ("Dash", charController.dash);
	}

	
	public void PlayJumpAnimation(){
		animator.SetBool ("Jump", controller.isGrounded);
	}



}
