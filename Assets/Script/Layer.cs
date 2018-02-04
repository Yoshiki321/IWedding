using UnityEngine;

public class Layer 
{
    public static GameObject LineLayer;

    public static GameObject SurfaceLayer;

	public static GameObject ItemLayer;

	public static void Init()
	{
		LineLayer = new GameObject();
		LineLayer.name = "LineLayer";

		SurfaceLayer = new GameObject();
		SurfaceLayer.name = "SurfaceLayer";

		ItemLayer = new GameObject();
		ItemLayer.name = "ItemLayer";
	}

	public static void Clear()
	{
		GameObject.Destroy (LineLayer);
		GameObject.Destroy (SurfaceLayer);
		GameObject.Destroy (ItemLayer);
	}
}
