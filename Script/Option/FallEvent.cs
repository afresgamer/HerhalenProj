using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : MonoBehaviour {
	
	[SerializeField]
	float speed = 0;
	[SerializeField,Range(0,10)]
	float distance = 0.5f;
	void Start () 
	{
		transform.position = Vector3.Lerp(transform.position,transform.forward * distance  ,speed* Time.deltaTime);
	}

	void Update () 
	{

	}
}
