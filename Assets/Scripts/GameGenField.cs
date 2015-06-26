using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameGenField : MonoBehaviour {

	public GameObject blue;
	public GameObject yellow;
	public GameObject green;
	public GameObject red;

	public float startX = 0.5f;
	public float startY = -2.2f;

	GameObject newCircle;

	float nextUsage;
	public float firstDelay = 1.8f;
	public float delay = 0.5f;

	GameObject deletedObj;
	GameObject panel1;
	GameObject btnGoBack;
	bool activeGame = true;
	
	int goal = 0;
	public int shags = 15;
//	int totalShags=0;

	GameObject goalLabel, shagLabel;

	GameObject gUnit;

	void  Start() {



		nextUsage = Time.time + firstDelay;
		initGameField ();


		goalLabel = GameObject.Find("GoalLabel") as GameObject;
		shagLabel = GameObject.Find("ShagLabel") as GameObject;
		goalLabel.GetComponent<Text>().text = goal + "";
		shagLabel.GetComponent<Text>().text = shags + "";

		panel1 = GameObject.Find ("PanelConfirmExit") as GameObject;
		panel1.SetActive (false);


		deletedObj = new GameObject();
		deletedObj.transform.position = new Vector2(-20,-20);

	}

	void Awake(){

	


	}


		
	// Update is called once per frame
	void Update () {
				if (Input.GetMouseButton(0)&& Time.time > nextUsage && activeGame)
				{
					
					
					
					
					Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
					RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
					
					deletedObj.transform.DetachChildren();
					deletedObj.transform.position = new Vector2(-20,-20);
					
					if(hit.collider != null)
					{
						
						testRight(hit);
						testLeft(hit);
						
						if(deletedObj.transform.childCount>1) 
						{
							hit.transform.parent = deletedObj.transform;
							deletedObj.transform.position = new Vector2(-100,-100);
							respawnNewCircles();
							addPoints((deletedObj.transform.childCount)*10);
							calcShags((deletedObj.transform.childCount-1));
							
						}
						else{
							deletedObj.transform.DetachChildren();
							hit.transform.parent = deletedObj.transform;
							deletedObj.transform.position = new Vector2(-100,-100);
							respawnNewCircles();
							addPoints(10);
							calcShags(-1);
						}
						nextUsage = Time.time + delay;
						//				Debug.Log(hit.collider.name + " " +newBehaviourScript.getType());
						
						
					}
				}
	






	}

	void addPoints(int addP){
		goal = goal + addP;
		goalLabel.GetComponent<Text>().text = goal + "";
	}

	void calcShags(int shag){
		//totalShags++;
		shags = shags + shag;
		shagLabel.GetComponent<Text>().text = shags + "";
		if (shags < 1) gameOver ();
	}


	void gameOver(){
//		PlayerPrefs.SetInt("Player_Score_"+DateTime.Now, goal);
//		PlayerPrefs.SetInt("Player_Shags_"+DateTime.Now, totalShags);
//		PlayerPrefs.Save ();
		activeGame = false;
		btnGoBack = GameObject.Find ("ButtonGoBack");
		btnGoBack.SetActive (false);
		panel1.SetActive (true);

		//write gameresult to userprefs
		int j = 0;
		while(PlayerPrefs.HasKey("game_" + j))
		{
			j++;
		}
		PlayerPrefs.SetString("game_" + j, DateTime.Now+";"+goal);


	}

	void respawnNewCircles(){
		float x=0.5f, y=2.8f;
//		startX = 0;
//		startY = 0;

		for (int i=0; i<5; i++) {
			Vector2 worldPoint = new Vector2 (x+i, y);
			RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
		

			if (hit.collider == null) {
				setRNDCircle (x+i, y);
							}
	
		}
	}


	void testRight (RaycastHit2D hit){

		RaycastHit2D hitNew;

		hit.collider.enabled = false;
		hitNew = Physics2D.Raycast (hit.transform.position, hit.transform.right);
		hit.collider.enabled = true;
		for (int i =0; i<4; i++){
			if (hitNew.collider != null && hit.collider.name == hitNew.collider.name) {
				Vector3 rDir = Quaternion.AngleAxis(-20.0f, Vector3.forward) * hit.transform.right;
				Debug.DrawRay(hit.transform.localPosition, hit.transform.right+rDir*3f, Color.red);
				hitNew.transform.parent = deletedObj.transform;
				//print ("There is " + hit.transform.name);
				hit = hitNew;

				hit.collider.enabled = false;
				hitNew = Physics2D.Raycast (hit.transform.position, hit.transform.right);
				hit.collider.enabled = true;
			}
	}




		}


	void testLeft (RaycastHit2D hit){

		RaycastHit2D hitNew;
		hit.collider.enabled = false;
		hitNew = Physics2D.Raycast (hit.transform.position, -hit.transform.right);
		hit.collider.enabled = true;
		for (int i =0; i<4; i++){
			if (hitNew.collider != null && hit.collider.name == hitNew.collider.name) {
				hitNew.transform.parent = deletedObj.transform;
			//	print ("There is " + hit.transform.name);
				hit = hitNew;
				hit.collider.enabled = false;
				hitNew = Physics2D.Raycast (hit.transform.position, -hit.transform.right);
				hit.collider.enabled = true;
			}
		}

	}

	
	void testUP (RaycastHit2D hit){

		RaycastHit2D hitNew;
		hit.collider.enabled = false;
		hitNew = Physics2D.Raycast (hit.transform.position, hit.transform.up);
		hit.collider.enabled = true;
		for (int i =0; i<4; i++){
			if (hitNew.collider != null && hit.collider.name == hitNew.collider.name) {
				hitNew.transform.parent = deletedObj.transform;
				print ("There is " + hit.transform.name);
				hit = hitNew;
				hit.collider.enabled = false;
				hitNew = Physics2D.Raycast (hit.transform.position, hit.transform.up);
				hit.collider.enabled = true;
			}
		}
		
	}

	void testDOWN (RaycastHit2D hit){
		
		RaycastHit2D hitNew;
		hit.collider.enabled = false;
		hitNew = Physics2D.Raycast (hit.transform.position, -hit.transform.up);
		hit.collider.enabled = true;
		for (int i =0; i<4; i++){
			if (hitNew.collider != null && hit.collider.name == hitNew.collider.name) {
				hitNew.transform.parent = deletedObj.transform;
				print ("There is " + hit.transform.name);
				hit = hitNew;
				hit.collider.enabled = false;
				hitNew = Physics2D.Raycast (hit.transform.position, -hit.transform.up);
				hit.collider.enabled = true;
			}
		}
		
	}





	void initGameField(){


		for (float y = 0; y < 5; y++) {
		for (float x = 0; x < 5; x++) {
				setRNDCircle(startX+x,startY+y);
		}
		}
	}




	void setRNDCircle(float x,float y){
		int rndN = UnityEngine.Random.Range(1, 5);

		switch (rndN) {
		case 1:
			//gUnit = (GameObject)Instantiate(blue, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			gUnit = (GameObject)Instantiate(blue, new Vector2(x,y), Quaternion.identity);
			break;
		case 2:
			//gUnit = (GameObject) Instantiate(yellow, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			gUnit = (GameObject)Instantiate(yellow, new Vector2(x, y), Quaternion.identity);
			break;
		case 3:
			//gUnit = (GameObject) Instantiate(green, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			gUnit = (GameObject)Instantiate(green, new Vector2(x, y), Quaternion.identity);
			break;
		case 4:
			//gUnit = (GameObject) Instantiate(red, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			gUnit = (GameObject)Instantiate(red, new Vector2(x, y), Quaternion.identity);
			break;
		}
	}
}
