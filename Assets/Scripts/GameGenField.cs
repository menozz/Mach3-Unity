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
	public float delay = 0.35f;


	GameObject deletedObj;
	GameObject panel1;
	GameObject panelConfirmExit;

	GameObject btnGoBack;

	int goal = 0;
	public int shags = 15;


	GameObject goalLabel, shagLabel;

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


		

	void Update () {
		Vector2 worldPoint;
		RaycastHit2D hit;

		if (GameManager.Instance.firstGame) {
			if (Input.GetMouseButtonUp(0) && Time.time > nextUsage && GameManager.Instance.activeGame){
				worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
				hit = Physics2D.Raycast( worldPoint, Vector2.zero );

				if(hit.collider != null && hit.transform.GetComponent<circle_controller>().firsStart==true)
				{
					hit.transform.GetComponent<circle_controller>().firsStartOff();
					GameManager.Instance.firstGame= false;
					StartCoroutine(runNormalGame(hit));
				}
			}

		}else if (Input.GetMouseButtonUp(0) && Time.time > nextUsage && GameManager.Instance.activeGame) {
			worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			hit = Physics2D.Raycast( worldPoint, Vector2.zero );

			if(hit.collider != null)
			{
				gameNormal(hit);
			}
		}
	






	}

	IEnumerator runNormalGame(RaycastHit2D hit) {
		yield return new WaitForSeconds(0.5f);
		gameNormal (hit);
	}

	void gameNormal(RaycastHit2D hit){
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
			}
	}



	IEnumerator deleteAll(RaycastHit2D hit) {
		hit.transform.parent = deletedObj.transform;
		for (int i=0; i<deletedObj.transform.childCount; i++) {
			deletedObj.transform.GetChild(i).GetComponent<circle_controller>().destroyed();
		}
		yield return new WaitForSeconds(delay);
		deletedObj.transform.position = new Vector2(-100,-100);
		respawnNewCircles();
		addPoints((deletedObj.transform.childCount)*10);
		calcShags((deletedObj.transform.childCount-1));
		for (int i=0; i<deletedObj.transform.childCount; i++) {
			Destroy (deletedObj.transform.GetChild(i).gameObject);
		}
		deletedObj.transform.position = new Vector2(-20,-20);
	}



	IEnumerator deleteOne(RaycastHit2D hit) {
		deletedObj.transform.DetachChildren();
		hit.transform.parent = deletedObj.transform;
		hit.transform.GetComponent<circle_controller>().destroyed();
		yield return new WaitForSeconds(delay);
		deletedObj.transform.position = new Vector2(-100,-100);
		respawnNewCircles();
		addPoints(10);
		calcShags(-1);
		for (int i=0; i<deletedObj.transform.childCount; i++) {
			Destroy (deletedObj.transform.GetChild(i).gameObject);
		}
		deletedObj.transform.position = new Vector2(-20,-20);
	}

	void addPoints(int addP){
		goal = goal + addP;
		goalLabel.GetComponent<Text>().text = goal + "";
	}

	void calcShags(int shag){
		shags = shags + shag;
		shagLabel.GetComponent<Text>().text = shags + "";
		if (shags < 1) gameOver ();
	}


	void gameOver(){
		LoadRecords lrec = new LoadRecords();
		lrec.initRecScreen ();
		List<GoalStr> recList = lrec.getList ();
		if (goal > recList [0].Goals_) {

				int j = 0;
			    while (PlayerPrefs.HasKey("game_" + j))
			    {
					j++;
				}
				PlayerPrefs.SetString ("game_" + j, DateTime.Now + ";" + goal);
			    GameManager.Instance.showNewScrorec = true;
				Application.LoadLevel (3);
			} else {
			    GameManager.Instance.activeGame = false;
				btnGoBack = GameObject.Find ("ButtonGoBack");
				btnGoBack.SetActive (false);
				panel1.SetActive (true);
			}

}

	void respawnNewCircles(){
		float x=0.5f, y=2.8f;

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
				hit = hitNew;
				hit.collider.enabled = false;
				hitNew = Physics2D.Raycast (hit.transform.position, -hit.transform.right);
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
			Instantiate(blue, new Vector2(x,y), Quaternion.identity);
			break;
		case 2:
			Instantiate(yellow, new Vector2(x, y), Quaternion.identity);
			break;
		case 3:
		     Instantiate(green, new Vector2(x, y), Quaternion.identity);
			break;
		case 4:
			Instantiate(red, new Vector2(x, y), Quaternion.identity);
			break;
		}
	}
}
