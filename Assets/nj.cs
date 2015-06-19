using UnityEngine;
using System.Collections;

public class nj : MonoBehaviour {
	public Transform blue;


	// Use this for initialization
	void Start () {
		Transform newCircle = Instantiate(blue, new Vector3(2, 2, 0), Quaternion.identity) as Transform;
		
		newCircle.GetComponent("GameCircle");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
