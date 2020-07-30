using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HackingGame : MonoBehaviour {

    public GameObject _BlockGrid;
    public GameObject _BlockObject;
    public UILabel _FailCount;

    bool _Warninging;
    public GameObject _Hacker_WarningPanel;
    public GameObject _Hacker_VirusEffect;

    public UI2DSprite _PointGazeSprite;

    public Animator _CameraAnimator;

    public bool _CanPlaying;

    int _Point;
    public int _Fail;

    public int _HackingCount;

    public int _PlayerNumber;

    public int _HackingPoint;

    public List<int> _BlockList = new List<int>();
    public List<HackingGame_Block> _BlockList_Obj = new List<HackingGame_Block>();
    int _MaxBlockList = 7;

    public GameObject _PanelParent;
    public GameObject _ScreenAnimation;
    public GameObject _Screen_Map;
    public GameObject _FadeOut;
    public UILabel _HackingCountLabel;

    public Player_InHouse _Hacker_Player;

    public GameObject _BlockClearEffect;
    public GameObject _BlockClearEffect_Virus;
    public GameObject _BlockClearEffect_Miss;

    public bool _CanOff;

	void Start () 
    {
        _CanPlaying = true;
	    for(int i=0;i<_MaxBlockList;i++)
        {
            CreateBlock();
        }
	}

    public void OffGame()
    {
        _PanelParent.transform.DestroyChildren();
    }

    public void StartHacking(int floor)
    {
        
        GameObject obj1 = NGUITools.AddChild(_PanelParent, _FadeOut);
        obj1.transform.localPosition = new Vector3(0, 0, 0);
        GameObject obj2 = NGUITools.AddChild(_PanelParent, _ScreenAnimation);
        obj2.transform.localPosition = new Vector3(0,0,0);
        StartCoroutine(OpenScreen(1.1f,floor));
    }

    IEnumerator OpenScreen(float time,int floor)
    {
        yield return new WaitForSeconds(time);
        _CanOff = true;
        GameObject obj2 = NGUITools.AddChild(_PanelParent, _Screen_Map);
        obj2.transform.localPosition = new Vector3(180, 130, 1);
        obj2.GetComponent<UISprite>().spriteName = "hacking_map_"+floor.ToString();
        _Hacker_Player._HackerMap = obj2.GetComponent<UISprite>();

        _Hacker_Player.SetCCTVListOn();
    }
	
	void Update () 
    {
        
        _FailCount.text = _Fail.ToString();
        _HackingCountLabel.text = "x" + _HackingCount.ToString();

        _PointGazeSprite.fillAmount = (float)((float)_Point / 10000.0f);
        if(_Point>=10000)
        {
            _Point -= 10000;
            _HackingCount++;
        }

        if (_CanPlaying)
        {
            if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L1"))
            {
                CrashBlock(0);
            }
            else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R1"))
            {
                CrashBlock(1);
            }
        }
	    
	}

    void CrashBlock(int num)
    {
        bool value = _BlockList_Obj[0]._VirusBlock;
        Destroy(_BlockList_Obj[0].gameObject);
        _BlockList_Obj.RemoveAt(0);
        for (int i = 0; i < _BlockList_Obj.Count;i++ )
        {
            _BlockList_Obj[i].MoveDown();
        }
            //_BlockGrid.hideInactive = !_BlockGrid.hideInactive;
            if (num == _BlockList[0])
                HitBlock(num);
            else
                WrongBlock(value, num);
        _BlockList.RemoveAt(0);
        CreateBlock();
    }

    void CreateBlock()
    {
        int num = Random.Range(0, 2);
        _BlockList.Add(num);

        GameObject obj = NGUITools.AddChild(_BlockGrid,_BlockObject);

        bool virus = false;
        int rand = Random.Range(0,10);
        if(rand==3)
            virus=true;
        obj.GetComponent<HackingGame_Block>().Init(num, virus);
        _BlockList_Obj.Add(obj.GetComponent<HackingGame_Block>());
        obj.transform.localPosition = new Vector3(0, (_BlockList_Obj.Count-1)*170, 0);
        //_BlockGrid.hideInactive = !_BlockGrid.hideInactive;

    }

    void HitBlock(int num)
    {
        _Point += 250;
        PlayerMng.Data._GameScore += 20;
        GameObject effect = NGUITools.AddChild(_BlockGrid,_BlockClearEffect);

        if (num == 0)
            effect.transform.localPosition = new Vector3(-175, 0, 0);
        else
            effect.transform.localPosition = new Vector3(175, 0, 0);
    }
    void WrongBlock(bool virus, int num)
    {
        _CameraAnimator.SetTrigger("shake");
        _Point -= 500;
        if (_Point <= 0.0f)
            _Point = 0;
        _Fail++;
        if(virus)
        {
            PlayerMng.Data._GameScore -= 1000;
            //
            GameObject effect = NGUITools.AddChild(_BlockGrid, _BlockClearEffect_Virus);
            if (num == 0)
                effect.transform.localPosition = new Vector3(-175, 0, 0);
            else
                effect.transform.localPosition = new Vector3(175, 0, 0);

            if(!_Warninging)
            {
                if (_HackingCount > 0)
                    _HackingCount--;
                _Warninging = true;
                GameObject effect1 = NGUITools.AddChild(_Hacker_WarningPanel, _Hacker_VirusEffect);
                StartCoroutine(DestroyVirus(5.0f));
            }
        }
        else
        {
            PlayerMng.Data._GameScore -= 200;
            GameObject effect = NGUITools.AddChild(_BlockGrid, _BlockClearEffect_Miss);
            if (num == 0)
                effect.transform.localPosition = new Vector3(-175, 0, 0);
            else
                effect.transform.localPosition = new Vector3(175, 0, 0);
        }
    }

    IEnumerator DestroyVirus(float time)
    {
        yield return new WaitForSeconds(time);

        _Warninging = false;
    }
}
