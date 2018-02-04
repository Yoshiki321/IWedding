using UnityEngine;
using System.Collections;
using System;

public class SceneComponent : MonoBehaviour
{
    protected IEnumerator LoadTexture(string url, Action<Texture2D> cb)
    {
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            cb.Invoke(www.texture);
            www.Dispose();
        }
    }

    public virtual void Init(AssetSprite _item)
    {

    }

    public virtual AssetVO VO
    {
        set { }
        get { return null; }
    }
}
