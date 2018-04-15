using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : BasePanel
{
    private GameObject progress;
    private Text text;

    void Start()
    {
        progress = GetUI("Progress");
        text = GetUI("Text").GetComponent<Text>();
    }

    void Update()
    {
        float pc = AssetsModel.Instance.progressItemCurrent;
        float pt = AssetsModel.Instance.progressItemTotal;
        float c = pc / pt;
        float xx = c * 1400;
        progress.GetComponent<RectTransform>().sizeDelta = new Vector2(xx, progress.GetComponent<RectTransform>().sizeDelta.y);
        progress.GetComponent<RectTransform>().anchoredPosition = new Vector2((965 - 265) * c + 265, progress.GetComponent<RectTransform>().anchoredPosition.y);
        text.text = AssetsModel.Instance.progressItemCurrent + " / " + AssetsModel.Instance.progressItemTotal;

        if (AssetsModel.Instance.progressItemCurrent == AssetsModel.Instance.progressItemTotal)
        {
            dispatchEvent(new ProgressPanelEvent(ProgressPanelEvent.LOAD_COMPLETE));
            CloseSelf();
        }
    }
}
