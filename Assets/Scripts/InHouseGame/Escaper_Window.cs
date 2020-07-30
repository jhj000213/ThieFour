using UnityEngine;
using System.Collections;

public class Escaper_Window : MonoBehaviour {

    public GameObject _BreakEffect;
    public GameObject _WindowLight;

    public GameObject[] _PlayerAnimation = new GameObject[4];

    public bool _Broken;

    public void WindowBreak()
    {
        GameObject obj = NGUITools.AddChild(gameObject, _BreakEffect);
        GameObject obj1 = NGUITools.AddChild(gameObject, _WindowLight);
        GetComponent<UISprite>().spriteName = "brokenwindow";
        _Broken = true;
    }

    public void MakeAnimation_Char(int num)
    {
        GameObject obj = NGUITools.AddChild(gameObject, _PlayerAnimation[num]);
        obj.transform.localPosition = new Vector3(0, 58, 0);
    }
}