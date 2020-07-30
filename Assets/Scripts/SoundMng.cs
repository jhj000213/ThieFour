using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
public class SoundMng : MonoBehaviour {

    public float _LineLenght;
    public UILabel _LineLenghtLabel;
    public UI2DSprite _LineLenghtBar;
    public UI2DSprite _LineLenghtBar_2;


    public GameObject _CorrectPopup;

    public List<PattunNode> _EditNodeList = new List<PattunNode>();

    public AudioSource _MySource;
    public AudioClip source_Invasion;
    public AudioClip source_House;
    public string _FileName;

    bool _SetLinePosMove;

    public GameObject _EditLine;
    public GameObject _Nodeparent;
    public GameObject _EditNode;

    public GameObject _TimeBall;
    public GameObject _PosBall;
    public GameObject _StartButton;
    public GameObject _PauseButton;
    public GameObject _StopButton;

    Dictionary<string, string> dicJson = new Dictionary<string, string>();
    
    void Start()
    {
        _MySource = GetComponent<AudioSource>();
        _MySource.clip = source_Invasion;
    }

    public void Init()
    {
        string path = @"c:\Editor\" + _FileName + ".txt";
        string textValue = System.IO.File.ReadAllText(path);
        dicJson = Json.Read(textValue);

        string path_total = @"c:\Editor\" + _FileName + "_lenght.txt";
        int _PattunTotalNode = int.Parse(System.IO.File.ReadAllText(path_total));

        for (int i = 0; i < _PattunTotalNode; i++)
        {
            if (int.Parse(dicJson["NodeSeq_" + i.ToString()]) == i)
            {
                GameObject obj = NGUITools.AddChild(_Nodeparent, _EditNode);
                float time = float.Parse(dicJson["Time_" + i.ToString()]) / _MySource.clip.length;
                obj.transform.localPosition = new Vector3(time * _LineLenght, 100, 0);

                obj.GetComponent<PattunNode>().Init(int.Parse(dicJson["NodeSeq_" + i.ToString()]),
                    float.Parse(dicJson["Time_" + i.ToString()]), int.Parse(dicJson["PattunNum_" + i.ToString()]), _MySource.clip.length, _LineLenght);
                _EditNodeList.Add(obj.GetComponent<PattunNode>());
            }
        }
    }

    public void StopInvasion()
    {
        _MySource.Stop();
        _MySource.clip = source_House;
        _MySource.Play();
    }

    public void SetLineLenght()
    {
        _LineLenght = float.Parse(_LineLenghtLabel.text);
        _LineLenghtBar.width = (int)_LineLenght;
        _LineLenghtBar_2.width = (int)_LineLenght;
        _LineLenghtBar.GetComponent<BoxCollider>().size = new Vector3(_LineLenght, 50, 0);
        _LineLenghtBar.GetComponent<BoxCollider>().center = new Vector3(_LineLenght / 2, 0, 0);
    }

    void Update()
    {
        //if (StaticDataMng._SoundOn)
            _MySource.volume = 0.75f;
        //else
            //_MySource.volume = 0.0f;

        _TimeBall.transform.localPosition = new Vector3(_LineLenght * (_MySource.time / _MySource.clip.length), 0, 0);
        _PosBall.transform.localPosition = new Vector3(1800.0f * -(_EditLine.transform.localPosition.x/_LineLenght), 0, 0);
        if(_SetLinePosMove)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x * (1920.0f / Screen.width), Input.mousePosition.y * (1080.0f / Screen.height));
            if(mousePos.x>=60.0f&&mousePos.x<=1860.0f)
            {
                float posx = (mousePos.x - 60) / 1800.0f;
                _EditLine.transform.localPosition = new Vector3(_LineLenght * -posx, -119, 0);
            }
            
        }
    }

    public void SetMusicTime()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x * (1920.0f / Screen.width), Input.mousePosition.y * (1080.0f / Screen.height));
        float posx = (mousePos.x - 60 - (_EditLine.transform.localPosition.x+900)) / _LineLenght;
        _MySource.time = posx * _MySource.clip.length;
    }

    public void SetMusicBarPos_On()
    {
        _SetLinePosMove = true;
    }
    public void SetMusicBarPos_Off()
    {
        _SetLinePosMove = false;
    }

    public void SetExport()
    {
        //시간순으로 정렬
        for(int i=0;i<_EditNodeList.Count;i++)
        {
            for(int j=i;j<_EditNodeList.Count;j++)
            {
                if(_EditNodeList[j]._CreateTime < _EditNodeList[i]._CreateTime)
                {
                    float node = _EditNodeList[i]._CreateTime;
                    _EditNodeList[i]._CreateTime = _EditNodeList[j]._CreateTime;
                    _EditNodeList[j]._CreateTime = node;


                    int num = _EditNodeList[i]._PattunNum;
                    _EditNodeList[i]._PattunNum = _EditNodeList[j]._PattunNum;
                    _EditNodeList[j]._PattunNum = num;
                }
            }
        }

        Dictionary<string, string> _editJson = new Dictionary<string, string>();
        for (int i = 0; i < _EditNodeList.Count;i++)
        {
            _editJson.Add("NodeSeq_" + i.ToString(), i.ToString());
            _editJson.Add("Time_" + i.ToString(), _EditNodeList[i]._CreateTime.ToString());
            _editJson.Add("PattunNum_" + i.ToString(), _EditNodeList[i]._PattunNum.ToString());
        }
        _EditNodeList.Clear();

        _MySource.time = 0.0f;
        _MySource.Stop();
        _CorrectPopup.SetActive(false);

        _Nodeparent.transform.DestroyChildren();

        string strJson = Json.Write(_editJson);
        string savePath = @"c:\Editor\" + _FileName + ".txt";
        System.IO.File.WriteAllText(savePath, strJson, Encoding.Default);


    }

    public void MusicStart()
    {
        _MySource.Play();
        _StartButton.SetActive(false);
        _PauseButton.SetActive(true);
    }

    public void MusicPause()
    {
        _MySource.Pause();
        _StartButton.SetActive(true);
        _PauseButton.SetActive(false);
    }
    public void MusicStop()
    {
        _MySource.time = 0.0f;
        _MySource.Stop();
        _StartButton.SetActive(true);
        _PauseButton.SetActive(false);
    }


}
