using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using UnityEngine.UI;
using BuildManager;
using Build3D;

public class ItemTool : BasePanel
{
    private Button leftBtn;
    private Button rightBtn;
    private Button resetBtn;
    private Button combinationBtn;
    private Button resolveBtn;
    private Button removeBtn;

    private void Awake()
    {
        leftBtn = GetUI("left").GetComponent<Button>();
        rightBtn = GetUI("right").GetComponent<Button>();
        resetBtn = GetUI("reset").GetComponent<Button>();
        combinationBtn = GetUI("combination").GetComponent<Button>();
        resolveBtn = GetUI("resolve").GetComponent<Button>();
        removeBtn = GetUI("remove").GetComponent<Button>();

        leftBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_LEFT)); SceneManager.Instance.mouse3Manager.canClick = false; });
        rightBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_RIGHT)); SceneManager.Instance.mouse3Manager.canClick = false; });
        resetBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_RESET)); SceneManager.Instance.mouse3Manager.canClick = false; });
        removeBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_REMOVE)); SceneManager.Instance.mouse3Manager.canClick = false; });
        combinationBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_COMBINATION)); UpdateCombination(); SceneManager.Instance.mouse3Manager.canClick = false; });
        resolveBtn.onClick.AddListener(() => { transform.parent.GetComponent<BasePanel>().dispatchEvent(new ItemToolEvent(ItemToolEvent.ITEMTOOL_RESOLVE)); UpdateCombination(); SceneManager.Instance.mouse3Manager.canClick = false; });

        AddEventOver(leftBtn.gameObject);
        AddEventOver(rightBtn.gameObject);
        AddEventOver(resetBtn.gameObject);
        AddEventOver(removeBtn.gameObject);
        AddEventOver(combinationBtn.gameObject);
        AddEventOver(resolveBtn.gameObject);

        AddEventExit(leftBtn.gameObject);
        AddEventExit(rightBtn.gameObject);
        AddEventExit(resetBtn.gameObject);
        AddEventExit(removeBtn.gameObject);
        AddEventExit(combinationBtn.gameObject);
        AddEventExit(resolveBtn.gameObject);
    }

    protected override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);

        SceneManager.Instance.control3Manager.loop = false;
    }

    protected override void OnExit(GameObject obj)
    {
        SceneManager.Instance.control3Manager.loop = true;
    }

    private List<Item3D> _items;

    public List<Item3D> items
    {
        set
        {
            _items = value;

            UpdateCombination();
            UpdateItem();
        }
        get { return _items; }
    }

    public void UpdateCombination()
    {
        if (_items != null && _items.Count > 1)
        {
            string id = "";
            bool same = true;
            foreach (Item3D item in _items)
            {
                if (id == "")
                {
                    id = (item.VO as ItemVO).groupId;
                }
                if (id != (item.VO as ItemVO).groupId)
                {
                    same = false;
                }
            }
            if (id == "")
            {
                combinationBtn.gameObject.SetActive(true);
                resolveBtn.gameObject.SetActive(false);
            }
            else
            {
                if (same)
                {
                    combinationBtn.gameObject.SetActive(false);
                    resolveBtn.gameObject.SetActive(true);
                }
                else
                {
                    combinationBtn.gameObject.SetActive(true);
                    resolveBtn.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            combinationBtn.gameObject.SetActive(false);
            resolveBtn.gameObject.SetActive(false);
        }
    }

    public void UpdateItem()
    {
        if (_items != null && _items.Count > 0)
        {
            Vector3 cpoint = SceneManager.Instance.editorGizmoSystem.TranslationGizmo.transform.position;
            Vector3 sp = SceneManager.Instance.Camera3D.WorldToScreenPoint(cpoint);

            leftBtn.transform.localPosition = new Vector3(sp.x - 90, sp.y - 220, sp.z);
            rightBtn.transform.localPosition = new Vector3(sp.x, sp.y - 220, sp.z);
            resetBtn.transform.localPosition = new Vector3(sp.x + 90, sp.y - 220, sp.z);
            removeBtn.transform.localPosition = new Vector3(sp.x + 180, sp.y - 220, sp.z);

            combinationBtn.transform.localPosition = new Vector3(sp.x - 45, sp.y - 310, sp.z);
            resolveBtn.transform.localPosition = new Vector3(sp.x + 45, sp.y - 310, sp.z);
        }

        if (SceneManager.Instance.editorGizmoSystem.TranslationGizmo.BecameVisible)
        {
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(true);
            resetBtn.gameObject.SetActive(true);
            removeBtn.gameObject.SetActive(true);
            combinationBtn.gameObject.SetActive(true);
            resolveBtn.gameObject.SetActive(true);
        }
        else
        {
            leftBtn.gameObject.SetActive(false);
            rightBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(false);
            removeBtn.gameObject.SetActive(false);
            combinationBtn.gameObject.SetActive(false);
            resolveBtn.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        UpdateItem();
    }
}
