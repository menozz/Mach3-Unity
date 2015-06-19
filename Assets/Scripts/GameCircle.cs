using UnityEngine;
using System.Collections;

public class GameCircle : MonoBehaviour {

	public int type;
	float x=0;
	float y=0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setX(float x){
		this.x = x;
	}

	public void setY(float y){
		this.y = y;
	}

	public int getType(){
		return type;

	}

	public float getX(){
		return x;
	}

	public float getY(){
		return y;
	}
}
