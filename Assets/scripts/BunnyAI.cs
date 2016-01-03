using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunnyAI : MonoBehaviour {

	public enum BunnyState{
		NONE,
		HIDING,
		RUNNING
	}

	BunnyState state = BunnyState.NONE;
	GoalFinder goalFinder;
	Vector3 goal = Vector3.zero;
	List<Vector3> path = new List<Vector3>();
	int pathIndex = 0;
	[SerializeField]
	MoveObject moveObject;
	RERT rert;
	[SerializeField]
	float growthFactor = 10.0f;
	[SerializeField]
	float goalDistance = 1.0f;



	// Use this for initialization
	void Start () {
		goalFinder = new GoalFinder();
		ChangeState(BunnyState.HIDING);
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
			case BunnyState.RUNNING:
				if(Vector3.Distance(transform.position, path[pathIndex]) < goalDistance){
					pathIndex ++;
					if(pathIndex >= path.Count){
						ChangeState(BunnyState.HIDING);
						break;
					}
				}
				Vector3 direction = (path[pathIndex] - transform.position).normalized;
				moveObject.UpdateMove(direction.x, direction.z);
				break;	
		}
	}

	public void ChangeState(BunnyState newState){
		state = newState;
		switch(state){
			case BunnyState.HIDING:
				FindGoal();
				break;
			case BunnyState.RUNNING:
				break;
		}
		Debug.Log("[BunnyAI.cs]: Changed state to " + state);
	}

	private void FindGoal(){
		goal = goalFinder.GetGoalFromTaggedGoals(goal);
		Debug.LogWarning("Found goal at " + goal.ToString());
		rert = new RERT(transform.position.x - growthFactor, transform.position.x + growthFactor,
						transform.position.y, transform.position.y,
						transform.position.z - growthFactor, transform.position.z + growthFactor);
		path = rert.BuildPath(transform.position, goal);
		pathIndex = 0;
		ChangeState(BunnyState.RUNNING);
	}

	void OnDrawGizmos(){
		Color color = new Color(0.0f, 1.0f, 1.0f);
		for(int p = 0; p < path.Count; p ++){
			Gizmos.color = color;
			color.r = path.Count / (p+1);
			Gizmos.DrawSphere(path[p], 1);
			if(p > 0){
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(path[p], path[p-1]);
			}
		}
	}
}
