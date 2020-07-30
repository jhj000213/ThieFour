using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMng : MonoBehaviour
{

    private static PlayerMng instance = null;

    public static PlayerMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(PlayerMng)) as PlayerMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    public bool _SUPERPOWER;

    public GameObject _SettingPanel;
    public GameObject _FadeIn;
    public GameObject _FadeOut;

    public GameObject _CameraSelectEffect_1;

    public LayerMask _WallLayer;
    public LayerMask _ObjectLayer;
    public LayerMask _GuardLayer;

    public GameObject[] _WallObjectArray;
    public GameObject[] _ObjectArray;
    public List<GameObject> _GuardList;

    public GameObject _SequrityGuard;
    public GameObject _GetSleepBulletEffect;

    public GameObject _PortalPos_FirstFloor;
    public GameObject _PortalPos_SecondFloor;
    public GameObject _UIRoot;
    public GameObject _FadeEffect_Step;

    public const int _MaxPlayerCount = 4;
    public bool _ControllerSettingFinish;

    public GameObject _CameraSelectScene;
    public GameObject _JobSelectScene;
    public GameObject _GameingScene;

    public GameObject _SettingRoot;

    public Camera _HackingGameCamera;
    public Camera _PickingGameCamera;

    public List<int> _ControllerNumberList = new List<int>();

    public List<int> _NowLockedCamera = new List<int>();
    public List<int> _NowLockedJob = new List<int>();

    public GameObject _InvasionScene;
    public InvasionMng _InvasionMng;

    public GameObject _CCTVDangerEffect;
    public GameObject _GuardCreate_Circle;
    public GameObject _GuardCreate_Dust;

    public Vector3 _PickerHoldPosition;
    public Vector3 _Picker_SafePosition;


    public GameObject _InHouseScene;
    public Player_InHouse[] _PlayerArray = new Player_InHouse[_MaxPlayerCount];
    public PickingGame _PickingGameMng;
    public HackingGame _HackingGameMng;
    public PickingGame_JoyStick _PickingGameJoyStick;

    public GameObject[] _CameraSelectBox = new GameObject[4];
    public GameObject[] _CameraTable = new GameObject[4];
    public GameObject[] _JobSelector = new GameObject[4];

    public int[] _NowLockedCamera_Array = new int[4];
    public int[] _NowLockedPlayerNumber_Array = new int[4];
    public int[] _NowLockedJob_Array = new int[4];

    public float _SafeGuardCallTime;
    public float _SafeGuardCallTime_Delay = 8.0f;
    public GameObject _GuardHitEffect;
    public GameObject _GuardHitRed;

    bool _StartInvasion;

    float _PlayTime;
    public int _GameScore;
    void Update()
    {
        bool clear = true;
        for (int i = 0; i < 4; i++)
        {
            if (!_PlayerArray[i]._EscapingClear)
                clear = false;
        }
        if (clear)
        {
            StaticDataMng._ClearTime = _PlayTime;
            StaticDataMng._Score = _GameScore;
            SceneManager.LoadScene("ClearScene");
        }

        for (int i = 0; i < 4;i++ )
        {
            if(_PlayerArray[i]._Dead)
                SceneManager.LoadScene("OverScene");
        }

        if (_InHouseScene.activeSelf)
        {
            _PlayTime += Time.smoothDeltaTime;
        }

            _SafeGuardCallTime -= Time.smoothDeltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _SUPERPOWER = true;
        }

        if (_CameraSelectScene.activeSelf)
        {
            for (int i = 1; i < 12; i++)
            {
                bool check = false;
                for (int j = 0; j < _ControllerNumberList.Count; j++)
                {
                    if (_ControllerNumberList[j] == i)
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    if (Input.GetButtonDown("Player" + i.ToString() + "_Share"))
                    {
                        _CameraSelectBox[_ControllerNumberList.Count].SetActive(true);
                        _CameraSelectBox[_ControllerNumberList.Count].GetComponent<SelectingPlayer>()._PlayerNumber = i;
                        _CameraSelectBox[_ControllerNumberList.Count].GetComponent<SelectingPlayer>()._NowSelectCameraTable = _ControllerNumberList.Count + 1;
                        _ControllerNumberList.Add(i);

                    }
                }
            }
            if (_NowLockedCamera.Count == _MaxPlayerCount)
            {
                //
                StartCoroutine(ChangePanel(0.1f, 1));
                _NowLockedCamera.Clear();
            }
        }
        if (_JobSelectScene.activeSelf && _StartInvasion == false)
        {
            if (_NowLockedJob.Count == _MaxPlayerCount)
            {
                //GameStart();
                _StartInvasion = true;
                //StartCoroutine(StartInvasion_C(1.5f));
                StartCoroutine(ChangePanel(1.5f,3));
                _NowLockedJob.Clear();
            }
        }
    }
    void SelectJobScene()
    {
        _CameraSelectScene.SetActive(false);
        _JobSelectScene.SetActive(true);

        //for (int i = 0; i < _MaxPlayerCount; i++)
        //{
        //    _JobSelector[_NowLockedCamera_Array[i] - 1].GetComponent<SelectingJob>()._PlayerNumber = _NowLockedPlayerNumber_Array[i];
        //}
    }

    IEnumerator StartInvasion_C(float time)
    {
        yield return new WaitForSeconds(time);

        InvasionStart();
    }

    void InvasionStart()
    {
        _CameraSelectScene.SetActive(false);
        _JobSelectScene.SetActive(false);
        _InvasionScene.SetActive(true);


        _InvasionMng._FileName = "invasion";
        _InvasionMng.Init();
        for (int i = 0; i < _MaxPlayerCount; i++)
        {
            int characternum = _NowLockedJob_Array[i];
            _InvasionMng._PlayerNumverArr[characternum - 1] = _NowLockedPlayerNumber_Array[i];

        }
        _SettingRoot.SetActive(false);
        StartCoroutine(EndInvasion(57.0f));
        StartCoroutine(LaddingCharacter(52.0f));
        StartCoroutine(CreateLadder(50.0f));//위에꺼 -2초

        //StartCoroutine(EndInvasion(2));
        //StartCoroutine(LaddingCharacter(0));
        //StartCoroutine(CreateLadder(0));//위에꺼 -2초
    }

    IEnumerator LaddingCharacter(float time)
    {
        yield return new WaitForSeconds(time);

        _InvasionMng.ClearInvasion();
    }

    IEnumerator CreateLadder(float time)
    {
        yield return new WaitForSeconds(time);

        _InvasionMng.CreateLadder();
    }

    IEnumerator EndInvasion(float time)
    {
        yield return new WaitForSeconds(time);

        GameStart();
    }
    void GameStart()
    {
        _SUPERPOWER = false;
        _CameraSelectScene.SetActive(false);
        _JobSelectScene.SetActive(false);
        _InvasionScene.SetActive(false);
        _GameingScene.SetActive(true);
        _InHouseScene.SetActive(true);

        for (int i = 0; i < _WallObjectArray.Length; i++)
        {

            for (int j = 0; j < _WallObjectArray[i].transform.childCount; j++)
            {
                _WallObjectArray[i].transform.GetChild(j).transform.gameObject.layer = LayerMask.NameToLayer("wall");
            }
        }
        for (int i = 0; i < _ObjectArray.Length; i++)
        {
            for (int j = 0; j < _ObjectArray[i].transform.childCount; j++)
            {
                _ObjectArray[i].transform.GetChild(j).gameObject.layer = LayerMask.NameToLayer("object");
            }
        }

        int[] on1 = new int[5];
        for (int i = 0; i < 5; i++)
        {
            while (true)
            {
                int num = Random.Range(1, 6);
                bool check = false;
                for (int j = 0; j < i; j++)
                {
                    if (on1[j] == num)
                        check = true;
                }
                if (!check)
                {
                    on1[i] = num;
                    break;
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (on1[i] == 1 || on1[i] == 2 || on1[i] == 3)
                _GuardList[i].GetComponent<SequrityGuard>()._HaveSleepBullet = true;
            _GuardList[i].layer = LayerMask.NameToLayer("Water");
        }


        int[] on2 = new int[5];
        for (int i = 0; i < 5; i++)
        {
            while (true)
            {
                int num = Random.Range(1, 6);
                bool check = false;
                for (int j = 0; j < i; j++)
                {
                    if (on2[j] == num)
                        check = true;
                }
                if (!check)
                {
                    on2[i] = num;
                    break;
                }
            }
        }

        for (int i = 5; i < 10; i++)
        {
            if (on2[i-5] == 1 || on2[i-5] == 2 || on2[i-5] == 3)
                _GuardList[i].GetComponent<SequrityGuard>()._HaveSleepBullet = true;
            _GuardList[i].layer = LayerMask.NameToLayer("Water");
        }





        for (int i = 0; i < _MaxPlayerCount; i++)
        {
            int characternum = _NowLockedJob_Array[i];

            _PlayerArray[characternum - 1].SetPlayerNumber(_NowLockedPlayerNumber_Array[i]);
            if (characternum == 1)
                _HackingGameMng._PlayerNumber = _NowLockedPlayerNumber_Array[i];
            if (characternum == 3)
            {
                _PickingGameMng._PlayerNumber = _NowLockedPlayerNumber_Array[i];
                _PickingGameJoyStick._PlayerNumber = _NowLockedPlayerNumber_Array[i];
            }

            if (i == 0)
                _PlayerArray[characternum - 1]._MyCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            else if (i == 1)
                _PlayerArray[characternum - 1]._MyCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            else if (i == 2)
                _PlayerArray[characternum - 1]._MyCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
            else if (i == 3)
                _PlayerArray[characternum - 1]._MyCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            if (characternum == 1)
            {
                if (i == 0)
                    _HackingGameCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                else if (i == 1)
                    _HackingGameCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (i == 2)
                    _HackingGameCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
                else if (i == 3)
                    _HackingGameCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            }
            if (characternum == 3)
            {
                if (i == 0)
                    _PickingGameCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                else if (i == 1)
                    _PickingGameCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (i == 2)
                    _PickingGameCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
                else if (i == 3)
                    _PickingGameCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            }
            _PlayerArray[characternum - 1].Init();
        }
    }

    IEnumerator ChangePanel(float time,int num)
    {
        yield return new WaitForSeconds(time);

        GameObject obj = NGUITools.AddChild(_SettingPanel, _FadeIn);
        if (num == 3)
            StartCoroutine(StartInvasion_C(1.0f));
        else
            StartCoroutine(Step1(1.0f, num));
    }
    IEnumerator Step1(float time,int num)
    {
        yield return new WaitForSeconds(time);

        GameObject obj = NGUITools.AddChild(_SettingPanel, _FadeOut);

        if(num==1)
            SelectJobScene();

        StartCoroutine(Step2(1.0f, num));
    }
    IEnumerator Step2(float time,int num)
    {
        yield return new WaitForSeconds(time);


    }
}