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
	public float waitForDeleteObj = 1.5f;

	GameObject deletedObj;
	GameObject panel1;
	GameObject panelConfirmExit;

	GameObject btnGoBack;
	//bool activeGame = true;
	
	int goal = 0;
	public int shags = 15;


	GameObject goalLabel, shagLabel;

	//GameObject gUnit;

	void  Start() {



		nextUsage = Time.time + firstDelay;
		initGameField ();


		goalLabel = GameObject.Find("GoalLabel") as GameObject;
		shagLabel = GameObject.Find("ShagLabel") as GameObject;
		goalLabel.GetComponent<Text>().text = goal + "";
		shagLabel.GetComponent<Text>().text = shags + "";

		panel1 = GameObject.Find ("PanelGameOver") as GameObject;
		panel1.SetActive (false);



		deletedObj = new GameObject();
		deletedObj.transform.position = new Vector2(-20,-20);

	}

	void Awake(){




	}


		
	// Update is called once per frame
	void Update () {
		Vector2 worldPoint;
		RaycastHit2D hit;
		//Debug.Log(ScoresManager.Instance.firstGame);
		if (ScoresManager.Instance.firstGame) {
			if (Input.GetMouseButton(0) && Time.time > nextUsage && ScoresManager.Instance.activeGame){
				worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
				hit = Physics2D.Raycast( worldPoint, Vector2.zero );

			//	RaycastHit2D hitTest;

			//	hitTest = Physics2D.Raycast( new Vector2(ScoresManager.Instance.fX,ScoresManager.Instance.fY), Vector2.zero );
				if(hit.collider != null && hit.transform.GetComponent<circle_controller>().firsStart==true)
				{
				//	Debug.Log(hit.transform.GetComponent<circle_controller>().firsStart);
					hit.transform.GetComponent<circle_controller>().firsStartOff();
					ScoresManager.Instance.firstGame= false;
					StartCoroutine(runNormalGame(hit));

				}
			}

		}else if (Input.GetMouseButton(0) && Time.time > nextUsage && ScoresManager.Instance.activeGame) {
			worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			hit = Physics2D.Raycast( worldPoint, Vector2.zero );

			if(hit.collider != null)
			{
				gameNormal(hit);
			}
		}
	






	}

	IEnumerator runNormalGame(RaycastHit2D hit) {
		
		yield return new WaitForSeconds(0.3f);
		gameNormal (hit);
	}

	void gameNormal(RaycastHit2D hit){
	
			
			//					deletedObj.transform.DetachChildren();
			//					deletedObj.transform.position = new Vector2(-20,-20);
			
			
			
			if(hit.collider != null)
			{
				
				
				
				
				
				
				
				testRight(hit);
				testLeft(hit);
				
				if(deletedObj.transform.childCount>1) 
				{
					StartCoroutine(deleteAll(hit));
					
				}
				else{
					
					StartCoroutine(deleteOne(hit));
					
				}
				nextUsage = Time.time + delay;
				//				Debug.Log(hit.collider.name + " " +newBehaviourScript.getType());
				
				
			}

	}

//	void gameNormal(){
//		if (Input.GetMouseButton(0)&& Time.time > nextUsage && ScoresManager.Instance.activeGame)
//		{
//			
//			
//			
//			
//			Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
//			RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
//			
//			
//			
//			//					deletedObj.transform.DetachChildren();
//			//					deletedObj.transform.position = new Vector2(-20,-20);
//			
//			
//			
//			if(hit.collider != null)
//			{
//				
//				
//				
//				
//				
//				
//				
//				testRight(hit);
//				testLeft(hit);
//				
//				if(deletedObj.transform.childCount>1) 
//				{
//					StartCoroutine(deleteAll(hit));
//					
//				}
//				else{
//					
//					StartCoroutine(deleteOne(hit));
//					
//				}
//				nextUsage = Time.time + delay;
//				//				Debug.Log(hit.collider.name + " " +newBehaviourScript.getType());
//				
//				
//			}
//		}
//	}

	IEnumerator deleteAll(RaycastHit2D hit) {



		hit.transform.parent = deletedObj.transform;
		for (int i=0; i<deletedObj.transform.childCount; i++) {
			deletedObj.transform.GetChild(i).GetComponent<circle_controller>().destroyed();
		}

		yield return new WaitForSeconds(waitForDeleteObj);


		deletedObj.transform.position = new Vector2(-100,-100);
		respawnNewCircles();
		addPoints((deletedObj.transform.childCount)*10);
		calcShags((deletedObj.transform.childCount-1));

		deletedObj.transform.DetachChildren();
		deletedObj.transform.position = new Vector2(-20,-20);
	}



	IEnumerator deleteOne(RaycastHit2D hit) {

		hit.transform.GetComponent<circle_controller>().destroyed();

		yield return new WaitForSeconds(waitForDeleteObj);
		deletedObj.transform.DetachChildren();
		hit.transform.parent = deletedObj.transform;
		deletedObj.transform.position = new Vector2(-100,-100);
		respawnNewCircles();
		addPoints(10);
		calcShags(-1);

		deletedObj.transform.DetachChildren();
		deletedObj.transform.position = new Vector2(-20,-20);
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



		LoadRecords lrec = new LoadRecords();
		List<GoalStr> theGalaxies = lrec.getList ();
		if (goal > theGalaxies [0].Goals_) {
			//write gameresult to userprefs
			int j = 0;
			while (PlayerPrefs.HasKey("game_" + j)) {
				j++;
			}
			PlayerPrefs.SetString ("game_" + j, DateTime.Now + ";" + goal);
			ScoresManager.Instance.showNewScrorec = true;
			Application.LoadLevel (3);
		} else {
			ScoresManager.Instance.activeGame = false;
			btnGoBack = GameObject.Find ("ButtonGoBack");
			btnGoBack.SetActive (false);
			panel1.SetActive (true);
		}


	








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

				Instantiate(blue, new Vector2(x,y), Quaternion.identity);
			break;
		case 2:
			//gUnit = (GameObject) Instantiate(yellow, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			// gUnit = (GameObject)Instantiate(yellow, new Vector2(x, y), Quaternion.identity);
			Instantiate(yellow, new Vector2(x, y), Quaternion.identity);
			break;
		case 3:
			//gUnit = (GameObject) Instantiate(green, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
		     Instantiate(green, new Vector2(x, y), Quaternion.identity);
			break;
		case 4:
			//gUnit = (GameObject) Instantiate(red, new Vector3(startX+x, startY+y, 0), Quaternion.identity);
			Instantiate(red, new Vector2(x, y), Quaternion.identity);
			break;
		}
	}
}
