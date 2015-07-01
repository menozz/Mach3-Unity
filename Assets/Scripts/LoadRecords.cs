using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LoadRecords{

	List<GoalStr> theGalaxies;

	public LoadRecords(){
		Start ();
	}

	void Start(){
		theGalaxies = new List<GoalStr>();
		
		
		string pattern = @";";
		
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
	}

	public List<GoalStr> getList(){
		return theGalaxies;
	}
	
}
