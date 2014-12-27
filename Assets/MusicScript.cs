using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {
	
	public AudioSource[] sources;

	// Use this for initialization
	void Start () 
	{
		sources = GetComponents<AudioSource>();
		sources[Random.Range (0, sources.Length)].Play();
			
	}
}
