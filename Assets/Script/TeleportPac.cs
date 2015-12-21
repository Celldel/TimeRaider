using UnityEngine;
using System.Collections;

public class TeleportPac : MonoBehaviour {
	public Transform[] teleportLocation;

	public KeyCode teleportRight = KeyCode.E;
	public KeyCode teleportLeft = KeyCode.Q;

	int direction;
	public float distanceFromTransform = 0.5f;

	public bool isColliding;

	public LayerMask mask = 5;


	public bool pacActivateTeleport;

	public bool rightIsColliding;
	public bool leftIsColliding;


	//____Raycast_______
	Vector3[] raycastStartpoint;
	Vector3[] raycastDirection;


	RaycastHit[] hitsFromPac;
	RaycastHit[] hits2Pac;

	int pacLayerMask = 1 << 8 | 1 << 9;
	int wallLayermask = 1 << 9;


	// Use this for initialization
	void Start () {
		raycastStartpoint = new Vector3[] {
			teleportLocation[direction].position + new Vector3(0,0,distanceFromTransform),
			teleportLocation[direction].position + new Vector3(0,0,-distanceFromTransform), 
			teleportLocation[direction].position + new Vector3(distanceFromTransform,0,0),
			teleportLocation[direction].position + new Vector3(-distanceFromTransform,0,0)};
		raycastDirection = new Vector3[] {Vector3.forward,-Vector3.forward,Vector3.right,-Vector3.right};
	}

	public void PacActivadetTheTeleport(){
		pacActivateTeleport = true;
	}



	public void PacManWillTeleportInWall(){
		isColliding = true;
	}

	public void PacTeleport(){
		 

		if ( direction != 4){
			transform.position = teleportLocation[direction].position;
		}
	}


	// Gör en Metod i Colliders så de Resetar sig!! GLÖM INTE MARIO!!!! FITTUNGE '

	// Update is called once per frame
	void Update () {
	


			if(pacActivateTeleport && Input.GetKeyDown(teleportRight)){
			direction = GetComponent<MoveBox>().direction;


			if (direction !=4){
				Debug.DrawRay(transform.position,raycastDirection[direction] * 3, Color.green);
				
				Debug.DrawRay(raycastStartpoint[direction],-raycastDirection[direction] * 3, Color.green);

			raycastStartpoint = new Vector3[] {
				teleportLocation[direction].position + new Vector3(0,0,distanceFromTransform),
				teleportLocation[direction].position + new Vector3(0,0,-distanceFromTransform), 
				teleportLocation[direction].position + new Vector3(distanceFromTransform,0,0),
				teleportLocation[direction].position + new Vector3(-distanceFromTransform,0,0)};


				hitsFromPac = Physics.RaycastAll(transform.position,raycastDirection[direction], 3F,  pacLayerMask | wallLayermask);

			hits2Pac = Physics.RaycastAll(raycastStartpoint[direction],-raycastDirection[direction], 3F,  pacLayerMask);

		




			Debug.Log(hitsFromPac.Length + " = hitsFromPac " + hits2Pac.Length + " = hits2Pac ");
			if (hitsFromPac.Length != hits2Pac.Length) {
				PacTeleport();
				Debug.Log("Innan funktionen gick bra");
			}

			}








	 //

	//	if(Physics.Raycast(raycastStartpoint[direction],raycastDirection,   ))
	}
}
}























