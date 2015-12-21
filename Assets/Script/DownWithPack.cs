using UnityEngine;
using System.Collections;

public class DownWithPack : MonoBehaviour {

	//______________Gravity_____________
	public bool pacDontTouchTheFloor = true;
	public float moveSpeed;
	int touchingAFloor;


	//______________Jump_________________

	public KeyCode jumpUp;
	public bool jumpOkOrNot = false;
	public bool jumpKeyPressed = true;

	public float timer4HowLongJump;

	float x;
	float y = 0;
	public float jumpHeight;


	public float rayLeangth;

	RaycastHit hit;

	public bool pacCanNowJump = true;

	bool pacDash = false;
	// Use this for initialization
	void Start () {

	}

	public void PacIsDashing(){
		pacDash = true;
	}
	public void PacStopedDashing(){
		pacDash = false;
	}

	public void PacHasEatenTheJumpPillAndCanNowJumpLol(){
		pacCanNowJump = true;
	}
	public void StopTheMovement(){

		if (x > 0){
			x = 0.2f;
		}
		jumpOkOrNot = false;
	}
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Wall"){

			touchingAFloor++;
			if (touchingAFloor == 1) {
				pacDontTouchTheFloor = false;
				jumpKeyPressed = true;
				x = 0;
			}
			


		}
	}
	void OnTriggerExit(Collider col){

		if (col.gameObject.tag == "Wall"){

			touchingAFloor--;
			if (touchingAFloor == 0)
				{
					pacDontTouchTheFloor = true;
				}
			

		}
	}

	void Update(){


		if (jumpKeyPressed && !pacDontTouchTheFloor && pacCanNowJump){
			if (Input.GetKeyDown(jumpUp)){
				x = jumpHeight;	
			}
		}

		else if (Input.GetKeyUp(jumpUp) && jumpKeyPressed ){
			jumpKeyPressed = false;
			StartCoroutine(EyTappaHoppBre());
		}
	}
	//____Delay på när man släpper knappen och när PacFaller
	IEnumerator EyTappaHoppBre() {
		yield return new WaitForSeconds(0.1f);
		if (x > 0){
			x = 0.2f;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		y += Time.deltaTime;


		if (x > 0){
		transform.parent.position += transform.up * Time.deltaTime * x;
		}
		//____________For Jump_____________
		if (!pacDash){


			if (pacDontTouchTheFloor && x < 0.5f ){
				
				transform.parent.position += -transform.up * Time.deltaTime * -x;
				
				if (x > -7){
					x -= 7*Time.deltaTime;
				}
				
			}
			
			if (x > 0){
					x -= 7*Time.deltaTime;
				if( x < 0){
					x = 0;
				}
			}
		}
	}
}
