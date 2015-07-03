using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CSVReader  {

	List<GoalStr> theGalaxies;



	public List<GoalStr> getList ()
	{
		theGalaxies = new List<GoalStr>();

		StreamReader reader = new StreamReader(File.OpenRead(@"C:\test.csv"));
	
		while (!reader.EndOfStream)
		{
			string line = reader.ReadLine();
			string[] values = line.Split(';');
//			Debug.Log(line);
			theGalaxies.Add(new GoalStr(){Date_ = values[0],Goals_=int.Parse(values[1])});
		}

		theGalaxies.Sort(delegate(GoalStr x,GoalStr y)
		                 {
			return y.Goals_.CompareTo(x.Goals_ );
		});

		return theGalaxies;
	}
}



