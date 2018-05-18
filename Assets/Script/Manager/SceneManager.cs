using Build3D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using NAudio.Wave;
using Build2D;
using System.IO;

namespace BuildManager
{
    public class SceneManager : MonoBehaviour
    {
        public static string Version = "1.0.5";

        public static string ProjectName;
        public static string ProjectURL;
        public static string ProjectModelURL;
        public static string ProjectPictureURL;
        public static string ProjectCombinationURL;

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
        public AudioSourceManager audioSourceManager;
        public ControlManager controlManager;
        public Control3Manager control3Manager;
        public KeyboardManager keyboardManager;

        public MouseManager mouseManager;
        public Mouse3Manager mouse3Manager;

        public static Camera[] cameras;

        public RTEditor.EditorObjectSelection editorObjectSelection;
        public RTEditor.EditorGizmoSystem editorGizmoSystem;

        public static SceneManager Instance { get; private set; }

        void Start()
        {
            Instance = this;

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = Color.white;
            RenderSettings.ambientEquatorColor = Color.white;
            RenderSettings.ambientGroundColor = Color.white;

            InitConfig();
            new Context();
            InitComponent();
            InitUI();
            InitCamera();

            InitGrapics2();

            activeSceneLight = true;
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

        private void InitConfig()
        {
            new ItemManager();
            new HotelManager();
            new SurfaceManager();
            new TexturesManager();
            new CookieManager();
            new DrawShapeManager();
            new SprinkleManager();
        }

        private void InitComponent()
        {
            brushManager = new BrushManager();
            audioSourceManager = new AudioSourceManager();
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
            if (Instance == null) Instance = this;

            if (CameraManager.visual == CameraFlags.Two)
            {
                mouseManager.Update();
            }
            else
            {
                mouse3Manager.Update();
            }

            AssetsModel.Instance.Update();
            keyboardManager.Update();
            brushManager.Update();

            //CameraUI.clearFlags = CameraClearFlags.Depth;

            if (Input.GetKeyDown(KeyCode.C))
            {
                //if (visual == CameraFlags.Look)
                //{
                //    ChangeCamera(CameraFlags.Roam);
                //}
                if (CameraManager.visual == CameraFlags.VR)
                {
                    CameraManager.ChangeCamera(CameraFlags.Fly);
                }
                else if (CameraManager.visual == CameraFlags.Fly)
                {
                    CameraManager.ChangeCamera(CameraFlags.VR);
                }
                //else if (CameraManager.visual == CameraFlags.VR)
                //{
                //    CameraManager.ChangeCamera(CameraFlags.Roam);
                //}
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
                TakePhotoHandle();
            }

            //if (Input.GetKeyDown(KeyCode.F8))
            //{
            //    UIManager.OpenUI(UI.LoadModelPanel);
            //}

            //if (Input.GetKeyDown(KeyCode.F7))
            //{
            //    CodeManager.GetCombinationCode(Mouse3Manager.selectionItem);
            //}

            if (Input.GetKeyDown(KeyCode.F6))
            {
                VisibleEditorItem = !VisibleEditorItem;
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                if (Mouse3Manager.selectedSurface)
                {
                    SurfaceVO svo = Mouse3Manager.selectedSurface.VO as SurfaceVO;
                    svo.height = 20;
                    SurfaceStruct sData = BuilderModel.Instance.GetSurfaceData(svo.id);
                    sData.surface3.VO = svo;
                    foreach (LineVO linevo in svo.linesVO)
                    {
                        LineStruct linedata = BuilderModel.Instance.GetLineData(linevo.id);
                        linedata.vo.height = 20;
                        linedata.line3.VO = linedata.vo;
                        linedata.line3.UpdateNow();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                CommandMap.DispatcherEvent(new FileEvent(FileEvent.LOAD_SOUND));
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                audioSourceManager.Volume -= .1f;
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                audioSourceManager.Volume += .1f;
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                UIManager.OpenUI(UI.InventoryPanel);
            }

        }

        /// <summary>
        /// 打开笔刷
        /// </summary>
        public void OpenBrushHandle()
        {
            brushManager.brushMode = BrushManager.BrushMode.Direct;
        }

        /// <summary>
        /// 关闭笔刷
        /// </summary>
        public void CloseBrushHandle()
        {
            brushManager.brushMode = BrushManager.BrushMode.Place;
        }

        /// <summary>
        /// 切换笔刷
        /// </summary>
        public void ToggleBrushMode()
        {
            if (brushManager.brushMode == BrushManager.BrushMode.Place)
            {
                TopToolPanel.Instance.CloseBrush();
                brushManager.brushMode = BrushManager.BrushMode.Direct;
            }
            else
            {
                TopToolPanel.Instance.OpenBrush();
                brushManager.brushMode = BrushManager.BrushMode.Place;
            }
        }

        /// <summary>
        /// 切换3D视角模式
        /// </summary>
        public void ToggleVRMode()
        {
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

        /// <summary>
        /// 切换灯光
        /// </summary>
        public void ToggleLightHandle()
        {
            SetSceneLightActive(!activeSceneLight);
        }

        /// <summary>
        /// 打开灯光
        /// </summary>
        public void OpenLightHandle()
        {
            SetSceneLightActive(true);
        }

        /// <summary>
        /// 关闭灯光
        /// </summary>
        public void CloseLightHandle()
        {
            SetSceneLightActive(false);
        }

        /// <summary>
        /// 截图
        /// </summary>
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

        /// <summary>
        /// 环境光关闭
        /// 当环境光关闭时道具灯才会打开
        /// </summary>
        /// <param name="value"></param>
        public static void SetSceneLightActive(bool value)
        {
            activeSceneLight = value;

            if (activeSceneLight)
            {
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
                RenderSettings.ambientSkyColor = ColorUtils.HexToColor("CACACA");
                RenderSettings.ambientEquatorColor = ColorUtils.HexToColor("CACACA");
                RenderSettings.ambientGroundColor = ColorUtils.HexToColor("CACACA");
            }
            else
            {
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
                RenderSettings.ambientSkyColor = ColorUtils.HexToColor("5D5C65");
                RenderSettings.ambientEquatorColor = ColorUtils.HexToColor("19191A");
                RenderSettings.ambientGroundColor = ColorUtils.HexToColor("464E54");
            }

            for (int i = 0; i < AssetsModel.Instance.itemDatas.Count; i++)
            {
                SpotlightComponent[] ss = AssetsModel.Instance.itemDatas[i].item3.GetComponentsInChildren<SpotlightComponent>();
                foreach (SpotlightComponent sc in ss)
                {
                    Light[] ls = sc.GetComponentsInChildren<Light>();
                    foreach (Light l in ls)
                    {
                        l.enabled = !activeSceneLight;
                    }
                }
            }
        }

        public static bool activeSceneLight { get; private set; }

        /// <summary>
        /// 获取场景里的3D物体（包括墙）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ObjectSprite GetObject3(string id)
        {
            ItemStruct data = AssetsModel.Instance.GetItemData(id);

            if (id == "EditorCamera")
            {
                return Instance.EditorCamera.GetComponent<SceneCamera3D>();
            }

            ObjectSprite item = null;
            if (data != null)
                item = AssetsModel.Instance.GetItemData(id).item3;

            if (!item)
            {
                LineStruct ld = BuilderModel.Instance.GetLineData(id);
                if (ld != null) item = BuilderModel.Instance.GetLineData(id).line3;
            }

            if (!item)
                item = BuilderModel.Instance.GetSurfaceData(id).surface3;

            return item;
        }

        /// <summary>
        /// 获取所有相同资源ID的道具
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ItemStruct> GetSameAssetItemData(string id)
        {
            List<ItemStruct> list = new List<ItemStruct>();

            foreach (ItemStruct data in AssetsModel.Instance.itemDatas)
            {
                if (data.vo.assetId == id)
                {
                    list.Add(data);
                }
            }

            return list;
        }

        /// <summary>
        /// 取消选中
        /// </summary>
        /// <param name="value"></param>
        public static void EnabledEditorObjectSelection(bool value)
        {
            if (!value) Instance.editorObjectSelection.ClearSelection(false);
            Instance.editorObjectSelection.gameObject.SetActive(value);
        }

        private static bool _visibleEditorItem = true;

        /// <summary>
        /// 隐藏所有编辑道具
        /// </summary>
        public static bool VisibleEditorItem
        {
            set
            {
                _visibleEditorItem = value;

                foreach (ItemStruct data in AssetsModel.Instance.itemDatas)
                {
                    Item3D item = data.item3 as Item3D;
                    PointLightComponent pointLightComponent = item.GetComponentInChildren<PointLightComponent>();
                    if (pointLightComponent && pointLightComponent.gameObject.transform.Find("Sphere") != null)
                    {
                        pointLightComponent.gameObject.transform.Find("Sphere").gameObject.SetActive(value);
                    }

                    SprinkleComponent sprinkleComponent = item.GetComponentInChildren<SprinkleComponent>();
                    if (sprinkleComponent)
                    {
                        item.gameObject.SetActive(value);
                    }

                    BallLampComponent ballLampComponent = item.GetComponentInChildren<BallLampComponent>();
                    if (ballLampComponent && ballLampComponent.gameObject.transform.Find("Sphere") != null)
                    {
                        ballLampComponent.gameObject.transform.Find("Sphere").gameObject.SetActive(value);
                    }
                }
            }
            get { return _visibleEditorItem; }
        }

        /// <summary>
        /// 鼠标是否在操作区
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool InEditorArea(Vector2 v)
        {
            if ((Input.mousePosition.x > 350 && Input.mousePosition.x < 1520) &&
             (Input.mousePosition.y < 1000))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除保存文件里无用的连接资源
        /// </summary>
        public static void UpdateQuoteFileURL()
        {
            List<string> imgs = new List<string>();
            List<string> models = new List<string>();

            foreach (ItemStruct data in AssetsModel.Instance.itemDatas)
            {
                FrameVO framevo = data.vo.GetComponentVO<FrameVO>();
                if (framevo != null)
                {
                    imgs.Add(framevo.url);
                }

                CollageVO collagevo = data.vo.GetComponentVO<CollageVO>();
                if (collagevo != null)
                {
                    foreach (CollageStruct cs in collagevo.collages)
                    {
                        if (cs.url != "")
                        {
                            imgs.Add(cs.url);
                        }
                    }
                }

                ThickIrregularVO thickIrregularvo = data.vo.GetComponentVO<ThickIrregularVO>();
                if (thickIrregularvo != null)
                {
                    models.Add(thickIrregularvo.url);
                }
            }

            foreach (SurfaceStruct data in BuilderModel.Instance.surfaceDatas)
            {
                CollageVO collagevo = data.vo.GetComponentVO<CollageVO>();
                if (collagevo != null)
                {
                    foreach (CollageStruct cs in collagevo.collages)
                    {
                        if (cs.url != "")
                        {
                            imgs.Add(cs.url);
                        }
                    }
                }
            }

            foreach (LineStruct data in BuilderModel.Instance.lineDatas)
            {
                CollageVO collagevo = data.vo.GetComponentVO<CollageVO>();
                if (collagevo != null)
                {
                    foreach (CollageStruct cs in collagevo.collages)
                    {
                        if (cs.url != "")
                        {
                            imgs.Add(cs.url);
                        }
                    }
                }
            }

            bool has = false;
            List<string> list = new List<string>();
            string[] files = Directory.GetFiles(SceneManager.ProjectPictureURL, "*", SearchOption.AllDirectories);
            foreach (string f in files)
            {
                has = false;
                foreach (string imgurl in imgs)
                {
                    if (f.Contains(imgurl))
                    {
                        has = true;
                        break;
                    }
                }
                if (!has)
                {
                    File.Delete(f);
                }
            }

            files = Directory.GetFiles(SceneManager.ProjectModelURL, "*", SearchOption.AllDirectories);
            foreach (string f in files)
            {
                has = false;
                foreach (string modelurl in models)
                {
                    if (f.Contains(modelurl))
                    {
                        has = true;
                        break;
                    }
                }
                if (!has)
                {
                    File.Delete(f);
                }
            }
        }
    }
}
