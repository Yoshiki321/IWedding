using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : BaseController 
{
    public RectPenetratePlane3D rect3D;

	public WallPlaneVO wallPlaneVO;

    private bool _open;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool _isCreate;

	public void CreateHardWall()
	{
		if (!_isCreate)
		{
			HardWall3D h = new HardWall3D ();
			h.Create (wallPlaneVO);
			_isCreate = true;
		}
	}

	public void CreateFrame()
	{
		GameObject frame = new GameObject ();
		frame.AddComponent<Frame3D> ().Create (wallPlaneVO, Infrared.CurrentInfo.point);
	}

    public override void Interaction()
    {
        if (_open)
        {
            _open = false;
            //rect3D.SetCollage(CollageModel.Instance.CreateCollageVO("1002"));
        }
        else
        {
            _open = true;
            //rect3D.SetCollage(CollageModel.Instance.CreateCollageVO("1005"));
        }
    }
}
