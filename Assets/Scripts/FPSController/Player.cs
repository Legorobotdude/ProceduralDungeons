	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public PlayerController playerController;
	public Camera camera;
	// Use this for initialization
	void Start () {
		playerController = GetComponent<PlayerController>();
		camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
