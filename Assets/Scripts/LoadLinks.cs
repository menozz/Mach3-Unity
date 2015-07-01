using UnityEngine;
using System.Collections;

public class LoadLinks : MonoBehaviour {

	public void loadRecords()
	{
		Application.LoadLevel(3);
	}

	public void gotoVK(){
		Application.OpenURL ("https://vk.com/sangheli");
	}

	public void loadAbout()
	{
		Application.LoadLevel(1);
	}

	public void loadGame()
	{
		Application.LoadLevel(2);
	}

	public void loadMenu()
	{
		Application.LoadLevel(0);
	}
}
