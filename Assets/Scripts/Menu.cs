using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject panel1;


	void Awake () {
		panel1 = GameObject.Find ("PanelConfirmExit");
		panel1.SetActive (false);
	}

	void Update() {

	}

	public void onExitClick(){
		panel1.SetActive (true);
	}

	public void closeExitConfirmPanel(){
		panel1.SetActive (false);
	}

	public void confirmExit(){
		Application.Quit();
	}
}
