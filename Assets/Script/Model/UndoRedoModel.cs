using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UndoRedoModel : Actor<UndoRedoModel>
{
    private List<SceneEvent> _undoStack = new List<SceneEvent>();
    private List<SceneEvent> _redoStack = new List<SceneEvent>();

    public int maxUndoActions = 20;

    public bool canUndo
    {
        get { return _undoStack.Count > 0; }
    }

    public bool canRedo
    {
        get { return this._redoStack.Count > 0; }
    }

    public SceneEvent GetLastActon()
    {
        if (canUndo)
        {
            return _undoStack[_undoStack.Count - 1];
        }
        return null;
    }

    public void RegisterAction(SceneEvent e)
    {
        this._redoStack = new List<SceneEvent>();

        this._undoStack.Add(e);
        while (this._undoStack.Count > this.maxUndoActions)
        {
            this._undoStack.RemoveAt(0);
        }

        Dispatch(new UndoRedoEvent(UndoRedoEvent.UNDO_LIST_CHANGE));
    }

    public void Clear()
    {
        _undoStack = _redoStack = new List<SceneEvent>();
    }

    public void Undo()
    {
        if (this._undoStack.Count == 0)
        {
            return;
        }

        SceneEvent e = _undoStack[_undoStack.Count - 1];
        _undoStack.RemoveAt(_undoStack.Count - 1);

        SceneEvent undoEvent = e.Clone() as SceneEvent;
        List<AssetVO> undoAssets = undoEvent.oldAssets;
        undoEvent.oldAssets = undoEvent.newAssets;
        undoEvent.newAssets = undoAssets;
        undoEvent.isRedoAction = false;
        undoEvent.isUndoAction = true;
        Dispatch(undoEvent);
        this._redoStack.Add(e);
        Dispatch(new UndoRedoEvent(UndoRedoEvent.UNDO_LIST_CHANGE));
    }

    public void Redo()
    {
        if (this._redoStack.Count == 0)
        {
            return;
        }
        SceneEvent e = _redoStack[_redoStack.Count - 1];
        _redoStack.RemoveAt(_redoStack.Count - 1);
        SceneEvent redoEvent = e.Clone() as SceneEvent;
        redoEvent.isRedoAction = true;
        redoEvent.isUndoAction = false;
        Dispatch(redoEvent);
        this._undoStack.Add(e);
        Dispatch(new UndoRedoEvent(UndoRedoEvent.UNDO_LIST_CHANGE));
    }
}
