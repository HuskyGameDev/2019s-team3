using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {
	public static int score = 0;
	
	public static void addScore(int amount) {
		score += amount;
	}
	
	public static void subtractScore(int amount)  {
		score -= amount;
	}
	
	public static int getScore() {
		return score;
	}
}
