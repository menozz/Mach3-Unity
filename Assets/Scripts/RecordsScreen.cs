using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class RecordsScreen : MonoBehaviour {

	public int startX =20;
	public int startY = 230;
	public int shagY = -30;
	int y=0;

	List<GoalStr> theGalaxies;

	public GameObject hScoreField;
	GameObject canv;

	LoadRecords lrec;
	CSVReader csvread;

	// Use this for initialization
	void Start () {



		GameObject hRecord;
		canv = GameObject.Find ("Canvas1");


		//theGalaxies = this.GetComponent<LoadRecords> ().getList ();
		if(!PlayerPrefs.HasKey("game_0")){
			Debug.Log("load from csv");
			csvread = new CSVReader ();
			theGalaxies = csvread.getList();

			foreach(GoalStr fvar in theGalaxies)
			{
				hRecord =(GameObject) Instantiate(hScoreField, new Vector2(startX, startY+y), Quaternion.identity); 
				hRecord.transform.SetParent(canv.transform,false);
				hRecord.transform.GetChild(0).GetComponent<Text>().text = fvar.Date_;
				hRecord.transform.GetChild(1).GetComponent<Text>().text = fvar.Goals_+"";
			
				y=y+shagY;
			}
		}else {
			lrec = new LoadRecords();
			theGalaxies = lrec.getList ();
			int j = 0;
			while(PlayerPrefs.HasKey("game_" + j))
			{
				hRecord =(GameObject) Instantiate(hScoreField, new Vector2(startX, startY+y), Quaternion.identity); 
				hRecord.transform.SetParent(canv.transform,false);
				hRecord.transform.GetChild(0).GetComponent<Text>().text = theGalaxies[j].Date_;
				hRecord.transform.GetChild(1).GetComponent<Text>().text = theGalaxies[j].Goals_+"";
				if(j==0 && ScoresManager.Instance.showNewScrorec ){
					hRecord.transform.GetComponent<FlashText>().flash();
					ScoresManager.Instance.showNewScrorec = false;
				}
				y=y+shagY;
				j++;
			}
		
		};


//
//		if (ScoresManager.Instance.showNewScrorec) {
//			hRecord.transform.GetComponent<FlashText>().flash();
//			ScoresManager.Instance.showNewScrorec = false;
//		}



	}

//	public class GoalStr{
//		public string Date_ { get; set; }
//		public int Goals_ { get; set; }
//		public GoalStr(){
//			
//		}
//	}

	



}
