using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    [Tooltip("What camera to poll viewport information from. Uses Camera.main when this is not specified.")]
	public Camera cam;
	[Tooltip("How far the colliders are scaled in the camera's forward direction. Has no effect when 'use3DPhysics' is set to 'false'.")]
	public float depth = 100f;
	[Tooltip("When camera is in perspective mode, what distance to sample at to get the correct bounding box. Has no effect when camera is in orthographic mode."), Range(0f, 1f)]
	public float distanceQuery = 1f;
	[SerializeField, Tooltip("Changes between 2D and 3D collider physics. Can not be adjusted at runtime.")]
	bool use3DPhysics = false;

	bool _use3DPhysics = false;
	float _distanceQuery = 1000f;
	float _skin2D = 0.05f;

	GameObject top, bottom, left, right;
	GameObject[] barriers;

	void Start() {
		if(cam == null)
			cam = Camera.main;

		_use3DPhysics = use3DPhysics;

		if(_use3DPhysics)
			Create3DBarriers();
		else
			Create2DBarriers();
	}

	private void Create2DBarriers() {
		top = GameObject.CreatePrimitive(PrimitiveType.Cube);
		top.name = "Top";
		top.transform.localScale = new Vector3(0f, _skin2D, 0f);

		bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bottom.name = "Bottom";
		bottom.transform.localScale = new Vector3(0f, _skin2D, 0f);

		left = GameObject.CreatePrimitive(PrimitiveType.Cube);
		left.name = "Left";
		left.transform.localScale = new Vector3(_skin2D, 0f, 0f);

		right = GameObject.CreatePrimitive(PrimitiveType.Cube);
		right.name = "Right";
		right.transform.localScale = new Vector3(_skin2D, 0f, 0f);

		barriers = new GameObject[] { top, bottom, left, right };

		foreach(var b in barriers) {
			DestroyImmediate(b.GetComponent<Collider>());
			//no need to render these.
			DestroyImmediate(b.GetComponent<Renderer>());

			b.transform.parent = cam.transform;

			var bc = b.AddComponent<BoxCollider2D>();
			var rb = b.AddComponent<Rigidbody2D>();
			rb.isKinematic = true;
		}
	}

	private void Create3DBarriers() {
		top = GameObject.CreatePrimitive(PrimitiveType.Cube);
		top.name = "Top";
		top.transform.localScale = new Vector3(100f, 0f, 0f);

		bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bottom.name = "Bottom";
		bottom.transform.localScale = new Vector3(100f, 0f, 0f);

		left = GameObject.CreatePrimitive(PrimitiveType.Cube);
		left.name = "Left";
		left.transform.localScale = new Vector3(0f, 100f, 0f);

		right = GameObject.CreatePrimitive(PrimitiveType.Cube);
		right.name = "Right";
		right.transform.localScale = new Vector3(0f, 100f, 0f);

		barriers = new GameObject[] { top, bottom, left, right };

		foreach(var b in barriers) {
			b.transform.parent = cam.transform;
			var rb = b.AddComponent<Rigidbody>();
			rb.isKinematic = true;

			//no need to render these.
			DestroyImmediate(b.GetComponent<Renderer>());
		}

		SetScales();
		SetPositions();
	}

	private void SetScales() {
		var verticalSize = (cam.orthographic) ? cam.orthographicSize * 2.0f : 2.0f * _distanceQuery * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
		var horizontalSize = (cam.orthographic) ? verticalSize * ((float)Screen.width / (float)Screen.height) : verticalSize * cam.aspect;

		top.transform.localScale = (_use3DPhysics) ? new Vector3(horizontalSize, 0f, depth) : new Vector3(horizontalSize, _skin2D, 0f);
		bottom.transform.localScale = (_use3DPhysics) ? new Vector3(horizontalSize, 0f, depth) : new Vector3(horizontalSize, _skin2D, 0f);
		left.transform.localScale = (_use3DPhysics) ? new Vector3(0f, verticalSize, depth) : new Vector3(_skin2D, verticalSize, 0f);
		right.transform.localScale = (_use3DPhysics) ? new Vector3(0f, verticalSize, depth) : new Vector3(_skin2D, verticalSize, 0f);
	}

	private void SetPositions() {
		//Camera rotations mess up the positions
		var camRot = cam.transform.rotation;
		cam.transform.rotation = Quaternion.identity;

		top.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, _distanceQuery));
		bottom.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, _distanceQuery));
		left.transform.position = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, _distanceQuery));
		right.transform.position = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, _distanceQuery));

		foreach(var b in barriers)
			b.transform.localPosition = ZeroZ(b.transform);

		//reset to the real rotation.
		cam.transform.rotation = camRot;
	}

	//Make each cube's position start at the camera's z position and extend outwards.
	private Vector3 ZeroZ(Transform t) {
		var pos = t.localPosition;
		pos.z = (_use3DPhysics) ? t.localScale.z / 2f : 0f;

		return pos;
	}

	void FixedUpdate() {
		if(cam == null)
			cam = Camera.main;

		distanceQuery = Mathf.Clamp01(distanceQuery);
		_distanceQuery = cam.farClipPlane * distanceQuery;
		_distanceQuery = Mathf.Clamp(_distanceQuery, cam.nearClipPlane, cam.farClipPlane);

		SetScales();
		SetPositions();
	}

}
