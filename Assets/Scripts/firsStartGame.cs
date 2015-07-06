using UnityEngine;
using System.Collections;

public class firsStartGame : MonoBehaviour {

	public float startX = 0.5f;
	public float startY = -2.2f;
	public float firstDelay = 1.9f;

	// Use this for initialization
	void Start () {
		StartCoroutine(selectRndCircle());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator selectRndCircle() {
		
		int rndX = UnityEngine.Random.Range(0, 5);
		int rndY = UnityEngine.Random.Range(0, 5);	
		GameManager.Instance.fX = startX + rndX;
		GameManager.Instance.fY = startY + rndY;
		yield return new WaitForSeconds(firstDelay);
		RaycastHit2D hit = Physics2D.Raycast( new Vector2(startX+rndX,startY+rndY), Vector2.zero );
		hit.transform.GetComponent<circle_controller> ().firsStartOn ();
	}


}
