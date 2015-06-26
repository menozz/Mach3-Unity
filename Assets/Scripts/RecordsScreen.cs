using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class RecordsScreen : MonoBehaviour {

	public int startX =20;
	public int startY = 230;
	public int shagY = -30;
	int y=0;

	public GameObject hScoreField;
	GameObject canv;

	// Use this for initialization
	void Start () {
		GameObject hRecord;
		canv = GameObject.Find ("Canvas1");

		List<GoalStr> theGalaxies = new List<GoalStr>();
		
//		foreach (Galaxy theGalaxy in theGalaxies)
//		{
//			Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears);
//		}

		string pattern = @";";
		//Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

		int j = 0;
		while(PlayerPrefs.HasKey("game_" + j))
		{

			string[] lines = Regex.Split(PlayerPrefs.GetString("game_" + j), pattern);

			theGalaxies.Add(new GoalStr(){Date_ = lines[0],Goals_=int.Parse(lines[1])});
			j++;
		}

		theGalaxies.Sort(delegate(GoalStr x,GoalStr y)
		         {
			return y.Goals_.CompareTo(x.Goals_ );
		});

		j = 0;
		while(PlayerPrefs.HasKey("game_" + j))
		{
			hRecord =(GameObject) Instantiate(hScoreField, new Vector3(startX, startY+y, 0), Quaternion.identity); 
			hRecord.transform.SetParent(canv.transform,false);
			hRecord.transform.GetChild(0).GetComponent<Text>().text = theGalaxies[j].Date_;
			hRecord.transform.GetChild(1).GetComponent<Text>().text = theGalaxies[j].Goals_+"";
			y=y+shagY;
			j++;
		}


	}


	
	// Update is called once per frame
	void Update () {
	
	}


	public class GoalStr{
		public string Date_ { get; set; }
		public int Goals_ { get; set; }
		public GoalStr(){
		
		}
	}
}
