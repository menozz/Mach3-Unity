using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
	GameObject panel1;
	
	
	void Awake () {
		panel1 = GameObject.Find ("PanelConfirmExit");
		panel1.SetActive (false);
	}
	
	void Update() {
		
	}
	
	public void onExitClick(){
		panel1.SetActive (true);
		GameManager.Instance.activeGame = false;
	}
	
	public void closeExitConfirmPanel(){
		panel1.SetActive (false);
		GameManager.Instance.activeGame = true;
	}
	
	public void confirmExit(){
		Application.LoadLevel(0);
	}
}
