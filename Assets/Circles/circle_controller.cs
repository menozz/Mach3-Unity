using UnityEngine;
using System.Collections;

public class circle_controller : MonoBehaviour {
	private Animator anim;
	public bool firsStart = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		//anim.SetBool("isClicked", false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void FixedUpdate()
	{

	}

	public void destroyed(){
		anim.SetBool("isClicked", true);

	}

	public void firsStartOn(){
		anim.SetBool("firstStart", true);
		firsStart = true;

	}

	public void firsStartOff(){
		anim.SetBool("firstStart", false);
		firsStart = false;
	}
}
