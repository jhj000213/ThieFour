using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_InHouse : MonoBehaviour
{

    public int _HP = 2;
    public bool _Dead;

    public Player_InHouse _EscaperCharacter;
    public Player_InHouse _PickerCharacter;

    public CameraMng _CameraMng;

    [HideInInspector]
    public InHouse_Character _MyCharacterFrameWork;
    public Rigidbody2D _MyRigidBody;

    public Player_JoyStick_Move _JoyStick_Move;
    public Player_JoyStick_Aim _JoyStick_Aim;

    public GameObject _Silencer_Machine;
    public GameObject _Silencer_ZoomImage;
    public GameObject _Silencer_ZoomShoot;
    public GameObject _Silencer_MachineIcon;
    public UILabel _Silencer_BulletValueLabel;
    bool _Silencer_HaveMachine;
    public GameObject _Silencer_SetMachine;
    public GameObject _Silencer_GetBullet;
    public int _Silencer_BulletCount;

    public GameObject _GunMouse;
    public GameObject _GunMouseSprite;
    public GameObject _UIRoot;
    public GameObject _Bullet;

    public GameObject _HackerUI;
    public GameObject _SilencerUI;
    public GameObject _PickerUI;
    public GameObject _EscaperUI;

    public GameObject[] _HPImage = new GameObject[2];

    public GameObject _BulletEffect;

    bool _PerpectHacking;
    public HackingGame _HackingGameMng;
    public List<SequrityCCTV> _CCTVList_1floor = new List<SequrityCCTV>();
    public List<SequrityCCTV> _CCTVList_2floor = new List<SequrityCCTV>();
    public List<GameObject> _CCTVList_Icon_1floor = new List<GameObject>();
    public List<GameObject> _CCTVList_Icon_2floor = new List<GameObject>();
    Vector3 _CCTVLockPoint_DesPos;
    public int _CCTVListNumber;
    public GameObject _CCTVLockPoint;
    public GameObject _CCTVLockPoint_Ani;
    public GameObject _CCTVIconFlashAni;
    bool _CenterAxis_X;
    int _HackingMap_Floor;
    public UISprite _HackerMap;
    bool _MapFloorChanging;
    public GameObject _CCTVListPanel;
    public GameObject _CCTVListPanel_Floor1;
    public GameObject _CCTVListPanel_Floor2;

    public List<EscaperItem> _EscaperItemList = new List<EscaperItem>();
    public bool _Escaper_Camouflage;
    public float _Escaper_Camouflage_CoolTime;
    public float _Escaper_Camouflage_Duration;
    public int _Escaper_ItemValue;
    public GameObject _Escaper_CamoGazeBar;
    public UI2DSprite _Escaper_CamoCoolTimeGaze;
    public UI2DSprite _Escaper_CamoDurationGaze;
    public UILabel _Escaper_ItemValueLabel;
    public List<Escaper_Window> _Escaper_Window = new List<Escaper_Window>();
    public GameObject _Escaper_TransformEffect;

    public GameObject _Picker_Safe;
    public PickingGame _PickingMng;
    public GameObject _PickerTresureItem;

    public Camera _MyCamera;

    //public UILabel _HPLabel;

    public int _PlayerNumber;

    public bool _DoingMyWork1;
    public bool _DoingMyWork2;

    float _ShootDelay;
    float _ShootDelayMax = 0.33f;

    float _MoveSpeed = 0.35f;

    float _WalkSpeed = 0.35f;
    float _RunSpeed = 0.51f;

    bool _Changing;
    public bool _EscapingClear;

    public LayerMask WallLayer;

    void RazerAimSet()
    {
        Vector3 zeropoint = _GunMouseSprite.transform.InverseTransformPoint(transform.position);
        Vector3 hitpoint = new Vector3();

        float angle = transform.localEulerAngles.z + 90;
        Vector3 pluspos = new Vector3(Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad) * 0.03f, Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad) * 0.03f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + pluspos + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 0.11f, Mathf.Sin(angle * Mathf.Deg2Rad) * 0.11f),
            transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 4000.0f, Mathf.Sin(angle * Mathf.Deg2Rad) * 4000.0f, 0), 9000, WallLayer);
        if (hit.collider != null)
        {
            hitpoint = _GunMouseSprite.transform.InverseTransformPoint(hit.point);

            float range = Vector3.Distance(zeropoint, hitpoint);
            _GunMouseSprite.GetComponent<UI2DSprite>().width = (int)range;
        }
    }


    void Start()
    {
        
        
    }

    public void Init()
    {
        _MyCharacterFrameWork = GetComponent<InHouse_Character>();
        _MyRigidBody = transform.parent.GetComponent<Rigidbody2D>();
        _HP = 2;
        if (_MyCharacterFrameWork._MyJob == "hacker")
        {

            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            _HackerUI.transform.localPosition = pos;
        }
        if (_MyCharacterFrameWork._MyJob == "silencer")
        {
            _Silencer_HaveMachine = true;
            _Silencer_BulletCount = 3;

            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            _SilencerUI.transform.localPosition = pos;
        }
        if (_MyCharacterFrameWork._MyJob == "picker")
        {
            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            _PickerUI.transform.localPosition = pos;
        }
        if (_MyCharacterFrameWork._MyJob == "escaper")
        {
            _Escaper_Camouflage_CoolTime = 30.0f;

            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            _EscaperUI.transform.localPosition = pos;

            _WalkSpeed = 0.5f;
            _RunSpeed = 0.85f;
        }
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            _HPImage[i].SetActive(false);
        }
        for (int i = 0; i < _HP;i++ )
        {
            _HPImage[i].SetActive(true);
        }
            //_HPLabel.text = _HP.ToString();
            _ShootDelay -= Time.smoothDeltaTime;
        if(_EscapingClear)
        {

        }
        else if (_PlayerNumber != 0)
        {
            if(_Dead)
            {

            }
            else
            {
                if (_MyCharacterFrameWork._MyJob == "hacker")
                {
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L2") && !(_CCTVList_1floor.Count == 0 && _CCTVList_2floor.Count == 0))
                    {
                        if (!_Changing && !_DoingMyWork2)
                        {
                            _DoingMyWork1 = !_DoingMyWork1;
                            if (_DoingMyWork1)
                            {
                                //hacking
                                StartCoroutine(SetHacking(3));
                                _MyRigidBody.isKinematic = true;
                                _MyCharacterFrameWork.SetAnimation("openlaptop", "hacking", false);
                                _Changing = true;
                                _CenterAxis_X = true;

                                if (_CCTVList_1floor.Count == 0)
                                {
                                    _HackingMap_Floor = 2;
                                    _CCTVListPanel_Floor1.SetActive(false);
                                    _CCTVListPanel_Floor2.SetActive(true);
                                }
                                else
                                {
                                    _HackingMap_Floor = 1;
                                    _CCTVListPanel_Floor1.SetActive(true);
                                    _CCTVListPanel_Floor2.SetActive(false);
                                }

                            }
                            else
                            {
                                _DoingMyWork1 = true;
                                //hacking
                                if (_HackingGameMng._CanOff)
                                {
                                    _HackingGameMng.OffGame();
                                    _CCTVListPanel.SetActive(false);
                                    _DoingMyWork1 = false;
                                    _HackingGameMng._CanOff = false;
                                    _CCTVLockPoint.SetActive(false);
                                    _CameraMng._HackerCamera_Hacking.SetActive(false);
                                    _CameraMng._HackerCamera_InHouse.SetActive(true);
                                    _MyRigidBody.isKinematic = false;
                                    _MyCharacterFrameWork.SetAnimation("closelaptop", "stay", false);
                                }
                            }


                        }
                    }
                    if (_DoingMyWork1)
                    {

                        if (_HackingGameMng._CanOff)
                        {

                            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") == 0)
                                _CenterAxis_X = true;

                            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") > 0 && _CenterAxis_X)//오른쪽
                            {
                                _CenterAxis_X = false;
                                _CCTVListNumber++;
                                //Debug.Log(_CCTVListNumber);
                                if (_HackingMap_Floor == 1)
                                {
                                    if (_CCTVListNumber >= _CCTVList_1floor.Count)
                                        _CCTVListNumber = 0;
                                    _CCTVLockPoint_DesPos = _CCTVList_Icon_1floor[_CCTVListNumber].transform.localPosition;
                                }
                                else
                                {
                                    if (_CCTVListNumber >= _CCTVList_2floor.Count)
                                        _CCTVListNumber = 0;
                                    _CCTVLockPoint_DesPos = _CCTVList_Icon_2floor[_CCTVListNumber].transform.localPosition;
                                }

                            }
                            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") < 0 && _CenterAxis_X)//왼쪽
                            {
                                _CenterAxis_X = false;
                                _CCTVListNumber--;
                                if (_HackingMap_Floor == 1)
                                {
                                    if (_CCTVListNumber < 0)
                                        _CCTVListNumber = _CCTVList_1floor.Count - 1;
                                    _CCTVLockPoint_DesPos = _CCTVList_Icon_1floor[_CCTVListNumber].transform.localPosition;
                                }
                                else
                                {
                                    if (_CCTVListNumber < 0)
                                        _CCTVListNumber = _CCTVList_2floor.Count - 1;
                                    _CCTVLockPoint_DesPos = _CCTVList_Icon_2floor[_CCTVListNumber].transform.localPosition;
                                }
                            }
                            if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_O"))
                            {
                                if (_HackingGameMng._HackingCount>0)
                                {
                                    _HackingGameMng._HackingCount--;
                                    if (_HackingMap_Floor == 1)
                                    {
                                        PlayerMng.Data._GameScore += 1000;
                                        _CCTVList_1floor[_CCTVListNumber].GetComponent<SequrityCCTV>().CCTVOff();
                                        _CCTVList_1floor.RemoveAt(_CCTVListNumber);
                                        _CCTVList_Icon_1floor[_CCTVListNumber].SetActive(false);


                                        GameObject obj = NGUITools.AddChild(_CCTVList_Icon_1floor[_CCTVListNumber].transform.parent.gameObject, _CCTVIconFlashAni);
                                        obj.transform.localPosition = _CCTVList_Icon_1floor[_CCTVListNumber].transform.localPosition;

                                        _CCTVList_Icon_1floor.RemoveAt(_CCTVListNumber);


                                    }
                                    else
                                    {
                                        _CCTVList_2floor[_CCTVListNumber].GetComponent<SequrityCCTV>().CCTVOff();
                                        _CCTVList_2floor.RemoveAt(_CCTVListNumber);
                                        _CCTVList_Icon_2floor[_CCTVListNumber].SetActive(false);
                                        GameObject obj = NGUITools.AddChild(_CCTVList_Icon_2floor[_CCTVListNumber].transform.parent.gameObject, _CCTVIconFlashAni);
                                        obj.transform.localPosition = _CCTVList_Icon_2floor[_CCTVListNumber].transform.localPosition;
                                        _CCTVList_Icon_2floor.RemoveAt(_CCTVListNumber);
                                    }
                                    GameObject effect = NGUITools.AddChild(_CCTVLockPoint, _CCTVLockPoint_Ani);
                                    if (_HackingMap_Floor == 1 && _CCTVList_1floor.Count != 0)
                                    {
                                        if (_CCTVListNumber >= _CCTVList_1floor.Count)
                                            _CCTVListNumber = 0;
                                        _CCTVLockPoint_DesPos = _CCTVList_Icon_1floor[_CCTVListNumber].transform.localPosition;
                                    }
                                    else if (_HackingMap_Floor == 2 && _CCTVList_2floor.Count != 0)
                                    {
                                        if (_CCTVListNumber >= _CCTVList_2floor.Count)
                                            _CCTVListNumber = 0;
                                        _CCTVLockPoint_DesPos = _CCTVList_Icon_2floor[_CCTVListNumber].transform.localPosition;
                                    }
                                }

                            }
                            if ((_HackingMap_Floor == 1 && _CCTVList_1floor.Count == 0) || (_HackingMap_Floor == 2 && _CCTVList_2floor.Count == 0))
                            {
                                _HackingGameMng._CanOff = false;
                                _MapFloorChanging = true;
                                if (_HackingMap_Floor == 1)
                                {
                                    _HackingMap_Floor = 2;
                                    _CCTVListNumber = 0;
                                }
                                else
                                {
                                    _HackingMap_Floor = 1;
                                    _CCTVListNumber = 0;
                                }
                                _HackerMap.GetComponent<Animator>().SetTrigger("changefloor");
                                StartCoroutine(SetChangingMapValue(1.1f));
                                StartCoroutine(ChangeHackerMiniMap(0.4f));
                                StartCoroutine(SetHackerMapAlpha(0.01f));
                            }
                            else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R2"))
                            {
                                if (!_MapFloorChanging)
                                {
                                    _HackingGameMng._CanOff = false;
                                    _MapFloorChanging = true;
                                    if (_HackingMap_Floor == 1 && _CCTVList_2floor.Count != 0)
                                    {
                                        _HackingMap_Floor = 2;
                                        _CCTVListNumber = 0;
                                    }
                                    else if (_HackingMap_Floor == 2 && _CCTVList_1floor.Count != 0)
                                    {
                                        _HackingMap_Floor = 1;
                                        _CCTVListNumber = 0;
                                    }
                                    _HackerMap.GetComponent<Animator>().SetTrigger("changefloor");
                                    StartCoroutine(SetChangingMapValue(1.1f));
                                    StartCoroutine(ChangeHackerMiniMap(0.4f));
                                    StartCoroutine(SetHackerMapAlpha(0.01f));
                                }

                            }
                        }
                        if (_CCTVList_1floor.Count == 0 && _CCTVList_2floor.Count == 0)
                        {
                            _PerpectHacking = true;
                            _HackingGameMng.OffGame();
                            _CCTVListPanel.SetActive(false);
                            _DoingMyWork1 = false;
                            _HackingGameMng._CanOff = false;
                            _CCTVLockPoint.SetActive(false);
                            _CameraMng._HackerCamera_Hacking.SetActive(false);
                            _CameraMng._HackerCamera_InHouse.SetActive(true);
                            _MyRigidBody.isKinematic = false;
                            _MyCharacterFrameWork.SetAnimation("coselaptop", "stay", false);
                        }
                        _CCTVLockPoint.transform.localPosition = Vector3.MoveTowards(_CCTVLockPoint.transform.localPosition, _CCTVLockPoint_DesPos, 120);
                    }
                }
                else if (_MyCharacterFrameWork._MyJob == "silencer")
                {
                    _Silencer_BulletValueLabel.text = "x" + _Silencer_BulletCount.ToString();
                    if (_DoingMyWork1)
                    {
                        RazerAimSet();

                        //float angle = transform.localEulerAngles.z-180;
                        //Vector3 mypos = transform.localPosition + new Vector3(Mathf.Cos(angle) * 18, Mathf.Sin(angle) * 18, 0);
                        Vector3 mypos = transform.parent.InverseTransformPoint(_GunMouseSprite.transform.position);

                        _MyCamera.transform.localPosition += _JoyStick_Move._NowPoint * 3.0f;
                        _GunMouse.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_MyCamera.transform.localPosition.y - mypos.y, _MyCamera.transform.localPosition.x - mypos.x) * Mathf.Rad2Deg);

                        if (Vector3.Distance(transform.localPosition, _MyCamera.transform.localPosition) > 100)
                            transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_MyCamera.transform.localPosition.y - mypos.y, _MyCamera.transform.localPosition.x - mypos.x) * Mathf.Rad2Deg - 90);
                    }
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L2"))
                    {
                        if(_ShootDelay<=0.0f)
                        {
                            _DoingMyWork1 = !_DoingMyWork1;
                            if (_DoingMyWork1)
                            {
                                Vector3 pos = new Vector3();
                                if (_MyCamera.rect.x == 0)
                                    pos.x = -960;
                                else
                                    pos.x = 0;
                                if (_MyCamera.rect.y == 0)
                                    pos.y = 0;
                                else
                                    pos.y = 540;
                                _Silencer_ZoomImage.transform.localPosition = pos;
                                _Silencer_ZoomImage.SetActive(true);
                                _MyCharacterFrameWork.SetAnimation("onsna", "zoom", true);
                                _MyCamera.transform.localPosition += new Vector3(0, 100, 0);
                                _ShootDelay = 0.875f;
                                //silencer
                                _GunMouseSprite.GetComponent<UI2DSprite>().width = 4000;
                                _GunMouseSprite.GetComponent<UI2DSprite>().height = 5;
                                _GunMouseSprite.transform.localPosition = new Vector3(0, -18, 0);
                                _MyRigidBody.isKinematic = true;
                            }
                            else
                            {
                                _Silencer_ZoomImage.SetActive(false);
                                _MyCharacterFrameWork.SetAnimation("offsna", "stay", true);
                                _ShootDelay = 0.625f;
                                //silencer
                                _GunMouseSprite.GetComponent<UI2DSprite>().width = 80;
                                _GunMouseSprite.GetComponent<UI2DSprite>().height = 10;
                                _GunMouseSprite.transform.localPosition = new Vector3(0, 0, 0);
                                _MyCamera.transform.localPosition = new Vector3(0, 0, 0);
                                _MyRigidBody.isKinematic = false;
                            }
                        }
                    }
                    else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R1"))
                    {
                        if (_ShootDelay <= 0.0f)
                        {
                            ShootSleepBullet();

                            _ShootDelay = _ShootDelayMax;
                        }
                    }
                    else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R2"))
                    {
                        if (!_DoingMyWork2 && !_DoingMyWork1)
                        {
                            if (_Silencer_HaveMachine)
                            {
                                _Silencer_HaveMachine = false;
                                _Silencer_MachineIcon.SetActive(false);
                                _DoingMyWork2 = true;
                                _MyCharacterFrameWork.SetAnimation("setmachine", "stay", false);

                                Vector3 pos = new Vector3(Mathf.Cos((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad) * 30.0f, Mathf.Sin((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad) * 30.0f, 0);
                                StartCoroutine(SetMachine(2.5f, pos + transform.parent.localPosition, transform.localEulerAngles.z));
                                StartCoroutine(SetDoingWork(2.75f, 2));
                            }
                            else
                            {
                                if (Vector3.Distance(transform.parent.localPosition, _Silencer_SetMachine.transform.localPosition) < 150.0f)
                                {
                                    Destroy(_Silencer_SetMachine);
                                    _Silencer_HaveMachine = true;
                                    _Silencer_MachineIcon.SetActive(true);
                                }
                            }

                        }

                    }
                }
                else if (_MyCharacterFrameWork._MyJob == "picker")
                {
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L2"))
                    {
                        if (Vector3.Distance(transform.parent.localPosition, _Picker_Safe.GetComponent<Picker_Safe>()._MousePos) < 80 && !_PickingMng._UnLocked)
                        {
                            _DoingMyWork1 = !_DoingMyWork1;
                            if (_DoingMyWork1)
                            {
                                //picking
                                _CameraMng._PickerCamera_Picking.SetActive(true);
                                _CameraMng._PickerCamera_InHouse.SetActive(false);
                                _MyRigidBody.isKinematic = true;
                                _Picker_Safe.GetComponent<Picker_Safe>()._MakeNoise = true;
                                transform.parent.localPosition = _Picker_Safe.GetComponent<Picker_Safe>()._MousePos;
                                transform.localEulerAngles = _Picker_Safe.transform.localEulerAngles;

                                _MyCharacterFrameWork.SetAnimation("openingready", "opening", false);
                            }
                            else
                            {
                                //picking
                                _CameraMng._PickerCamera_Picking.SetActive(false);
                                _CameraMng._PickerCamera_InHouse.SetActive(true);
                                _MyRigidBody.isKinematic = false;
                                _Picker_Safe.GetComponent<Picker_Safe>()._MakeNoise = false;
                            }
                        }

                    }
                }
                else if (_MyCharacterFrameWork._MyJob == "escaper" || _MyCharacterFrameWork._MyJob == "guard")
                {
                    _Escaper_Camouflage_CoolTime += Time.smoothDeltaTime;
                    if (_Escaper_Camouflage_Duration <= 0.0f)
                    {
                        _MyCharacterFrameWork._MyJob = "escaper";
                        if (_Escaper_CamoGazeBar.activeSelf == true)
                        {
                            GameObject effect = NGUITools.AddChild(_UIRoot, _Escaper_TransformEffect);
                            effect.transform.localPosition = transform.parent.localPosition;
                        }
                        _Escaper_CamoGazeBar.SetActive(false);
                    }
                    _Escaper_CamoCoolTimeGaze.fillAmount = (float)(_Escaper_Camouflage_CoolTime / 30.0f);
                    _Escaper_CamoDurationGaze.fillAmount = (float)(_Escaper_Camouflage_Duration / 20.0f);
                    _Escaper_ItemValueLabel.text = "x" + _Escaper_ItemValue.ToString();

                    if (_Escaper_Camouflage)
                    {
                        _Escaper_Camouflage_Duration -= Time.smoothDeltaTime;
                        if (_Escaper_Camouflage_Duration <= 0.0f)
                        {
                            _Escaper_Camouflage = false;
                        }
                    }
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L2"))
                    {
                        if (_Escaper_Camouflage_CoolTime >= 30.0f)
                        {
                            GameObject effect = NGUITools.AddChild(_UIRoot, _Escaper_TransformEffect);
                            effect.transform.localPosition = transform.parent.localPosition;

                            _Escaper_Camouflage = true;
                            _Escaper_Camouflage_CoolTime = 0.0f;
                            _Escaper_Camouflage_Duration = 20.0f;
                            _Escaper_CamoGazeBar.SetActive(true);
                            _MyCharacterFrameWork._MyJob = "guard";

                        }
                    }
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R2"))
                    {
                        for (int i = 0; i < _EscaperItemList.Count; i++)
                        {
                            if (Vector3.Distance(transform.parent.localPosition, _EscaperItemList[i].transform.localPosition) < 150)
                            {
                                PlayerMng.Data._GameScore += 5000;
                                _EscaperItemList[i].GetItem();
                                _EscaperItemList.RemoveAt(i);
                                _Escaper_ItemValue++;
                                break;
                            }
                        }
                    }
                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_R1"))
                    {
                        for (int i = 0; i < _Escaper_Window.Count; i++)
                        {
                            if (Vector3.Distance(transform.parent.localPosition, _Escaper_Window[i].transform.localPosition) < 70 && !_Escaper_Window[i]._Broken && !_DoingMyWork2)
                            {
                                _DoingMyWork2 = true;
                                StartCoroutine(BreakWindow_C(0.75f, i));
                                _MyCharacterFrameWork.SetAnimation("breakwindow", "stay", false);
                                _MyCharacterFrameWork._MyJob = "escaper";
                                float angle = Mathf.Atan2(transform.parent.localPosition.y - _Escaper_Window[i].transform.localPosition.y, transform.parent.localPosition.x - _Escaper_Window[i].transform.localPosition.x);
                                transform.localEulerAngles = new Vector3(0, 0, (angle * Mathf.Rad2Deg) + 90);
                                break;
                            }
                        }
                    }
                }



                if (!_DoingMyWork1 && !_DoingMyWork2)
                {
                    transform.parent.localPosition += _JoyStick_Move._NowPoint * _MoveSpeed;
                    _GunMouse.transform.localEulerAngles = new Vector3(0, 0, _JoyStick_Aim._JoyStickAngle);



                    if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L1"))
                        _MoveSpeed = _RunSpeed;
                    if (Input.GetButtonUp("Player" + _PlayerNumber.ToString() + "_L1"))
                        _MoveSpeed = _WalkSpeed;

                    if (_JoyStick_Move._NowPoint == Vector3.zero)
                    {
                        _MyCharacterFrameWork._NowState = "stay";
                        _MyCharacterFrameWork._MaxFrame = 6;
                        if (_MyCharacterFrameWork._MyJob == "guard")
                            _MyCharacterFrameWork._MaxFrame = 1;
                    }
                    else
                    {
                        if (_MyCharacterFrameWork._CanMove)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, _JoyStick_Move._JoyStickAngle - 90);

                        }
                        if (!Input.GetButton("Player" + _PlayerNumber.ToString() + "_L1"))
                        {
                            _MyCharacterFrameWork._NowState = "walk";
                            _MyCharacterFrameWork._MaxFrame = 8;
                        }
                        else
                        {
                            _MyCharacterFrameWork._NowState = "run";
                            _MyCharacterFrameWork._MaxFrame = 6;
                        }

                        if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_L1"))
                        {
                            _MoveSpeed = _RunSpeed;
                            
                            _MyCharacterFrameWork.SetAnimation("run", true);
                        }
                        if (Input.GetButtonUp("Player" + _PlayerNumber.ToString() + "_L1"))
                        {
                            _MyCharacterFrameWork.SetAnimation("walk", true);
                            _MoveSpeed = _WalkSpeed;
                        }
                    }
                }

                if (Input.GetButtonUp("Player" + _PlayerNumber.ToString() + "_squar") && !_DoingMyWork1 && !_DoingMyWork2)
                {
                    for (int i = 0; i < _Escaper_Window.Count; i++)
                    {
                        if (Vector3.Distance(transform.parent.localPosition, _Escaper_Window[i].transform.localPosition) < 100 && _PickerCharacter._PickerTresureItem.activeSelf)
                        {
                            if(_MyCharacterFrameWork._MyJob=="escaper")
                            {
                                _EscapingClear = true;
                                Escaping(i);
                                _MyRigidBody.isKinematic = true;
                            }
                            else if(_Escaper_Window[i]._Broken && _EscaperCharacter._Escaper_ItemValue > 0)
                            {
                                _EscapingClear = true;
                                _EscaperCharacter._Escaper_ItemValue--;
                                Escaping(i);
                                
                                _GunMouse.SetActive(false);
                            }
                            break;
                        }
                    }
                }
            }
            //if (_HP < 1)
            //    Destroy(transform.parent.gameObject);
        }

    }

    IEnumerator BreakWindow_C(float time,int num)
    {
        yield return new WaitForSeconds(time);

        _Escaper_Window[num].WindowBreak();
        _DoingMyWork2 = false;
        StartCoroutine(SetStay(0.875f));
    }

    public void PickingClear()
    {
        StartCoroutine(SetStay(5.2f));
    }
    IEnumerator SetStay(float time)
    {
        yield return new WaitForSeconds(time);

        _DoingMyWork1 = false;
        _DoingMyWork2 = false;
    }

    void Escaping(int windownumber)
    {
        int num=0;
        if (_MyCharacterFrameWork._MyJob == "hacker")
            num = 0;
        else if (_MyCharacterFrameWork._MyJob == "silencer")
            num = 1;
        else if (_MyCharacterFrameWork._MyJob == "picker")
            num = 2;
        else if (_MyCharacterFrameWork._MyJob == "escaper")
            num = 3;
        _Escaper_Window[windownumber].MakeAnimation_Char(num);
        gameObject.SetActive(false);
    }

    IEnumerator SetHackerMapAlpha(float time)
    {
        bool _minum = true;
        while(true)
        {
            yield return new WaitForSeconds(time);

            int num = -5;
            
            if(_HackerMap.alpha<=0)
            {
                
            }
            if (!_minum)
                num = 5;
            _HackerMap.alpha += num;
        }
    }

    IEnumerator ChangeHackerMiniMap(float time)
    {
        yield return new WaitForSeconds(time);

        _HackerMap.spriteName = "hacking_map_" + _HackingMap_Floor.ToString();
        //_HackerMap.alpha += 1;
        if(!(_CCTVList_1floor.Count==0&&_CCTVList_2floor.Count==0))
        {
            if (_HackingMap_Floor == 1)
            {
                _CCTVLockPoint_DesPos = _CCTVList_Icon_1floor[_CCTVListNumber].transform.localPosition;
                _CCTVListPanel_Floor1.SetActive(true);
                _CCTVListPanel_Floor2.SetActive(false);
            }
            else
            {
                _CCTVLockPoint_DesPos = _CCTVList_Icon_2floor[_CCTVListNumber].transform.localPosition;
                _CCTVListPanel_Floor1.SetActive(false);
                _CCTVListPanel_Floor2.SetActive(true);
            }
        }
        
    }
    IEnumerator SetChangingMapValue(float time)
    {
        yield return new WaitForSeconds(time);

        _MapFloorChanging = false;
        _HackingGameMng._CanOff = true;
    }

    public void SetPlayerNumber(int i)
    {
        _PlayerNumber = i;
        _JoyStick_Move._PlayerNumber = i;
        _JoyStick_Aim._PlayerNumber = i;
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "bullet")
        //{
        //    if(other.GetComponent<SleepBullet>()._PlayerNumber!=_PlayerNumber)
        //    {
        //        _HP-=1;
        //        other.GetComponent<SleepBullet>().Hit();
        //    }
        //}
        if(other.tag == "portal_1")
        {
            //Debug.Log("asdf");
            transform.parent.localPosition = PlayerMng.Data._PortalPos_SecondFloor.transform.localPosition;

            GameObject obj = NGUITools.AddChild(PlayerMng.Data._UIRoot, PlayerMng.Data._FadeEffect_Step);
            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            obj.transform.localPosition = pos;
        }
        if (other.tag == "portal_2")
        {
            //Debug.Log("asdf");
            transform.parent.localPosition = PlayerMng.Data._PortalPos_FirstFloor.transform.localPosition;
            GameObject obj = NGUITools.AddChild(PlayerMng.Data._UIRoot, PlayerMng.Data._FadeEffect_Step);
            Vector3 pos = new Vector3();
            if (_MyCamera.rect.x == 0)
                pos.x = -960;
            else
                pos.x = 0;
            if (_MyCamera.rect.y == 0)
                pos.y = 0;
            else
                pos.y = 540;
            obj.transform.localPosition = pos;
        }
        if(other.tag == "exit")
        {
            if(_EscaperCharacter._Escaper_ItemValue>=3 && _PickerCharacter._PickerTresureItem)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void Silencer_GetBullet()
    {
        GameObject effect = NGUITools.AddChild(transform.parent.gameObject, _Silencer_GetBullet);
        effect.transform.localPosition = new Vector3(0, 150, 0);

        _Silencer_BulletCount++;
    }

    public void HitGuard()
    {
        GameObject effect = NGUITools.AddChild(transform.parent.parent.gameObject, PlayerMng.Data._GuardHitEffect);
        effect.transform.localPosition = transform.parent.localPosition;

        if(_MyCharacterFrameWork._MyJob == "hacker")
        {
            GameObject red = NGUITools.AddChild(_HackerUI, PlayerMng.Data._GuardHitRed);
        }
        else if (_MyCharacterFrameWork._MyJob == "silencer")
        {
            GameObject red = NGUITools.AddChild(_SilencerUI, PlayerMng.Data._GuardHitRed);
        }
        else if (_MyCharacterFrameWork._MyJob == "picker")
        {
            GameObject red = NGUITools.AddChild(_PickerUI, PlayerMng.Data._GuardHitRed);
        }
        else if (_MyCharacterFrameWork._MyJob == "escaper")
        {
            GameObject red = NGUITools.AddChild(_EscaperUI, PlayerMng.Data._GuardHitRed);
        }
        

        //_HP -= dmg;
        if (!PlayerMng.Data._SUPERPOWER)
            _HP--;
        if (_HP <= 0)
        {
            _Dead = true;

            //if(_MyCharacterFrameWork._MyJob == "hacker")
            //{
            //    _HackingGameMng.OffGame();
            //    _CCTVListPanel.SetActive(false);
            //    _DoingMyWork1 = false;
            //    _HackingGameMng._CanOff = false;
            //    _CCTVLockPoint.SetActive(false);
            //    _CameraMng._HackerCamera_Hacking.SetActive(false);
            //    _CameraMng._HackerCamera_InHouse.SetActive(true);
            //    _MyRigidBody.isKinematic = false;
            //}
            //else if(_MyCharacterFrameWork._MyJob == "silencer")
            //{
            //    _Silencer_ZoomImage.SetActive(false);
            //    _GunMouseSprite.GetComponent<UI2DSprite>().width = 80;
            //    _GunMouseSprite.GetComponent<UI2DSprite>().height = 10;
            //    _GunMouseSprite.transform.localPosition = new Vector3(0, 0, 0);
            //    _MyCamera.transform.localPosition = new Vector3(0, 0, 0);
            //    _MyRigidBody.isKinematic = false;
            //}
            //else if(_MyCharacterFrameWork._MyJob == "picker")
            //{
            //
            //}
        }
    }

    IEnumerator SetDoingWork(float time, int num)
    {
        yield return new WaitForSeconds(time);

        if (num == 1)
            _DoingMyWork1 = false;
        else
            _DoingMyWork2 = false;
    }

    IEnumerator SetHacking(float time)
    {
        yield return new WaitForSeconds(time);

        _CameraMng._HackerCamera_Hacking.SetActive(true);
        _CameraMng._HackerCamera_InHouse.SetActive(false);

        _HackingGameMng.StartHacking(_HackingMap_Floor);
        _CCTVLockPoint.SetActive(true);
        if(_HackingMap_Floor==1)
        {
            _CCTVLockPoint.transform.localPosition = _CCTVList_Icon_1floor[0].transform.localPosition;
            _CCTVLockPoint_DesPos = _CCTVList_Icon_1floor[0].transform.localPosition;
        }
        else
        {
            _CCTVLockPoint.transform.localPosition = _CCTVList_Icon_2floor[0].transform.localPosition;
            _CCTVLockPoint_DesPos = _CCTVList_Icon_2floor[0].transform.localPosition;
        }
        _CCTVListNumber = 0;

        
        _Changing = false;
    }

    public void SetCCTVListOn()
    {
        _CCTVListPanel.SetActive(true);
        _CCTVListPanel_Floor1.SetActive(true);
    }

    IEnumerator SetMachine(float time, Vector3 pos, float angle)
    {
        yield return new WaitForSeconds(time);

        GameObject obj = NGUITools.AddChild(_UIRoot, _Silencer_Machine);
        obj.transform.localPosition = pos;
        obj.transform.localEulerAngles = new Vector3(0, 0, angle);
        _Silencer_SetMachine = obj;
    }

    void ShootSleepBullet()
    {
        if (_Silencer_BulletCount>0)
        {
            _Silencer_BulletCount--;
            if (_MyCharacterFrameWork._MyJob == "silencer" && _DoingMyWork1)
            {
                ShootSniping();
                _DoingMyWork2 = true;
                StartCoroutine(SetDoingWork(2.125f, 2));
                _ShootDelayMax = 2.125f;
            }
            else
            {
                GameObject bullet = NGUITools.AddChild(_UIRoot, _Bullet);
                bullet.transform.localEulerAngles = _GunMouse.transform.localEulerAngles;
                bullet.transform.localPosition = transform.parent.localPosition;
                bullet.GetComponent<SleepBullet>().Init(_PlayerNumber, 7, 1);
                _ShootDelayMax = 0.33f;
            }
        }
        
    }
    IEnumerator SetActiveZoomImage(float time)
    {
        yield return new WaitForSeconds(time);

        _Silencer_ZoomImage.SetActive(true);
    }
    void ShootSniping()
    {
        GameObject obj = NGUITools.AddChild(_Silencer_ZoomImage.transform.parent.gameObject, _Silencer_ZoomShoot);
        obj.transform.localPosition = _Silencer_ZoomImage.transform.localPosition;
        StartCoroutine(SetActiveZoomImage(0.4166667f));
        _Silencer_ZoomImage.SetActive(false);


        _MyCharacterFrameWork.SetAnimation("shoot", "reload", false);

        float angle = transform.localEulerAngles.z+90;


        Vector3 pluspos = new Vector3(Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad) * 0.03f, Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad) * 0.03f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + pluspos + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 0.11f, Mathf.Sin(angle * Mathf.Deg2Rad) * 0.11f),
            transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 4000.0f, Mathf.Sin(angle * Mathf.Deg2Rad) * 4000.0f, 0), 9000, WallLayer);

        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);

            if (hit.collider.tag == "wall")
                Hit(hit.point);
            else if (hit.collider.tag == "guard")
            {
                    hit.collider.GetComponent<SequrityGuard>().HitBullet();
                    GuardHit(hit.collider.transform.localPosition);
            }
        }
    }
    public void Hit(Vector3 pos)
    {
        GameObject effect = NGUITools.AddChild(_UIRoot, _BulletEffect);
        effect.transform.localEulerAngles = _GunMouse.transform.localEulerAngles+new Vector3(0,0,0);
        //effect.transform.localPosition = transform.localPosition;
        effect.transform.position = pos;
    }
    void GuardHit(Vector3 pos)
    {
        GameObject effect = NGUITools.AddChild(_UIRoot, _BulletEffect);
        effect.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, 0);
        //effect.transform.localPosition = transform.localPosition;
        effect.transform.localPosition = pos;
    }
}
