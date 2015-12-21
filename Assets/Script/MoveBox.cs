﻿using UnityEngine;
using System.Collections;

public class MoveBox : MonoBehaviour {

	//__________MOVEMENT________
	public KeyCode upMovement;
	public KeyCode downMovement;
	public KeyCode rightMovement;
	public KeyCode leftMovement;

	public float moveSpeed;
	public int direction;
	public int nextDirection;


	Vector3[] pacMoveDir;
	public bool herculesMode;
	public bool pacManCantMove;

	//_______ JUMP________ 

//	public KeyCode jumpUp;
//	bool jumpOkOrNot = false;
//	bool jumpKeyPressed;
//	public float timer4HowLongJump;
//
//	Vector3 pacPos;
//	float x;
//	float y;
	//__________Dash___________________
	public KeyCode dash = KeyCode.LeftShift;
	public bool dashing;
	public bool pressOnce = true;
	public bool dashActivated = true;

		//_________DrawRay && Real RayCast________

	Vector3[] movement;
	Vector3[] offset ;

	public float raycastLeangthForBool;
	public float raycastForStop;
	public float dictanceFromPac;
	public float movePacFromWallLength = 0.75f;

	RaycastHit hit; 

	Vector3[] vecDir;
	Vector3[] offsetpush;




	void Start () {
		//___________Används till de första Raycasten som sparar boolvärden_______
		movement = new Vector3[] {Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right,Vector3.forward, Vector3.forward};
		offset = new Vector3[]{new Vector3(dictanceFromPac,0,0),new Vector3(dictanceFromPac,0,0),new Vector3(0,0,dictanceFromPac),new Vector3(0,0,dictanceFromPac)};
	
		pacMoveDir = new Vector3[] {transform.forward, -transform.forward, transform.right, -transform.right, Vector3.zero};

		vecDir = new Vector3[] {transform.right,transform.right,transform.forward,transform.forward, Vector3.zero};
		offsetpush = new Vector3[] {new Vector3(0,0,movePacFromWallLength),new Vector3(0,0,-movePacFromWallLength),
			new Vector3(movePacFromWallLength,0,0),new Vector3(-movePacFromWallLength,0,0)};
	}
	void Update () {
	
		//___________Trycka på en knapp?___________

		if(Input.GetKey(upMovement) && !herculesMode){
			nextDirection = 0;
		}else if(Input.GetKey(downMovement) && !herculesMode){
			nextDirection = 1;
		}else if(Input.GetKey(rightMovement)){
			nextDirection = 2;
		}else if(Input.GetKey(leftMovement)){
			nextDirection = 3;
		}
		//___________RayCast som sparar inputvärde______


		//_________________STOPPA_BOLLEN_GENOM_ATT_SÄTTA_DIRECTION_TILL_0__________________________
	

	
		if ( Input.GetKeyDown(dash) && pressOnce && dashActivated){
			StartCoroutine(DashTimer());
		}
	}


	IEnumerator DashTimer(){
		pressOnce = false;
		dashing = true;
		GetComponentInChildren<DownWithPack>().PacIsDashing();
		yield return new WaitForSeconds(0.21f);
		dashing = false;

		GetComponentInChildren<DownWithPack>().PacStopedDashing();
		yield return new WaitForSeconds(1f);
		pressOnce = true; 
	}
	public void PacManActivatedDash(){
		dashActivated = true;
	}


	void FixedUpdate (){

		// ________________PacManMovement__________
		if ( nextDirection != 4){
			if (Physics.Raycast(transform.position + offset[nextDirection], movement[nextDirection], out hit, raycastLeangthForBool) ||
			    Physics.Raycast(transform.position - offset[nextDirection], movement[nextDirection], out hit, raycastLeangthForBool)){
				if((hit.collider.tag == "Wall")){
				}
				
				else {
					GetComponentInChildren<RotatePac>().EyPacMovedFromLocation();
					direction = nextDirection;
					nextDirection = 4;
				}
				
				
			}else{ 
				GetComponentInChildren<RotatePac>().EyPacMovedFromLocation();
				direction = nextDirection;
				nextDirection = 4;
			}
		}

		if ( Physics.Raycast(transform.position, movement[direction], out hit, raycastForStop) && hit.collider.tag == "Wall"){
			pacManCantMove = true;
		}
		else{
			pacManCantMove = false;
		}



		if (herculesMode){
			transform.position += transform.forward * Time.deltaTime * moveSpeed;
		}

		if (!pacManCantMove && !herculesMode) {
			transform.position += pacMoveDir[direction] * Time.deltaTime * moveSpeed;
		}
	

		//_________________Raycast som flyttar Pac från vägg____________________


		if (Physics.Raycast(transform.position + offsetpush[direction], vecDir[direction], out hit, movePacFromWallLength) && hit.collider.tag == "Wall"){
		
				transform.position += -vecDir[direction] * Time.deltaTime * moveSpeed;
		}
		if (Physics.Raycast(transform.position + offsetpush[direction], -vecDir[direction], out hit, movePacFromWallLength) && hit.collider.tag == "Wall"){
				transform.position += vecDir[direction] * Time.deltaTime * moveSpeed;
		
		}

		//______Dash__________________
		if (!pacManCantMove && dashing && !herculesMode){
			transform.position += pacMoveDir[direction] * Time.deltaTime * moveSpeed * 2;
		}
		if (!pacManCantMove && dashing && herculesMode){
			transform.position += transform.forward * Time.deltaTime * moveSpeed * 4;
		}


	}
}

































		
//
//		//________________PacMoveFromTheWall
//
//	
//		//X--
//		Debug.DrawRay(transform.position + new Vector3(0,0,movePacFromWallLength), Vector3.right * movePacFromWallLength/5*4,Color.yellow); 
//		//X++
//		Debug.DrawRay(transform.position + new Vector3(0,0,movePacFromWallLength), -Vector3.right * movePacFromWallLength/5*4,Color.yellow);
//		
//		
//		//Down
//		//X--
//		Debug.DrawRay(transform.position + new Vector3(0,0,-movePacFromWallLength), Vector3.right * movePacFromWallLength/5*4,Color.yellow); 
//		
//		//X++
//		Debug.DrawRay(transform.position + new Vector3(0,0,-movePacFromWallLength), -Vector3.right * movePacFromWallLength/5*4,Color.yellow);
//		
//		//			//Left
//		//Z--
//		Debug.DrawRay(transform.position + new Vector3(movePacFromWallLength,0,0), Vector3.forward * movePacFromWallLength/5*4,Color.yellow); 
//		
//		//Z++
//		Debug.DrawRay(transform.position + new Vector3(movePacFromWallLength,0,0), -Vector3.forward * movePacFromWallLength/5*4,Color.yellow);
//		//
//		//
//		//			//Right
//		//Z--
//		Debug.DrawRay(transform.position + new Vector3(-movePacFromWallLength,0,0), Vector3.forward * movePacFromWallLength/5*4,Color.yellow); 
//		
//		//Z++
//		Debug.DrawRay(transform.position + new Vector3(-movePacFromWallLength,0,0), -Vector3.forward * movePacFromWallLength/5*4,Color.yellow);
//		//		
//		
//		
//		//______________Visar alla StopMovement Raycast______________
//		Debug.DrawRay(transform.position,Vector3.forward * raycastForStop, Color.black);
//		Debug.DrawRay(transform.position,-Vector3.forward * raycastForStop, Color.black);
//		Debug.DrawRay(transform.position,Vector3.right * raycastForStop, Color.black);
//		Debug.DrawRay(transform.position,-Vector3.right * raycastForStop, Color.black);
//
//	
//					if (direction == 1){
//						Debug.DrawRay(transform.position + new Vector3(-dictanceFromPac,0,0),Vector3.forward * raycastLeangthForBool, Color.red);
//						Debug.DrawRay(transform.position + new Vector3(dictanceFromPac,0,0),Vector3.forward * raycastLeangthForBool, Color.red);
//					}
//					if (direction == 2){
//						Debug.DrawRay(transform.position + new Vector3(-dictanceFromPac,0,0),-Vector3.forward * raycastLeangthForBool, Color.red);
//						Debug.DrawRay(transform.position + new Vector3(dictanceFromPac,0,0),-Vector3.forward * raycastLeangthForBool, Color.red);
//					}
//					if (direction == 3){
//						Debug.DrawRay(transform.position + new Vector3(0,0,-dictanceFromPac),Vector3.right * raycastLeangthForBool, Color.red);
//						Debug.DrawRay(transform.position + new Vector3(0,0,dictanceFromPac),Vector3.right * raycastLeangthForBool, Color.red);
//					}
//					if (direction == 4){
//						Debug.DrawRay(transform.position + new Vector3(0,0,-dictanceFromPac),-Vector3.right * raycastLeangthForBool, Color.red);
//						Debug.DrawRay(transform.position + new Vector3(0,0,dictanceFromPac),-Vector3.right * raycastLeangthForBool, Color.red);
//					}




