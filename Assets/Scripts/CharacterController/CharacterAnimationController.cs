using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour {


	private SpriteRenderer spriteRenderer;
	private Controller2D controller2D;
	private Animator animator;

	private CharacterController charController;
	private bool isChristmas;


	void Awake(){
		controller2D = gameObject.GetComponent<Controller2D>();
		animator = gameObject.GetComponent<Animator>();
		charController = GetComponent<CharacterController>();

		isChristmas = false;
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
		AnimationLayer ();

		//PlayChristmas ();
	}


	private void PlayIdleOrRun(){
		Vector2 move = Vector2.zero;
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


	private void PlayChristmas(){
		if (Input.GetKeyDown (KeyCode.J)) {
			isChristmas = !isChristmas;
		}
	}

	private void AnimationLayer(){
		if (!isChristmas) {
			if (controller2D.isGhost) {
				animator.SetLayerWeight (1, 1);
				animator.SetLayerWeight (0, 0);
				animator.SetLayerWeight (2, 0);
			}
		else if (!controller2D.isGhost) {
				animator.SetLayerWeight (0, 1);
				animator.SetLayerWeight (1, 0);
				animator.SetLayerWeight (2, 0);
			}
		} else {
			animator.SetLayerWeight (2, 1);
			animator.SetLayerWeight (0, 0);
			animator.SetLayerWeight (1, 0);
		}
	}
		


}
