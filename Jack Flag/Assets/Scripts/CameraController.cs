using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 0.125f;
    private float xmin = 5f;
    private float xmax = 27f;
	private float ymin = 5f;
    private float ymax = 27f;
	public Vector3 offset;

	void Start() {
		FixedUpdate();
	}

	void FixedUpdate() {
		Vector3 desiredPosition = target.position + offset;
		if(desiredPosition.x <= xmin) {
			desiredPosition.x = xmin;
		} else if(desiredPosition.x > xmax) {
			desiredPosition.x = xmax;
		}
		if(desiredPosition.y <= ymin) {
			desiredPosition.y = ymin;
		} else if(desiredPosition.y > ymax) {
			desiredPosition.y = ymax;
		}
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
