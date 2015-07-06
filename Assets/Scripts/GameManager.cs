using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
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
