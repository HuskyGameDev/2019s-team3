using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {
	
	public float minSurviveFall;
	private CharacterController controller;
	private GameManager gameManager;
	private float airTime = 0f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!controller.isGrounded) {
			airTime += Time.deltaTime;
		}
		if(controller.isGrounded) {
			if(airTime > minSurviveFall) {
				gameManager.RemoveHearts(1);
			}
			airTime = 0;
		}
	}
}
