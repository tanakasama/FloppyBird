using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	static public int score;
	static public float controlPointF;
	

	// Use this for initialization
	void Start () 
	{
		score = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(!GameController.isStarted)	
			return;

		if(Input.GetButtonDown("Fire1") )
		{
			move ();
		}

		if(transform.position.y <= -5.5f)
			GameController.restartGame();
	}

	void move()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		GetComponent<Rigidbody>().AddForce (new Vector3(275,200,0), ForceMode.Force);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Space")
			GameController.restartGame();
		else
			score++;
	}

}
