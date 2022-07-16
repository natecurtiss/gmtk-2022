using UnityEngine;

public class LookAtCam : MonoBehaviour {

	private void Awake() {
		Canvas _canvas = GetComponent<Canvas>();
		if(_canvas) {
			_canvas.worldCamera = Camera.main;
		}
	}
	void Update(){
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
