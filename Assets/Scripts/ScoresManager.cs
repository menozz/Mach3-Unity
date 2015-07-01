using UnityEngine;
using System.Collections;

public class ScoresManager : MonoBehaviour
{
	public static ScoresManager Instance { get; private set; }
	public bool showNewScrorec;
	public bool activeGame;
	public bool firstGame=true;
	public float fX;
	public float fY;
	
	public void Awake()
	{
		Instance = this;
	}


}
