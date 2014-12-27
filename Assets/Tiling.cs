using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {
	
	public int offsetX = 2;			// the offset so that we don't get any weird errors
	
	// these are used for checking if we need to instantiate stuff
	public bool hasARightBuddy = false;

	// one reverse one normal
	private bool reverseScale = false;	
	
	private float spriteWidth = 0f;		// the width of our element
	private Camera cam;

	void Awake () 
	{
		cam = Camera.main;
	}
	
	// Use this for initialization
	void Start () 
	{
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// does it still need buddies? If not do nothing
		if (!hasARightBuddy) {
			// calculate the cameras extend (half the width) of what the camera can see in world coordinates
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
			
			// calculate the x position where the camera can see the edge of the sprite (element)
			float edgeVisiblePositionRight = (transform.position.x + spriteWidth/2) - camHorizontalExtend;

			// checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy ();
				hasARightBuddy = true;
			}
		}

		// remove self if offscreen and behind camera
		removeOffScreenTiles();
	}
	
	// a function that creates a buddy on the side required
	void MakeNewBuddy () {
		reverseScale = !reverseScale;
		// calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (transform.position.x + spriteWidth, transform.position.y, transform.position.z);
		// instantating our new body and storing him in a variable
		Transform newBuddy = Instantiate (transform, newPosition, transform.rotation) as Transform;
		
		// if not tilable let's reverse the x size og our object to get rid of ugly seams
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}
		
		newBuddy.parent = transform.parent;
		newBuddy.name = transform.name;
		
	
	}

	void removeOffScreenTiles()
	{
		float barrierOffset = 4f;
		float endOfSpriteX = transform.position.x + spriteWidth/2;
		float frustumLeftX = cam.transform.position.x - (cam.orthographicSize * Screen.width/Screen.height + barrierOffset);
		if(endOfSpriteX < frustumLeftX)
			Destroy(gameObject);
	}
}
