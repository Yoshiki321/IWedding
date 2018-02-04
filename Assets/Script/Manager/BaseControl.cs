using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControl : MonoBehaviour {

	protected float mouseMoveX;
	protected float mouseMoveY;
	private float _lastDownX;
	private float _lastDownY;

    /// <summary>
    /// 初始鼠标状态 
    /// </summary>
    protected void InitialMousePoint()
	{
		_lastDownX = Input.mousePosition.x;
		_lastDownY = Input.mousePosition.y;
	}
}
