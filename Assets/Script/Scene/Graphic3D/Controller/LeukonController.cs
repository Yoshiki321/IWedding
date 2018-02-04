using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeukonController : BaseController
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
		Light[] lights = transform.GetComponentsInChildren<Light> ();

		foreach(Light l in lights){
			l.enabled = true;
		}
			
        transform.Find("Glass").GetComponent<Renderer>().material = material_on;
        _open = true;
    }

    public void CloseLight()
    {
		Light[] lights = transform.GetComponentsInChildren<Light> ();

		foreach(Light l in lights){
			l.enabled = false;
		}

        transform.Find("Glass").GetComponent<Renderer>().material = material_off;
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
