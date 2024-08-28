using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject food1;
	public GameObject food2;
	public GameObject food3;
	public GameObject obstacle1;
	public GameObject obstacle2;
	//public Material green;
	//public Material red;

	Perceptron p;

	// Use this for initialization
	void Start () {
		p = GetComponent<Perceptron>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1"))
		{
			GameObject g = Instantiate(food1,Camera.main.transform.position,Camera.main.transform.rotation);
			//g.GetComponent<Renderer>().material = red;
			g.GetComponent<Rigidbody>().AddForce(0,0,500);
			p.SendInput(1,1,1);
		}
		else if(Input.GetKeyDown("2"))
		{
			GameObject g = Instantiate(food2,Camera.main.transform.position,Camera.main.transform.rotation);
			//g.GetComponent<Renderer>().material = green;
			g.GetComponent<Rigidbody>().AddForce(0,0,500);
			p.SendInput(0,1,1);
		}
		else if(Input.GetKeyDown("3"))
		{
			GameObject g = Instantiate(food3,Camera.main.transform.position,Camera.main.transform.rotation);
			//g.GetComponent<Renderer>().material = red;
			g.GetComponent<Rigidbody>().AddForce(0,0,500);
			p.SendInput(1,0,1);
		}
		else if(Input.GetKeyDown("4"))
		{
			GameObject g = Instantiate(obstacle1,Camera.main.transform.position+ new Vector3(0,.7f,0),Camera.main.transform.rotation);
			//g.GetComponent<Renderer>().material = green;
			g.GetComponent<Rigidbody>().AddForce(0,0,500);
			p.SendInput(0,0,0);
		}
		else if(Input.GetKeyDown("5"))
		{
			GameObject g = Instantiate(obstacle2,Camera.main.transform.position+ new Vector3(-.5f,.7f,0),Camera.main.transform.rotation);
			//g.GetComponent<Renderer>().material = green;
			g.GetComponent<Rigidbody>().AddForce(0,0,500);
			p.SendInput(0,0,0);
		}
	}
}

