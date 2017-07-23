using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLicorne : MonoBehaviour
{


	public GameObject licorne;
	Vector3 temp;

	void Update () 
	{
		temp =	new Vector3(transform.position.x,transform.position.y, licorne.transform.position.z); 
		transform.position = temp;
	}
}
