using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InvasionMng : MonoBehaviour {

    public LobbyMng _LobbyMng;
    public SoundMng _SoundMng;

    public GameObject _FadeIn;

    public GameObject[] _NodeBoomEffect = new GameObject[4];

    public GameObject _Panel;

    public GameObject _FailBar;
    public GameObject[] _FailBlock = new GameObject[4];
    public int _FailCount;

    public string _FileName;
    public GameObject _Line;
    public GameObject _Node;
    public UILabel _Timer;
    public float _NowTime;
    public int _MinListNum=0;
    int _MaxListNum = 5;
    public bool _Starting;

    public GameObject _InvasionLadder;

    public int[] _PlayerNumverArr = new int[4];

    public Animator _BgAnimator;
    public Invasion_Character[] _CharacterArray = new Invasion_Character[4];

    public int _PattunTotalNode;
    Dictionary<string, string> dicJson = new Dictionary<string, string>();

    public List<PlayNode> _PlayNodeList = new List<PlayNode>();

    bool _GameOver;

    public void Init()
    {
        //string path = @"c:\Editor\" + _FileName + ".txt";
        //string path = "Assets/Resources/Pattuns/" + _FileName + ".txt";
        string textvalue = Resources.Load<TextAsset>("Pattuns/" + _FileName.ToString()).text;
        //string textValue = System.IO.File.ReadAllText(path);
        dicJson = Json.Read(textvalue);

        //string path_total = @"c:\Editor\" + _FileName + "_lenght.txt";
        //string path_total = "Assets/Resources/Pattuns/" + _FileName + "_lenght.txt";
        //_PattunTotalNode = int.Parse(System.IO.File.ReadAllText(path_total));
        string path_total = Resources.Load<TextAsset>("Pattuns/" + _FileName.ToString()+"_lenght").text;
        _PattunTotalNode = int.Parse(path_total);
        _Starting = true;


    }

    void GameOver()
    {
        GameObject obj = NGUITools.AddChild(gameObject.transform.parent.gameObject, _FadeIn);
        StartCoroutine(GoScene(1.0f));
    }

    IEnumerator GoScene(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("OverScene");
    }
	
    void Start()
    {
        StartMusic();
    }

	void Update () 
    {
        if(_FailCount>=5 &&!_GameOver)
        {
            GameOver();
            _GameOver = true;
        }
        if (_Starting)
        {
            _Timer.text = _NowTime.ToString();
            _NowTime += Time.smoothDeltaTime;
            int _Max = _MaxListNum;
            if (_Max > _PattunTotalNode)
                _Max = _PattunTotalNode;
            for (int i = _MinListNum; i < _Max; i++)
            {
                if (int.Parse(dicJson["NodeSeq_" + i.ToString()]) == i)
                {
                    if (float.Parse(dicJson["Time_" + i.ToString()]) <= _NowTime)
                    {
                        GameObject obj = NGUITools.AddChild(_Line, _Node);
                        obj.transform.localPosition = new Vector3(1000, 0, 0);
                        obj.GetComponent<PlayNode>().Init(int.Parse(dicJson["PattunNum_" + i.ToString()]));
                        //node
                        _PlayNodeList.Add(obj.GetComponent<PlayNode>());
                        _MaxListNum++;
                        _MinListNum++;
                    }
                }
            }
            for (int i = 0; i < _PlayNodeList.Count;i++ )
            {
                if (PlayerMng.Data._SUPERPOWER)
                {
                    if (_PlayNodeList[i]._NowPos.x <= -800.0f)
                    {
                        HitNodeCheck(_PlayNodeList[i]._NodeNum);
                    }
                }
                else
                {
                    if (_PlayNodeList[i]._NowPos.x < -900.0f)
                    {
                        _CharacterArray[_PlayNodeList[i]._NodeNum - 1].MissNode();
                        GameObject obj = NGUITools.AddChild(_FailBar, _FailBlock[_PlayNodeList[i]._NodeNum - 1]);
                        obj.transform.localPosition = new Vector3(227 * (_FailCount - 2), 0, 0);

                        _FailCount++;
                        Destroy(_PlayNodeList[i].gameObject);
                        _PlayNodeList.RemoveAt(i);
                        i--;
                    }
                } 
            }
            for (int i = 0; i < PlayerMng._MaxPlayerCount;i++ )
            {
                if(Input.GetButtonDown("Player" + _PlayerNumverArr[i].ToString() + "_O"))
                {

                    
                    HitNodeCheck(i+1);
                }
            }

            //    if (Input.GetKeyDown(KeyCode.A))
            //    {
            //        
            //    }
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    HitNodeCheck(2);
            //}
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    HitNodeCheck(3);
            //}
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    HitNodeCheck(4);
            //}
        }
        

        
	}

    public void ClearInvasion()
    {
        _CharacterArray[0].StartUpRadder();
        _CharacterArray[1].StartUpRadder();
        _CharacterArray[2].StartUpRadder();
        _CharacterArray[3].StartUpRadder();
        //_BgAnimator.Stop();
        StartCoroutine(StopInvasion(1.5f));
    }

    public void CreateLadder()
    {

        GameObject ladder = NGUITools.AddChild(_Panel, _InvasionLadder);
        
    }

    void HitNodeCheck(int nodenum)
    {
        if(_PlayNodeList.Count>0)
        {
            if (Mathf.Abs(-800 - _PlayNodeList[0]._NowPos.x) <= 100)
            {
                if (nodenum == _PlayNodeList[0]._NodeNum)
                {
                    //hit
                    PlayerMng.Data._GameScore += 50;
                    GameObject effect = NGUITools.AddChild(_Line, _NodeBoomEffect[nodenum-1]);
                    effect.transform.localPosition = _PlayNodeList[0]._NowPos;
                    effect.transform.localScale = new Vector3(1, 1, 1);
                    Destroy(_PlayNodeList[0].gameObject);
                    _PlayNodeList.RemoveAt(0);

                }
            }
        }
        
    }

    public void StartMusic()
    {
        StartCoroutine(StartMusic_C(1.0f));//노드딜레이(날아오는속도)
    }

    IEnumerator StartMusic_C(float time)
    {
        yield return new WaitForSeconds(time);

        _LobbyMng._SoundMng._MySource.Play();
        //Debug.Log("Start");
    }

    IEnumerator StopInvasion(float time)
    {
        yield return new WaitForSeconds(time);
        _SoundMng.StopInvasion();
    }
}
