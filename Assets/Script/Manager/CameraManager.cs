using System.Collections;
using BuildManager;
using UnityEngine;
using UnityEngine.VR;

public class CameraManager : EventDispatcher
{
    public static Camera main
    {
        get
        {
            if (visual == CameraFlags.Two)
            {

            }
            if (visual == CameraFlags.Roam)
            {
            }
            if (visual == CameraFlags.Fly)
            {
            }
            if (visual == CameraFlags.VR)
            {
            }

            return SceneManager.Instance.Camera3D;
        }
    }

    public static Vector3 GetCameraForward()
    {
        GameObject cObj = SceneManager.Instance.Camera3D.gameObject;

        Vector3 v;
        RaycastHit hit;
        if (Physics.Raycast(cObj.transform.position, cObj.transform.forward, out hit, 9999, 1 << LayerMask.NameToLayer("Floor")))
        {
            v = hit.point;
        }
        else
        {
            v = cObj.transform.position + (SceneManager.Instance.Camera3D.transform.forward * 5);
        }

        v.y = 0;
        return v;
    }

    public static CameraFlags visual;

    public static void ChangeCamera(CameraFlags value)
    {
        visual = value;

        if (value == CameraFlags.Two)
        {
            SceneManager.Instance.Graphics2D.gameObject.SetActive(true);
            SceneManager.Instance.Graphics3D.gameObject.SetActive(false);
            SceneManager.Instance.Camera3DLayer.gameObject.SetActive(false);
            Cursor.visible = true;

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = Color.white;
            RenderSettings.ambientEquatorColor = Color.white;
            RenderSettings.ambientGroundColor = Color.white;
        }
        else
        {
            SceneManager.Instance.Graphics2D.gameObject.SetActive(false);
            SceneManager.Instance.Graphics3D.gameObject.SetActive(true);
            SceneManager.Instance.Camera3DLayer.gameObject.SetActive(true);

            SceneManager.SetSceneLightActive(SceneManager.activeSceneLight);

            if (value == CameraFlags.Roam)
            {
                //SceneManager.Instance.HoverCamera.gameObject.SetActive(false);
                SceneManager.Instance.FirstPersonCamera.gameObject.SetActive(true);
                SceneManager.Instance.EditorCamera.gameObject.SetActive(false);
                SceneManager.Instance.VRCameraRole.SetActive(false);
                SceneManager.Instance.CameraUI.gameObject.SetActive(false);
                BuilderModel.Instance.SetLinesTransparent(false);
                UnityEngine.XR.XRSettings.enabled = false;
            }
            //if (value == CameraFlags.Look)
            //{
            //	SceneManager.Instance.HoverCamera.gameObject.SetActive(true);
            //	SceneManager.Instance.FirstPersonCamera.gameObject.SetActive(false);
            //	SceneManager.Instance.EditorCamera.gameObject.SetActive(false);
            //	SceneManager.Instance.UICamera.gameObject.SetActive(true);
            //	BuilderModel.Instance.SetLinesTransparent(true);
            //	Cursor.visible = true;
            //}
            if (value == CameraFlags.Fly)
            {
                //SceneManager.Instance.HoverCamera.gameObject.SetActive(false);
                SceneManager.Instance.FirstPersonCamera.gameObject.SetActive(false);
                SceneManager.Instance.VRCameraRole.SetActive(false);
                SceneManager.Instance.EditorCamera.gameObject.SetActive(true);
                SceneManager.Instance.CameraUI.gameObject.SetActive(true);
                BuilderModel.Instance.SetLinesTransparent(true);
                Cursor.visible = true;
                UnityEngine.XR.XRSettings.enabled = false;
            }
            if (value == CameraFlags.VR)
            {
                UnityEngine.XR.XRSettings.enabled = true;
                SceneManager.Instance.FirstPersonCamera.gameObject.SetActive(false);
                SceneManager.Instance.EditorCamera.gameObject.SetActive(false);
                SceneManager.Instance.CameraUI.gameObject.SetActive(false);
                SceneManager.Instance.VRCameraRole.SetActive(true);
                BuilderModel.Instance.SetLinesTransparent(false);
                Cursor.visible = true;

                //SteamVR.enabled = true;
                //SteamVR vr = SteamVR.instance;
                //SteamVR_Render.instance.enabled = true;
            }
        }
    }
}
