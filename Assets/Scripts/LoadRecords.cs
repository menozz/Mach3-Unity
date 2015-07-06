using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class LoadRecords{

	List<GoalStr> recList;

	public LoadRecords(){
	}

	public void initRecordList(){
	

		if (Application.platform == RuntimePlatform.Android){
			PlayerPrefs.SetString ("game_0", "6/19/2015 11:19:56 PM;100");

	    } else{
	
		if (!PlayerPrefs.HasKey ("game_0")) {
			StreamReader reader = new StreamReader(File.OpenRead(@"data.csv"));
			
			int i=0;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				PlayerPrefs.SetString ("game_" + i, line);
				i++;
			}
		}
	
	
	}
	



	}


	public void initRecScreen(){
		recList = new List<GoalStr>();

		int j = 0;
		while(PlayerPrefs.HasKey("game_" + j))
		{

			string line = PlayerPrefs.GetString("game_" + j);
			string[] values = line.Split(';');
			recList.Add(new GoalStr(){Date_ = values[0],Goals_=int.Parse(values[1])});
			j++;
		}
		
		recList.Sort(delegate(GoalStr x,GoalStr y)
		                 {
			return y.Goals_.CompareTo(x.Goals_ );
		});
	}



	public List<GoalStr> getList(){
		return recList;
	}

}
