using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class RecordsScreen : MonoBehaviour {

	public int startX =20;
	public int startY = 230;
	public int shagY = -30;
	int y=0;

	List<GoalStr> recList;

	public GameObject hScoreField;
	GameObject canv;

	LoadRecords lrec;

	// Use this for initialization
	void Start () {



		GameObject hRecord;
		canv = GameObject.Find ("Canvas1");


		lrec = new LoadRecords();
		lrec.initRecScreen();
		recList = lrec.getList ();
		int j = 0;
		while(PlayerPrefs.HasKey("game_" + j))
		{
			hRecord =(GameObject) Instantiate(hScoreField, new Vector2(startX, startY+y), Quaternion.identity); 
			hRecord.transform.SetParent(canv.transform,false);
			hRecord.transform.GetChild(0).GetComponent<Text>().text = recList[j].Date_;
			hRecord.transform.GetChild(1).GetComponent<Text>().text = recList[j].Goals_+"";
			if(j==0 && GameManager.Instance.showNewScrorec ){
				hRecord.transform.GetComponent<FlashText>().flash();
				GameManager.Instance.showNewScrorec = false;
			}
			y=y+shagY;
			j++;
		}

	}
	
}
