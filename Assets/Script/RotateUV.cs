using UnityEngine;
using System.Collections;

public class RotateUV : MonoBehaviour {

	private float rotateSpeed = .1f;
	private float currentSpeed = 0;
	public Texture texture ;

	private Material m;

	void Start() {

		Shader s = Shader.Find("Custom/RotateUV"); 

		m = new Material (s);

		m.SetFloat("_RotationSpeed", 20);

		m.mainTexture = texture;
		GetComponent<MeshRenderer>().material = m;
	}

	void Update() {
		currentSpeed += rotateSpeed;
		m.SetFloat("_RotationSpeed", currentSpeed);
	}
}