using UnityEngine;
using System.Collections;

public class LoadLinks : MonoBehaviour {

	// Скрипт с командами перехода на различные экраны и по ссылке, при помощи него срабатывают действия по нажатию кнопок с главного экрана

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
