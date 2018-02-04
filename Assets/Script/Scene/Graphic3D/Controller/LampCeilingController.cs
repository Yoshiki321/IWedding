using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCeilingController : BaseController 
{
	public Material material_on;
	public Material material_off;

	private bool _open;

	// Use this for initialization
	void Start()
	{
		_open = false;
	}

	public void OpenLight()
	{
		transform.Find("Point light").GetComponent<Light>().enabled = true;
		transform.Find("Point light (1)").GetComponent<Light>().enabled = true;
		transform.Find("Point light (2)").GetComponent<Light>().enabled = true;
		transform.Find("Point light (3)").GetComponent<Light>().enabled = true;
		transform.Find("Point light (4)").GetComponent<Light>().enabled = true;
		transform.Find("Point light (5)").GetComponent<Light>().enabled = true;

		transform.GetComponent<Renderer>().material = material_on;
		_open = true;
	}

	public void CloseLight()
	{
		transform.Find("Point light").GetComponent<Light>().enabled = false;
		transform.Find("Point light (1)").GetComponent<Light>().enabled = false;
		transform.Find("Point light (2)").GetComponent<Light>().enabled = false;
		transform.Find("Point light (3)").GetComponent<Light>().enabled = false;
		transform.Find("Point light (4)").GetComponent<Light>().enabled = false;
		transform.Find("Point light (5)").GetComponent<Light>().enabled = false;

		transform.GetComponent<Renderer>().material = material_off;
		_open = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void Interaction()
	{
		if (_open)
		{
			CloseLight();
		}
		else
		{
			OpenLight();
		}
	}
}
