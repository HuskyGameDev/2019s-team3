using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
	
	public Transform player;
	public Vector3[] respawns = new Vector3[5];
	private GameManager gameManager;
	private float immunityTimer;
	public float immuneTime;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.GetHearts() <= 0 || player.position.y < -50f) {
			gameManager.RemoveOneUp();
			if (gameManager.GetOneUp() <= 0) {
				CloseGame();
			}
			gameManager.SetImmunity(true);
			player.position = ClosestSpawn();
			gameManager.SetHearts(3);
		}
		if(gameManager.GetImmunity() && immunityTimer < immuneTime) {
			immunityTimer += Time.deltaTime;
		} else {
			gameManager.SetImmunity(false);
			immunityTimer = 0.0f;
		}
	}
	
	private void CloseGame() {
	#if UNITY_EDITOR
		// Application.Quit() does not work in the editor
		UnityEditor.EditorApplication.isPlaying = false;
	#else
		Application.Quit();
	#endif
	}
	
	private Vector3 ClosestSpawn() {
		float closestDistance = 99999f;
		Vector3 closestRespawnPoint = respawns[0]; // default back to main spawn
		foreach (Vector3 respawnPoint in respawns) {
			float distance = Vector3.Distance(respawnPoint, player.position);
			if (distance < closestDistance) {
				closestDistance = distance;
				closestRespawnPoint = respawnPoint;
			}
		}
		return closestRespawnPoint;
	}
}