/****************************************************

	文件：
	作者：WWS
	日期：2022/10/31 15:25:09
	功能：追要对Unity的Componetn组件的拓展方法(this大法)


*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public static class ExtendComponent
{


    #region Button


    public static void Click(this Transform t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public static void Click(this RectTransform t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public static void Click(this GameObject t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }
    #endregion



    #region RectTransform

    public static RectTransform Rect(this Transform trans)
    {
        return trans.GetComponent<RectTransform>();
    }

    public static RectTransform Rect(this GameObject go)
    {
        return go.GetComponent<RectTransform>();
    }
    public static void Reset(this RectTransform rect)
    {
        rect.localPosition = Vector3.zero;
        rect.anchoredPosition = Vector3.zero;
        rect.sizeDelta = Vector3.zero;
    }
    #endregion



    #region Toggle


    /// <summary>
    /// bug 暂时不会，因为_bool传不出
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="_bool"></param>
    /// <param name="clickAction"></param>
    private static void AddListener(this Toggle toggle, bool _bool, Action clickAction)
    {
        Text text = toggle.GetComponentInChildren<Text>();  //Start
        text.text = _bool.ToString();
        toggle.isOn = _bool;
        //
        toggle.onValueChanged.AddListener((bool _state) =>   //Update
        {
            _bool = _state;
            text.text = _state.ToString();
            clickAction();
        });

    }
    #endregion




    #region Button


    /// <summary>
    /// 按钮监听
    /// </summary>
    /// <param name="trans">父节点</param>
    /// <param name="path">按钮路径</param>
    /// <param name="action"></param>
    /// <param name="useDefaultAudio">音效</param>
    public static void ButtonAction(this Transform trans, string path, Action action, bool useDefaultAudio = true)
    {
        var target = trans.Find(path);
        if (target == null)
        {
            Debug.LogErrorFormat("(Transform){0}不存在", path);
        }
        else
        {
            var button = target.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogErrorFormat("(Transform){0}不存在Button组件", target.name);
            }
            else
            {
                button.onClick.AddListener(() => action());
                if (useDefaultAudio)
                {
                    button.onClick.AddListener(AddButtonAudio);
                }
            }
        }
    }

    private static void ButtonAction(this Transform trans, Action action)
    {
        var button = trans.GetComponent<Button>();
        button.onClick.AddListener(() => action());
        button.onClick.AddListener(AddButtonAudio);
    }

    private static void AddButtonAudio()
    {
        // AudioMgr.Single.PlayOnce(UIAduio.UI_ClickButton.ToString());
    }


    public static void Listener(this Button button, Action action)
    {
        button.onClick.AddListener(() => action());

    }
    #endregion




    #region Transform
    public static T AddComponent<T>(this Transform t, string path) where T : Component
    {
        return t.Find(path).gameObject.AddComponent<T>();
    }

    public static void Reset(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public static Transform FindOrNew(this Transform t, string path) 
    {
      Transform tar=  t.Find(path);
        if (tar == null)
        {
            GameObject go = new GameObject();
            go.name = path;
            go.transform.Reset();


            return go.transform;
        }

        return null;
    }
    #endregion



        #region Component     


    public static T AddComponent<T>(this Transform t) where T : Component
    {
        return t.gameObject.AddComponent<T>();
    }

    public static T AddComponent<T>(this RectTransform t) where T : Component
    {
        return t.gameObject.AddComponent<T>();
    }

    /** Assets/Plugins/UnityGameFramework/Scripts/Runtime/Utility/UnityExtension.cs有重复的
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        if (go.GetComponent<T>() != null)
        {
            return go.GetComponent<T>();
        }
        else
        {
            return go.AddComponent<T>();
        }
    }

    public static T GetOrAddComponent<T>(this Transform t) where T : UnityEngine.Component
    {
        return GetOrAddComponent<T>(t.gameObject);
    }
    **/
    #endregion




    #region GameObject


    public static void SetParent(this GameObject go, Transform parent)
{
    go.transform.SetParent(parent);
}



/// <summary>如果存在就不要创建（返回时常用）</summary>
public static void DestoryOnExistOne<T>(this GameObject go) where T : MonoBehaviour
{
    if (UnityEngine.Object.FindObjectsOfType<T>().Length > 1)
    {
        GameObject.Destroy(go);
        return;
    }
}

/// <summary>
/// bug 回溢栈
/// 换个写法，少写括号
/// </summary>
private static void DontDestroyOnLoad(this GameObject go)
{
    DontDestroyOnLoad(go);
}
#endregion



/**#region Text Image等Graphic


/// <summary>
/// _image.DOColor报错
/// </summary>
/// <param name="graphic"></param>
/// <param name="endValue"></param>
/// <param name="duration"></param>
public static void DOColor<T>(this T graphic, Color endValue, float duration) where T:Graphic
{
    //Graphic graphic = (Image)image;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        endValue,
        duration) ;
}

public static void DOFade<T>(this T graphic, Int32 endAlpha, float duration) where T : Graphic
{
    Color tarColor = graphic.color;
    tarColor.a = endAlpha;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        tarColor,
        duration);
}

public static void DOColor<T>(this T graphic, Color endValue, float duration,Action action) where T : Graphic
{
    //Graphic graphic = (Image)image;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        endValue,
        duration).OnComplete(() => action()); ;
}

public static void DOFade<T>(this T graphic, Int32 endAlpha, float duration, Action action) where T : Graphic
{
    Color tarColor = graphic.color;
    tarColor.a = endAlpha;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        tarColor,
        duration).OnComplete(()=>action());
}

public static void DOFade_Material<T>(this T graphic, Int32 endAlpha, float duration, Action action) where T :  Material
{
    Color tarColor = graphic.color;
    tarColor.a = endAlpha;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        tarColor,
        duration).OnComplete(() => action());
}

public static void DOColor<T>(this T graphic, Color endValue, float duration, int loops, LoopType loopType) where T : Graphic
{
    //Graphic graphic = (Image)image;
    DOTween.To(() => graphic.color,
        newColor => { graphic.color = newColor; },
        endValue,
        duration).SetLoops(loops,loopType);
}

#endregion **/


}
