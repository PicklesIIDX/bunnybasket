using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RERT {

	float minBoundsX = 0;
	float maxBoundsX = 100;
	float minBoundsY = 1;
	float maxBoundsY = 1;
	float minBoundsZ = 0;
	float maxBoundsZ = 100;


	public RERT(float minX, float maxX, float minY, float maxY, float minZ, float maxZ){
		minBoundsX = minX;
		maxBoundsX = maxX;
		minBoundsY = minY;
		maxBoundsY = maxY;
		minBoundsZ = minZ;
		maxBoundsZ = maxZ;
	}

	public List<Vector3> BuildPath(Vector3 start, Vector3 goal){
		List<Vector3> path = new List<Vector3>();
		path.Add(start);
		int iterations = 0;
		while(iterations < 100){
			if(AddPointToPath(start, goal, start, ref path)){
				break;
			}
			iterations ++;
		}
		PrintString(path);
		return path;
	}

	bool AddPointToPath(Vector3 start, Vector3 goal, Vector3 currentPoint, ref List<Vector3> path){
		
		// place point
		Vector3 newPoint = GetRandomPoint(minBoundsX, maxBoundsX, minBoundsY, maxBoundsY, minBoundsZ, maxBoundsZ);
		// check to start
		if(PathIsClear(newPoint, currentPoint)){
//			Debug.LogWarning("Path was clear for point: " + newPoint);
			// add to path
			path.Add(newPoint);
			// check to goal
			if(PathIsClear(newPoint, goal)){
				// add goal to path
				path.Add(goal);
				// return path
				return true;
			} else {
				// get another point
				return AddPointToPath(start, goal, newPoint, ref path);
			}
		} else {
			// discard
//			Debug.LogWarning("Discaring point: " + newPoint);
			return false;
		}
	}

	private Vector3 GetRandomPoint(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax){
		return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
	}

	bool PathIsClear(Vector3 pointA	, Vector3 pointB){
		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(pointA, pointB-pointA, out hit, Vector3.Distance(pointA, pointB), ~(1 << LayerMask.NameToLayer("Bunny")) )){
			return false;
		}
		return true;
	}


	string PrintString(List<Vector3> pathList){
		string pathEntries = "Found path:\n";
		for(int p = 0; p < pathList.Count; p ++){
			pathEntries += p + ": " + pathList[p].ToString() + "\n";
		}
		Debug.LogWarning("[RERT.cs]: " + pathEntries);
		return pathEntries;
	}

}
