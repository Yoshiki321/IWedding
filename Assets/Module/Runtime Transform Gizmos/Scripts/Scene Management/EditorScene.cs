using UnityEngine;
using System;
using System.Collections.Generic;
using BuildManager;
using System.Collections;
using Build3D;

namespace RTEditor
{
    public class EditorScene : SingletonBase<EditorScene>
    {
        #region Private Variables
        private GameObjectSphereTree _gameObjectSphereTree = new GameObjectSphereTree(2);
        #endregion

        #region Public Methods
        public void MaskObjectCollectionForInteraction(IEnumerable<GameObject> gameObjects)
        {
            _gameObjectSphereTree.MaskObjectCollection(gameObjects);
        }

        public void MaskObjectForInteraction(GameObject gameObj)
        {
            _gameObjectSphereTree.MaskObject(gameObj);
        }

        public void UnmaskObjectCollectionForInteraction(IEnumerable<GameObject> gameObjects)
        {
            _gameObjectSphereTree.UnmaskObjectCollection(gameObjects);
        }

        public void UnmaskObjectForInteraction(GameObject gameObj)
        {
            _gameObjectSphereTree.UnmaskObjct(gameObj);
        }

        public void Update()
        {
            if (!RuntimeEditorApplication.Instance.UseUnityColliders || Application.isEditor)
                _gameObjectSphereTree.Update();
        }

        public List<GameObjectRayHit> RaycastAllBox(Ray ray)
        {
            if (!RuntimeEditorApplication.Instance.UseUnityColliders)
                return _gameObjectSphereTree.RaycastAllBox(ray);
            else
            {
                RaycastHit[] hits = Physics.RaycastAll(ray);
                var gameObjectHits = new List<GameObjectRayHit>();

                GameObject item3Object = null;
                Item3D item3;
                float d = 999999;
                GameObjectRayHit gameObjectRayHit = null;

                foreach (var hit in hits)
                {
                    // Retrieve the object which was hit
                    GameObject gameObject = hit.collider.gameObject;
                    if (gameObject == null) continue;
                    if (!gameObject.activeSelf) continue;

                    // If the ray intersects the object's box, add the hit to the list
                    //GameObjectRayHit gameObjectRayHit = null;

                    if (Vector3.Distance(SceneManager.Instance.EditorCamera.transform.position, hit.point) < d)
                    {
                        d = Vector3.Distance(SceneManager.Instance.EditorCamera.transform.position, hit.point);

                        item3 = gameObject.GetComponentInParent<Item3D>();

                        if (item3 != null)
                        {
                            item3Object = item3.gameObject;
                        }
                    }
                }

                if (item3Object != null)
                {
                    gameObjectRayHit = new GameObjectRayHit(ray, item3Object, null, null, null, null);
                    gameObjectHits.Add(gameObjectRayHit);
                }

                //if (item3Object != null && item3Object.RaycastBox(ray, out gameObjectRayHit)) gameObjectHits.Add(gameObjectRayHit);

                return gameObjectHits;
            }
        }

        public List<GameObjectRayHit> RaycastAllSprite(Ray ray)
        {
            return _gameObjectSphereTree.RaycastAllSprite(ray);
        }

        public List<GameObjectRayHit> RaycastAllMesh(Ray ray)
        {
            if (!RuntimeEditorApplication.Instance.UseUnityColliders)
                return _gameObjectSphereTree.RaycastAllMesh(ray);
            else
            {
                RaycastHit[] hits = Physics.RaycastAll(ray);
                var gameObjectHits = new List<GameObjectRayHit>();
                foreach (var hit in hits)
                {
                    // Retrieve the object which was hit
                    GameObject gameObject = hit.collider.gameObject;
                    if (gameObject == null) continue;
                    if (!gameObject.activeSelf) continue;

                    GameObjectRayHit gameObjectRayHit = null;
                    if (gameObject.RaycastMesh(ray, out gameObjectRayHit)) gameObjectHits.Add(gameObjectRayHit);
                }

                return gameObjectHits;
            }
        }

        // Note: When Unity colliders are used, this will actually perform an 'OverlapSphere' check
        public List<GameObject> OverlapBox(Box box, ObjectOverlapPrecision overlapPrecision = ObjectOverlapPrecision.ObjectBox)
        {
            if (!RuntimeEditorApplication.Instance.UseUnityColliders)
                return _gameObjectSphereTree.OverlapBox(box, overlapPrecision);
            else
            {
                var overlappedObjects = new List<GameObject>();
                Collider[] overlappedColliders = Physics.OverlapSphere(box.Center, box.Extents.magnitude);
                foreach (var collider in overlappedColliders) overlappedObjects.Add(collider.gameObject);

                return overlappedObjects;
            }
        }
        #endregion
    }
}