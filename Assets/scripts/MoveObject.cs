using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {


	float moveX = 0;
	float moveY = 0;
	[SerializeField]
	float moveSpeed = 10;
	[SerializeField]
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		rigidbody.velocity = new Vector3(moveX * moveSpeed * Time.deltaTime, 
										rigidbody.velocity.y, 
											moveY * moveSpeed * Time.deltaTime);
		
	}

	public void UpdateMove(float newX, float newY){
		moveX = Mathf.Clamp(newX, -1.0f, 1.0f);
		moveY = Mathf.Clamp(newY, -1.0f, 1.0f);
	}

	void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, new Vector3(moveX, 0, moveY));
	}
}
