using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed = 20.0f;
	[SerializeField] private bool xAxis = false;
	[SerializeField] private bool yAxis = true;
	[SerializeField] private bool zAxis = false;

	private Quaternion baseRoation;

	private void Awake() {
		baseRoation = transform.rotation;
	}

	void Update () {
		Quaternion rotation = Quaternion.Euler(
			xAxis ? transform.rotation.eulerAngles.x + (speed * Time.deltaTime) : baseRoation.eulerAngles.x,
			yAxis ? transform.rotation.eulerAngles.y + (speed * Time.deltaTime) : baseRoation.eulerAngles.y,
			zAxis ? transform.rotation.eulerAngles.z + (speed * Time.deltaTime) : baseRoation.eulerAngles.z);
		transform.rotation = rotation;
	}
}
