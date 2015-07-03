using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CSVReader  {

	List<GoalStr> theGalaxies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<GoalStr> getList ()
	{
		theGalaxies = new List<GoalStr>();
//		if(File.OpenRead(@"C:\test.csv").CanRead){Debug.Log("true");}
//		else{ Debug.Log("false");};

		StreamReader reader = new StreamReader(File.OpenRead(@"C:\test.csv"));
		List<string> listA = new List<string>();
		List<string> listB = new List<string>();
		while (!reader.EndOfStream)
		{
			string line = reader.ReadLine();
			string[] values = line.Split(';');
//			Debug.Log(line);
			listA.Add(values[0]);Debug.Log(values[0]);
			listB.Add(values[1]);Debug.Log(values[1]);
			theGalaxies.Add(new GoalStr(){Date_ = values[0],Goals_=int.Parse(values[1])});
		}

		theGalaxies.Sort(delegate(GoalStr x,GoalStr y)
		                 {
			return y.Goals_.CompareTo(x.Goals_ );
		});

		return theGalaxies;
	}
}



