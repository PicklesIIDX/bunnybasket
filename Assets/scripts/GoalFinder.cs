using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalFinder {

	public void GetGoalFromHiddenViews(){
		Debug.LogError("Not implemented!");
	}

	public Vector3 GetGoalFromTaggedGoals(Vector3 currentGoal){
		GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
		List<Transform> possibleGoals = new List<Transform>();
		for(int g = 0; g < goals.Length; g ++){
			if(goals[g].transform.position != currentGoal){
				possibleGoals.Add(goals[g].transform);
			}
		}
		if(possibleGoals.Count > 0){
			return possibleGoals[Random.Range(0, possibleGoals.Count)].position;
		} else {
			Debug.LogError("Couldn't find a goal!");
			return currentGoal;
		}
	}
}
