using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour {


	private SpriteRenderer spriteRenderer;
	private Controller2D controller2D;
	private Animator animator;

	private CharacterController charController;
    //private ControllerKeyManager keyManager;


	void Awake(){
		controller2D = gameObject.GetComponent<Controller2D>();
		animator = gameObject.GetComponent<Animator>();
		charController = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		//keyManager = GetComponent<ControllerKeyManager>();
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayIdleOrRun();
		PlayDashAnimation ();
		PlayJumpAnimation();

	}


	private void PlayIdleOrRun(){
		Vector2 move = Vector2.zero;
		//move.x = keyManager.getcharInput(this.name, true, controller2D.canCMove()).x;
		move.x = controller2D.charInput.x;
		if (move.x != 0) {
			bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.01f) : (move.x > 0.01f));
			if (flipSprite)
				spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetFloat ("Run", Mathf.Abs (move.x));
	}

	private void PlayDashAnimation(){
		animator.SetBool ("Dash", controller2D.dash);
	}

	
	public void PlayJumpAnimation(){
		animator.SetBool ("Jump", charController.isGrounded);
		animator.SetFloat ("MoveDirY", Mathf.Abs(controller2D.moveDir.y));
	}

	private void PlayDeathAnimation(){
		if (controller2D.alive)
			return;
		else{
			animator.SetBool ("Die", true);
		}
	}



}
