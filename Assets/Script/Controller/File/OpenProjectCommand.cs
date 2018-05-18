using BuildManager;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using UnityEngine;

public class OpenProjectCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = "请选择文件夹";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            if (string.IsNullOrEmpty(dialog.SelectedPath))
            {
                MessageBox.Show("文件夹路径不能为空");
                return;
            }

            fileEvent.url = dialog.SelectedPath;
        }

        if (!File.Exists(fileEvent.url + "\\project.zwkjProject"))
        {
            DispatchEvent(new FileEvent(FileEvent.OPEN_PROJECT_FAIL));
        }
        else
        {
            WWW www = new WWW("file:///" + fileEvent.url + "\\" + "project.zwkjProject");
            if (string.IsNullOrEmpty(www.error))
            {
                string str = System.Text.Encoding.Default.GetString(www.bytes);
                www.Dispose();

                SceneManager.ProjectName = str;
                SceneManager.ProjectURL = fileEvent.url;
                SceneManager.ProjectModelURL = fileEvent.url + "/" + "Resources" + "/" + "Model";
                SceneManager.ProjectPictureURL = fileEvent.url + "/" + "Resources" + "/" + "Picture";
                SceneManager.ProjectCombinationURL = fileEvent.url + "/" + "Resources" + "/" + "Combination";

                CodeManager.LoadBuildCode(fileEvent.url + "\\" + str + "_Build");
                CodeManager.LoadAssetsCode(fileEvent.url + "\\" + str + "_Assets");
                DispatchEvent(new FileEvent(FileEvent.OPEN_PROJECT_SUCCESS));
            }
        }
    }
}
