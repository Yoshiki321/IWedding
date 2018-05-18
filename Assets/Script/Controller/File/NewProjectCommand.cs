using BuildManager;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using UnityEngine;

public class NewProjectCommand : Command
{
    public override void Execute(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;

        string url = fileEvent.url + "/" + fileEvent.name;

        SceneManager.ProjectName = fileEvent.name;

        int i = 0;
        while (Directory.Exists(url))
        {
            i++;

            int idxStart = url.LastIndexOf(" ");
            string value = url.Substring(idxStart, url.Length - idxStart);
            value = Regex.Replace(value, " ", "");

            if (NumberUtils.IsInt(value))
            {
                url = url.Remove(idxStart);
                url += " " + (int.Parse(value) + 1);
            }
            else
            {
                url += " " + i.ToString();
            }

            string[] namets = url.Split('/');
            SceneManager.ProjectName = namets[namets.Length - 1];
        }
        
        SceneManager.ProjectURL = url;
        SceneManager.ProjectResourcesURL = url + "/" + "Resources";
        SceneManager.ProjectModelURL = url + "/" + "Resources" + "/" + "Model";
        SceneManager.ProjectPictureURL = url + "/" + "Resources" + "/" + "Picture";
        SceneManager.ProjectCombinationURL = url + "/" + "Resources" + "/" + "Combination";

        Directory.CreateDirectory(url);
        Directory.CreateDirectory(url + "/" + "Resources");
        Directory.CreateDirectory(SceneManager.ProjectModelURL);
        Directory.CreateDirectory(SceneManager.ProjectPictureURL);
        Directory.CreateDirectory(SceneManager.ProjectCombinationURL);

        string name = "project.zwkjProject";
        string urlName = url + "\\" + name;
        byte[] bytes = System.Text.Encoding.Default.GetBytes(SceneManager.ProjectName);
        File.WriteAllBytes(urlName, bytes);

        DispatchEvent(new FileEvent(FileEvent.NEW_PROJECT_COMPLETE));
    }
}
