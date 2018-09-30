using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 0.125f;
	private int xmin = 5;
	private int xmax = 26;
	private int ymin = 4;
	private int ymax = 27;
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
