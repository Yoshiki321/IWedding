using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardManager : EventDispatcher
{
    private bool ctrl;
    private bool shift;
    private bool alt;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) ctrl = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) ctrl = false;
        if (Input.GetKeyDown(KeyCode.LeftAlt)) alt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) alt = false;
        if (Input.GetKeyDown(KeyCode.LeftShift)) shift = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) shift = false;

        if (ctrl && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("UNDO");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.UNDO));
        }

        if (ctrl && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("COPY");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.COPY));
        }

        if (ctrl && Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("PASTE");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.PASTE));
        }

        if (ctrl && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("CUT");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.CUT));
        }

        if (ctrl && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("REDO");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.REDO));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("SAVE");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.SAVE_PROJECT));
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("LOAD");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.LOAD_PROJECT));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("FOCUSON_SELECTION");
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.FOCUSON_SELECTION));
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.DELETE));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.ESC));
        }

        if (ctrl && Input.GetKeyDown(KeyCode.G))
        {
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.COMBINATION));
        }

        if (ctrl && shift && Input.GetKeyDown(KeyCode.G))
        {
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.CANCEL_COMBINATION));
        }

        if (ctrl && shift && Input.GetKeyDown(KeyCode.L))
        {
            dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.OPEN_DRAWLINEPANEL));
        }

        //if (Input.GetKeyDown(KeyCode.F6))
        //{
        //    dispatchEvent(new KeyboardManagerEvent(KeyboardManagerEvent.LOAD_COMBINATION));
        //}
    }
}
