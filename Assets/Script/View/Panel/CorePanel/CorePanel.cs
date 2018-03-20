﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePanel : BasePanel
{
    public LineTool lineTool;
    public ItemTool itemTool;
    public RankToolPanel rankTool;
    public SceneToolbarPanel sceneToolbarPanel;

    void Start()
    {
        lineTool = GetUI("LineTool").GetComponent<LineTool>();
        itemTool = GetUI("ItemTool").GetComponent<ItemTool>();
        rankTool = GetUI("RankTool").GetComponent<RankToolPanel>();
        sceneToolbarPanel = GetUI("SceneTopTool").GetComponent<SceneToolbarPanel>();

        SetActiveLineTool(false);
        SetActiveItemTool(false);
        SetActiveRankTool(false);
		SetActiveSceneToolbarPanel(true);
    }

    public void SetActiveItemTool(bool value)
    {
        if (itemTool) itemTool.gameObject.SetActive(value);
    }

    public void SetActiveLineTool(bool value)
    {
        if (lineTool) lineTool.gameObject.SetActive(value);
    }

    public void SetActiveRankTool(bool value)
    {
        if (rankTool) rankTool.gameObject.SetActive(value);
    }

    public void SetActiveSceneToolPanel(bool value)
    {
        //sceneToolPanel.nested.SetActive(value);
        //sceneToolPanel.space.SetActive(value);
    }

    public void SetActiveSceneToolbarPanel(bool value)
    {
        if (sceneToolbarPanel) sceneToolbarPanel.gameObject.SetActive(value);
    }

    void Update()
    {

    }
}
