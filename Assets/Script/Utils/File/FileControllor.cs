using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common;

public class FileType
{
    public static string TEXTURE = "png(*.png)\0*.png\0jpg(*.jpg)\0*.jpg\0";
    public static string CODE = "xml(*.xml)\0*.xml";
    public static string THICKIRREGULAR = "zwkjtiobj(*.zwkjtiobj)\0*.zwkjtiobj";
    public static string SOUND = "mp3(*.mp3)";
    public static string OBJ = "obj(*.obj)"; 
    public static string COMBINATION = "zwkjCombination(*.zwkjCombination)\0*.zwkjCombination";
}

public class FileControllor
{
    public OpenFileDlg OpenProject(string type)
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = type;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath;
        pth.title = "打开项目";
        pth.defExt = "txt";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            return pth;
        }
        return null;
    }

    public SaveFileDlg SaveProject(string type, string folderName = "")
    {
        SaveFileDlg pth = new SaveFileDlg();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = type;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath;
        pth.title = "保存项目";
        pth.defExt = "txt";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (SaveFileDialog.GetSaveFileName(pth))
        {
            return pth;
        }
        return null;
    }
}