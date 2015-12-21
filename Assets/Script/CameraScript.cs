using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {



	//inställnignar för att underlätta testande
	public float followX;
	public float followY;
	public float followZ;
	public float distanceY;
	public float distanceZ;
	public float camerSpeed;

	//fkn allt annat lmao
	Vector3 camPos;
	Vector3 pacPos;
	Vector3 forwardSnap;
	Vector3 backSnap;
	Vector3 currentTarget;

	public GameObject pacman;
	public GameObject forwardTP;
	public GameObject backTP;

	bool moving;
	Vector3 velocity = Vector3.zero;
	Vector3 targetX;
	Vector3 targetY;

	void Start ()
	{
		pacPos = pacman.transform.position;
		transform.position = new Vector3 (pacPos.x, pacPos.y + distanceY, pacPos.z - distanceZ);
		forwardSnap = forwardTP.transform.position;
		backSnap = backTP.transform.position;
		currentTarget = forwardSnap;
	}

//	void Update ()
//	{
//		camPos = transform.position;
//		pacPos = pacman.transform.position;
//
//		Vector3 targetX = new Vector3 (pacPos.x + centerCamera, camPos.y, camPos.z);
//		Vector3 targetZ = new Vector3 (camPos.x, camPos.y, pacPos.z - distanceZ);
//		Vector3 targetY = new Vector3 (camPos.x, pacPos.y + distanceY, camPos.z);
//
//
//			
//	}
//
	void Update () {


		camPos = transform.position;
		pacPos = pacman.transform.position;
		forwardSnap = forwardTP.transform.position;
		backSnap = backTP.transform.position;
		Vector3 targetX = new Vector3 (pacPos.x, camPos.y, camPos.z);
		//Vector3 targetZ = new Vector3 (camPos.x, camPos.y, pacPos.z - distanceZ);
		Vector3 targetY = new Vector3 (camPos.x, pacPos.y + distanceY, camPos.z);


			transform.position = Vector3.SmoothDamp (transform.position, targetX, ref velocity, 0.3f);



		if (pacPos.x / camPos.x > followX) {

			moving = true;

		} else if (camPos.x / pacPos.x > followX) {

			moving = true;

		} else {
			moving = false;
		}  

//		if (pacPos.z / camPos.z > followZ) {
//
//			transform.position = Vector3.Lerp (camPos, targetZ, 0.03f);
//
//
//		} else if (camPos.z / pacPos.z > followZ) {
//
//			transform.position = Vector3.Lerp (camPos, targetZ, 0.03f);
//
//		}



		if (pacPos.y / camPos.y > followY) {
			
			transform.position = Vector3.Lerp (camPos, targetY, 0.03f);

		} else if (camPos.y / pacPos.y > followY) {
			
			transform.position = Vector3.Lerp (camPos, targetY, 0.03f);
		}
	}
}
