public class Context
{
    public Context()
    {
        UIManager.CreateUI("UI/MainPanel/MainPanel", UI.MainPanel, typeof(MainPanelMediators));
        UIManager.CreateUI("UI/ChooseSurface/ChooseSurfacePanel", UI.ChooseSurfacePanel, typeof(ChooseSurfaceMediators));
        UIManager.CreateUI("UI/CameraTool/CameraToolPanel", UI.CameraToolPanel, typeof(CameraToolMediators));
        UIManager.CreateUI("UI/ItemTool/ItemToolPanel", UI.ItemToolPanel, typeof(ItemToolMediators));
        UIManager.CreateUI("UI/GizmoPanel/GizmoPanel", UI.GizmoPanel, typeof(GizmoPanelMediators));
        UIManager.CreateUI("UI/ItemSelectPanel/ItemSelectPanel", UI.ItemSelectPanel, typeof(ItemSelectPanelMediators));
		UIManager.CreateUI("UI/SwitchToolPanel/SwitchToolPanel", UI.SwitchToolPanel, typeof(SwitchToolPanelMediators));
        UIManager.CreateUI("UI/TopToolPanel/TopToolPanel", UI.TopToolPanel, typeof(TopToolPanelMediators));
        UIManager.CreateUI("UI/Component/ComponentPanel", UI.ComponentPanel, typeof(ComponentPanelMediators));
		UIManager.CreateUI("UI/CorePanel/CorePanel", UI.CorePanel, typeof(CoreEditorMediator));
		UIManager.CreateUI("UI/ProjectPanel/ProjectPanel", UI.ProjectPanel, typeof(ProjectPanelMediators));
		UIManager.CreateUI("UI/DrawLinePanel/DrawLinePanel", UI.DrawLinePanel, typeof(DrawLineMediators));
		UIManager.CreateUI("UI/LoadModelPanel/LoadModelPanel", UI.LoadModelPanel, typeof(LoadModelPanelMediators));

        //UIManager.CreateComponent("UI/Component/TransformComponent", UI.TransformComponentUI);
        //UIManager.CreateComponent("UI/Component/SpotlightComponent", UI.SpotlightComponentUI);
        UIManager.CreateComponent("UI/Component/MultipleSpotlighComponent", UI.MultipleSpotlighComponentUI);
        //UIManager.CreateComponent("UI/Component/CollageComponent", UI.CollageComponentUI);
        //UIManager.CreateComponent("UI/Component/BallLampComponent", UI.BallLampComponentUI); 
        //UIManager.CreateComponent("UI/Component/RadiationLampComponent", UI.RadiationLampComponentUI);
        //UIManager.CreateComponent("UI/Component/FrameComponent", UI.FrameComponentUI);
        //UIManager.CreateComponent("UI/Component/TubeLightComponent", UI.TubeLightComponentUI); 
        UIManager.CreateComponent("UI/Component/BubbleComponent", UI.BubbleComponentUI); 
        UIManager.CreateComponent("UI/Component/SmokeComponent", UI.SmokeComponentUI); 
        //UIManager.CreateComponent("UI/Component/EditorCameraComponent", UI.EditorCameraComponentUI); 
        //UIManager.CreateComponent("UI/Component/PointLightComponent", UI.PointLightComponentUI);

        UIManager.CreateComponent(UI.EditorCameraComponentUI);
        UIManager.CreateComponent(UI.TransformComponentUI);
        UIManager.CreateComponent(UI.BallLampComponentUI);
        UIManager.CreateComponent(UI.SpotlightComponentUI);
        UIManager.CreateComponent(UI.RadiationLampComponentUI);
        UIManager.CreateComponent(UI.FrameComponentUI);
        UIManager.CreateComponent(UI.TubeLightComponentUI);
        UIManager.CreateComponent(UI.PointLightComponentUI);
        UIManager.CreateComponent(UI.CollageComponentUI);
        UIManager.CreateComponent(UI.RelationComponentUI); 
        UIManager.CreateComponent(UI.ThickIrregularComponentUI);
        UIManager.CreateComponent(UI.SprinkleComponentUI);

        UIManager.CreatePanel("UI/Currency/SelectColor/SelectColorPanel", Panel.SelectColorPanel);
        UIManager.CreatePanel("UI/Currency/SelectTexture/SelectTexturePanel", Panel.SelectTexturePanel);

        CommandMap.MapEvent(FileEvent.SAVE, typeof(SaveCodeCommand));
        CommandMap.MapEvent(FileEvent.LOAD, typeof(LoadCodeCommand)); 
        CommandMap.MapEvent(FileEvent.OPEN_PROJECT, typeof(OpenProjectCommand));
        CommandMap.MapEvent(FileEvent.NEW_PROJECT, typeof(NewProjectCommand));
        CommandMap.MapEvent(FileEvent.SAVE_PROJECT, typeof(LoadCodeCommand));
        CommandMap.MapEvent(FileEvent.SAVE_THICKIRREGULAR, typeof(SaveThickIrregularCommand));
        CommandMap.MapEvent(FileEvent.LOAD_THICKIRREGULAR, typeof(LoadThickIrregularCommand));
        CommandMap.MapEvent(FileEvent.LOAD_OBJ, typeof(LoadOBJCommand));
        CommandMap.MapEvent(FileEvent.LOAD_TEXTURE, typeof(LoadTextureCommand));

        CommandMap.MapEvent(FileEvent.SAVE_COMBINATION, typeof(SaveCombinationCommand));
        CommandMap.MapEvent(FileEvent.LOAD_COMBINATION, typeof(LoadCombinationCommand));

        CommandMap.MapEvent(CameraCommandEvent.CHANGE, typeof(ChangeCameraCommand));

        CommandMap.MapEvent(UndoRedoEvent.UNDO, typeof(UndoCommand));
        CommandMap.MapEvent(UndoRedoEvent.REDO, typeof(RedoCommand));

        CommandMap.MapEvent(SceneEvent.CHANGE_NESTED, typeof(ChangeNestedCommand)); 
        CommandMap.MapEvent(SceneEvent.CHANGE_ITEM, typeof(ChangeItemCommand)); 
        CommandMap.MapEvent(SceneEvent.CHANGE_GROUP_ITEM, typeof(ChangeGroupItemCommand));

        CommandMap.MapEvent(SceneEvent.TRANSFORM, typeof(TransformCommand));
        CommandMap.MapEvent(SceneEvent.CHANGE_COMPONENT, typeof(ChangeComponentCommand));
        CommandMap.MapEvent(SceneEvent.DELETE, typeof(DeleteCommand));

        CommandMap.MapEvent(SceneEvent.ADD_ITEM, typeof(AddItemCommand));
        CommandMap.MapEvent(SceneEvent.ADD_NESTED, typeof(AddNestedCommand));
        CommandMap.MapEvent(SceneEvent.ADD_SURFACE, typeof(AddSurfaceCommand));

        CommandMap.MapEvent(SceneEvent.CHANGE_BUILD, typeof(ChangeBuildCommand)); 
        CommandMap.MapEvent(SceneEvent.CHANGE_COLLAGE, typeof(ChangeCollageCommand));
        CommandMap.MapEvent(SceneEvent.CHANGE_SURFACE, typeof(ChangeSurfaceCommand));
    }
}
