using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform carTransform;
	[Range(1, 10)]
	[SerializeField] private float followSpeed = 2;
	[Range(1, 10)]
	[SerializeField] private float lookSpeed = 5;
	[SerializeField] private float borderSize;
	private Vector3 initialCameraPosition;

	void Start(){
		initialCameraPosition = gameObject.transform.position;
	}

	void LateUpdate()
	{
		//Look at car
		Vector3 _lookDirection = (new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z)) - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

		//Move to car
		Vector3 _targetPos = initialCameraPosition + carTransform.transform.position;
		_targetPos = new Vector3(Mathf.Clamp(_targetPos.x, initialCameraPosition.x - borderSize, initialCameraPosition.x + borderSize),
			_targetPos.y, 
			Mathf.Clamp(_targetPos.z, initialCameraPosition.z - borderSize, initialCameraPosition.z + borderSize));
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

	}

}
