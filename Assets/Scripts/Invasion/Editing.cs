using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class Editing : MonoBehaviour {

    public GameObject _UIRoot;
    public GameObject _Effect;
    public UILabel _Timer;

    public LobbyMng _LobbyMng;

    public bool _Starting;

    int _NowNodeSequence = 0;

    public int _One = 1;
    public int _Two = 2;
    public int _Three = 3;
    public int _Four = 4;

    public float _NowTime;
    public string _FileName;
    Dictionary<string, string> dicJson = new Dictionary<string, string>();
	void Update()
    {
        if(_Starting)
        {
            _NowTime += Time.smoothDeltaTime;
            _Timer.text = _NowTime.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddPattun(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddPattun(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddPattun(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddPattun(4);
        }
    }
	
    public void AddPattun(int pattunnum)
    {
        dicJson.Add("NodeSeq_" + _NowNodeSequence.ToString(), _NowNodeSequence.ToString());
        dicJson.Add("Time_"+_NowNodeSequence.ToString(), _NowTime.ToString());
        dicJson.Add("PattunNum_" + _NowNodeSequence.ToString(), pattunnum.ToString());

        GameObject obj = NGUITools.AddChild(_UIRoot, _Effect);
        obj.transform.localPosition = new Vector3(-675, -338, 0);
        _NowNodeSequence++;
    }

    public void FinishEditing()
    {
        string strJson = Json.Write(dicJson);
        string savePath = @"c:\Editor\" + _FileName + ".txt";
        System.IO.File.WriteAllText(savePath, strJson, Encoding.Default);

        string savePath_lenght = @"c:\Editor\" + _FileName + "_lenght.txt";
        System.IO.File.WriteAllText(savePath_lenght, _NowNodeSequence.ToString(), Encoding.Default);

    }
}
