using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace BuildManager
{
    public class SceneManager : MonoBehaviour
    {
        public static string Version = "1.0.1";

        public static string ProjectName;
        public static string ProjectURL;
        public static string ProjectModelURL;
        public static string ProjectPictureURL;

        public Graphics2D Graphics2D;
        public Graphics3D Graphics3D;
        public Camera CameraUI;
        public Camera Camera2D;
        public GameObject Camera3DLayer;
        public FirstPersonController FirstPersonCamera;
        public GameObject EditorCamera;
        public GameObject VRCameraRole;
        public Camera VRCamera;
        public GameObject UILayer;

        public BrushManager brushManager;
        public ControlManager controlManager;
        public Control3Manager control3Manager;
        public KeyboardManager keyboardManager;

        public MouseManager mouseManager;
        public Mouse3Manager mouse3Manager;

        public static Camera[] cameras;

        public RTEditor.EditorObjectSelection editorObjectSelection;
        public RTEditor.EditorGizmoSystem editorGizmoSystem;

        private static SceneManager _Instance;

        public static SceneManager Instance
        {
            get { return _Instance; }
        }

        void Start()
        {
            _Instance = this;

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = Color.white;
            RenderSettings.ambientEquatorColor = Color.white;
            RenderSettings.ambientGroundColor = Color.white;

            InitConfig();
            new Context();
            InitComponent();
            InitUI();
            InitCamera();
            InitSound();

            InitGrapics2();

            _surfaceLight = true;
        }

        private void InitCamera()
        {
            UnityEngine.XR.XRSettings.enabled = false;

            cameras = new Camera[]
            {
            FirstPersonCamera.GetComponentInChildren<Camera>(),
            //HoverCamera.GetComponentInChildren<Camera>(),
            EditorCamera.GetComponentInChildren<Camera>(),
            VRCamera
            };
        }

        private void InitGrapics2()
        {
            Graphics2D.gameObject.SetActive(false);
            Graphics3D.gameObject.SetActive(false);
        }

        private void InitUI()
        {
            UIManager.OpenUI(UI.MainPanel);
        }

        private void InitSound()
        {
            GetComponent<AudioSource>().Play();
        }

        private void InitConfig()
        {
            new ItemManager();
            new SurfaceManager();
            new TexturesManager();
            new CookieManager();
            new DrawShapeManager();
        }

        private void InitComponent()
        {
            brushManager = new BrushManager();
            controlManager = new ControlManager();
            control3Manager = new Control3Manager();
            keyboardManager = new KeyboardManager();

            mouseManager = new MouseManager();
            mouse3Manager = new Mouse3Manager();

            gameObject.AddComponent<SceneController>();
            gameObject.AddComponent<CameraRay>();

            CameraVO vo = new CameraVO();
            vo.id = "EditorCamera";
            EditorCamera.GetComponent<SceneCamera3D>().Instantiate(vo);
        }

        public Camera Camera3D
        {
            get
            {
                if (EditorCamera && EditorCamera.gameObject.activeSelf)
                {
                    return EditorCamera.GetComponent<Camera>();
                }
                return FirstPersonCamera.transform.Find("FirstPersonCharacter").gameObject.GetComponent<Camera>();
            }
        }

        public static bool visual3 = false;

        void LateUpdate()
        {
            if (controlManager != null && CameraManager.visual == CameraFlags.Two) controlManager.LateUpdate();
        }

        void Update()
        {
            if (_Instance == null) _Instance = this;

            if (CameraManager.visual == CameraFlags.Two)
            {
                mouseManager.Update();
            }
            else
            {
                mouse3Manager.Update();
            }

            keyboardManager.Update();
            brushManager.Update();

            //CameraUI.clearFlags = CameraClearFlags.Depth;

            if (Input.GetKeyDown(KeyCode.C))
            {
                //if (visual == CameraFlags.Look)
                //{
                //    ChangeCamera(CameraFlags.Roam);
                //}
                if (CameraManager.visual == CameraFlags.Roam)
                {
                    CameraManager.ChangeCamera(CameraFlags.Fly);
                }
                else if (CameraManager.visual == CameraFlags.Fly)
                {
                    CameraManager.ChangeCamera(CameraFlags.VR);
                }
                else if (CameraManager.visual == CameraFlags.VR)
                {
                    CameraManager.ChangeCamera(CameraFlags.Roam);
                }
            }

            //if (Input.GetKeyDown(KeyCode.V))
            //{
            //    VRPanoramaCamera.transform.position = EditorCamera.transform.position;
            //    VRPanoramaCamera.transform.rotation = EditorCamera.transform.rotation;
            //    VRPanoramaCamera.SetActive(true);
            //}

            if (Input.GetKeyDown(KeyCode.M))
            {
                RankManager.RankPositionY(Mouse3Manager.selectionItem);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                SetSceneLightActive(!activeSceneLight);
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                UILayer.SetActive(false);
                ScreenCapture.CaptureScreenshot("Screenshot.png");
                Invoke("CaptureScreenshot", .1f);
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                UIManager.OpenUI(UI.LoadModelPanel);
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                CodeManager.GetCombinationCode(Mouse3Manager.selectionItem);
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                if (brushManager.brushMode == BrushManager.BrushMode.Place)
                {
                    brushManager.brushMode = BrushManager.BrushMode.Direct;
                }
                else
                {
                    brushManager.brushMode = BrushManager.BrushMode.Place;
                }
            }
        }

        public void ToggleLightHandle()
        {
            SetSceneLightActive(!activeSceneLight);
        }

        public void TakePhotoHandle()
        {
            UILayer.SetActive(false);
            ScreenCapture.CaptureScreenshot("Screenshot.png");
            Invoke("CaptureScreenshot", .1f);
        }

        private void CaptureScreenshot()
        {
            UILayer.SetActive(true);
        }

        private static bool _surfaceLight;

        public static void SetSceneLightActive(bool value)
        {
            _surfaceLight = value;

            if (_surfaceLight)
            {
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
                RenderSettings.ambientSkyColor = ColorUtils.HexToColor("E1E1E1");
                RenderSettings.ambientEquatorColor = ColorUtils.HexToColor("E1E1E1");
                RenderSettings.ambientGroundColor = ColorUtils.HexToColor("E1E1E1");
            }
            else
            {
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
                RenderSettings.ambientSkyColor = ColorUtils.HexToColor("5D5C65");
                RenderSettings.ambientEquatorColor = ColorUtils.HexToColor("19191A");
                RenderSettings.ambientGroundColor = ColorUtils.HexToColor("464E54");
            }
        }

        public static bool activeSceneLight
        {
            get { return _surfaceLight; }
        }

        /// <summary>
        /// 获取场景里的3D物体（包括墙）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ObjectSprite GetObject3(string id)
        {
            ObjectData data = AssetsModel.Instance.GetObjectData(id);

            if (id == "EditorCamera")
            {
                return SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>();
            }

            ObjectSprite item = null;
            if (data != null)
                item = AssetsModel.Instance.GetObjectData(id).object3;

            if (!item)
            {
                LineData ld = BuilderModel.Instance.GetLineData(id);
                if (ld != null) item = BuilderModel.Instance.GetLineData(id).line3;
            }

            if (!item)
                item = BuilderModel.Instance.GetSurfaceData(id).surface3;

            return item;
        }

        public static List<ObjectData> GetSameAssetItemData(string id)
        {
            List<ObjectData> list = new List<ObjectData>();

            foreach (ObjectData data in AssetsModel.Instance.objectDatas)
            {
                if (data.vo.assetId == id)
                {
                    list.Add(data);
                }
            }

            return list;
        }

        public static void EnabledEditorObjectSelection(bool value)
        {
            if (!value) SceneManager.Instance.editorObjectSelection.ClearSelection(false);
            SceneManager.Instance.editorObjectSelection.gameObject.SetActive(value);
        }
    }
}
