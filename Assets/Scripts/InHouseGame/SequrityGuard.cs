using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequrityGuard : MonoBehaviour {

    public bool _OneUse;
    bool _SearchingStart;

    public GameObject _ViewObject;
    FieldOfView _ViewMng;

    public bool _HaveSleepBullet;
    public GameObject _SleepBulletHaveIcon;

    public GameObject _Circle;

    public List<Vector3> _RouteList = new List<Vector3>();
    Vector3 _NowDestinationPos;
     [HideInInspector]
    public InHouse_Character _MySprite;
    
    public float _CorrectionDistance;
    public float _MoveSpeed;
    bool _AlphaDown;

    float _MissingTime_One;

    bool _Forward;
    int _NowListPosNumber;
    int _MaxListPosNumber;

    public int _LoopStartPosNumber;
    

    public bool _FindTheif;
    bool _Turning;
    bool _Miss;

    bool _Hitting;

    
    bool _Sleep;

    [HideInInspector]
    public Vector3 _DestinationAngle;
    public float _PlusAngle;

    public void CCTVCalled()
    {
        _FindTheif = true;

    }

    void Start()
    {
        _MoveSpeed = 150;

        _Forward = true;
        _NowListPosNumber = _LoopStartPosNumber;
        _MaxListPosNumber = _RouteList.Count;
        if(_NowListPosNumber>=_MaxListPosNumber)
        {
            _Forward = false;
        }
        if (_RouteList.Count != 0)
            _NowDestinationPos = _RouteList[_LoopStartPosNumber];
        _ViewMng = _ViewObject.GetComponent<FieldOfView>();
        _MySprite.SetAnimation("walk", true);
    }
    public void Init()
    {
        _ViewMng = _ViewObject.GetComponent<FieldOfView>();
        _OneUse = true;
        _MissingTime_One = 1.5f;
        StartCoroutine(SetSearch(0.1f));
    }

    IEnumerator SetSearch(float time)
    {
        yield return new WaitForSeconds(time);

        _SearchingStart = true;
    }

    public void HeardOfPickingSound()
    {
        if(PlayerMng.Data._SafeGuardCallTime<=0.0f)
        {
            PlayerMng.Data._SafeGuardCallTime = PlayerMng.Data._SafeGuardCallTime_Delay;

            StartCoroutine(MakeGuard_Safe(2.0f));
            GameObject obj = NGUITools.AddChild(transform.parent.gameObject, PlayerMng.Data._GuardCreate_Circle);
            obj.transform.localPosition = PlayerMng.Data._PickerHoldPosition;
        }
        
    }

    IEnumerator MakeGuard_Safe(float time)
    {
        yield return new WaitForSeconds(time);
        //float angle = (transform.localEulerAngles.z)*Mathf.Deg2Rad;
        //Vector3 plus = new Vector3(Mathf.Cos(angle)*_MakeGuardPos.transform.localPosition.x*2, Mathf.Sin(angle)*_MakeGuardPos.transform.localPosition.y*2,0);
        GameObject obj = NGUITools.AddChild(transform.parent.gameObject, PlayerMng.Data._GuardCreate_Dust);
        obj.transform.localPosition = PlayerMng.Data._PickerHoldPosition;

        GameObject guard = NGUITools.AddChild(PlayerMng.Data._UIRoot.transform.parent.gameObject, PlayerMng.Data._SequrityGuard);
        Vector3 pos = PlayerMng.Data._PickerHoldPosition;
        float angle1 = Mathf.Atan2(PlayerMng.Data._PlayerArray[2].transform.parent.localPosition.y - pos.y, PlayerMng.Data._PlayerArray[2].transform.parent.localPosition.x - pos.x);
        angle1 *= Mathf.Rad2Deg;
        guard.transform.localEulerAngles = new Vector3(0, 0, angle1 - 90);
        guard.transform.localPosition = PlayerMng.Data._PickerHoldPosition;

        guard.GetComponent<SequrityGuard>().CCTVCalled();
        guard.GetComponent<SequrityGuard>().Init();

        //guard.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, 90);
    }

    void Hitting(Player_InHouse target)
    {
        _Hitting = true;
        _MySprite.SetAnimation("hit", "run", false);
        StartCoroutine(StopHitting(0.5f));
        StartCoroutine(HitCharacter(0.25f, target));
    }

    IEnumerator HitCharacter(float time,Player_InHouse target)
    {
        yield return new WaitForSeconds(time);

        target.HitGuard();
    }

    IEnumerator StopHitting(float time)
    {
        yield return new WaitForSeconds(time);

        _Hitting = false;
        _MySprite.SetAnimation("run", true);
    }

    void Update()
    {
        if(!_OneUse)
        {
            if (_HaveSleepBullet)
            {
                _SleepBulletHaveIcon.transform.localPosition = transform.localPosition + new Vector3(0, 100, 0);
            }
            else
                _SleepBulletHaveIcon.SetActive(false);
        }
        
        if (_AlphaDown)
        {
            _MySprite._MySprite.alpha -= Time.smoothDeltaTime / 2;

            _Circle.SetActive(false);
            if (_MySprite._MySprite.alpha <= 0)
            {
                if (_OneUse)
                    Destroy(gameObject);
                else
                {
                    transform.localPosition = _RouteList[0];
                    _NowListPosNumber = 1;
                    _NowDestinationPos = _RouteList[1];
                    _MoveSpeed = 150;
                }
                _ViewObject.SetActive(true);
                _AlphaDown = false;
                _MySprite._MySprite.alpha = 1;
            }
        }
        else if (_Sleep)
        {
            _Circle.SetActive(false);
        }
        else
        {
            _ViewObject.SetActive(true);
            _Circle.SetActive(true);
            //_ViewMng._VisibleTargets.Clear();
            for (int i = 0; i < 4; i++)
            {
                if (Vector3.Distance(transform.localPosition, PlayerMng.Data._PlayerArray[i].transform.parent.localPosition) < 150)
                {
                    bool check = false;
                    for (int j = 0; j < _ViewMng._VisibleTargets.Count; j++)
                    {
                        if (_ViewMng._VisibleTargets[j]._PlayerNumber == PlayerMng.Data._PlayerArray[i]._PlayerNumber)
                            check = true;
                    }
                    if (!check && !PlayerMng.Data._PlayerArray[i]._Escaper_Camouflage)
                        _ViewMng._VisibleTargets.Add(PlayerMng.Data._PlayerArray[i]);
                }
            }


            if (_ViewMng._VisibleTargets.Count != 0)
            {
                if (!_FindTheif)
                {
                    GameObject obj = NGUITools.AddChild(transform.parent.gameObject, PlayerMng.Data._CCTVDangerEffect);
                    obj.transform.localPosition = transform.localPosition;
                    _FindTheif = true;
                    _MoveSpeed = 225;
                    _MySprite.SetAnimation("run", true);
                }
                for (int i = 0; i < _ViewMng._VisibleTargets.Count; i++)
                {
                    if (Vector3.Distance(transform.localPosition, _ViewMng._VisibleTargets[i].transform.parent.localPosition) < 140 && !_Hitting)
                    {
                        Hitting(_ViewMng._VisibleTargets[i]);
                
                    }
                }
            }
            else
            {
                if (_OneUse)
                {
                    _MissingTime_One -= Time.smoothDeltaTime;
                    if (_MissingTime_One <= 0.0f)
                        StartCoroutine(SetMissOne(0.0f));
                }


                if (_FindTheif)
                {
                    if (_OneUse && _SearchingStart)
                    {
                        StartCoroutine(SetMissOne(0.0f));
                    }
                    else if (!_OneUse)
                    {
                        Vector3 pos = _RouteList[_NowListPosNumber];
                        int listnum = _NowListPosNumber;
                        //for (int i = 0; i < _RouteList.Count; i++)
                        //{
                        //    if (Vector3.Distance(transform.localPosition, pos) > Vector3.Distance(transform.localPosition, _RouteList[i]))
                        //    {
                        //        pos = _RouteList[i];
                        //        listnum = i;
                        //    }
                        //}

                        //_NowListPosNumber = listnum;
                        //_NowDestinationPos = _RouteList[_NowListPosNumber];

                        StartCoroutine(SetMiss(1.0f));

                        _MoveSpeed = 0;
                        _Miss = true;
                        _MySprite.SetAnimation("walk", true);
                    }



                }
                else
                {
                    _MoveSpeed = 150;
                }
                _FindTheif = false;


            }

            if (_FindTheif)
            {
                Player_InHouse nowlockplayer = _ViewMng._VisibleTargets[0];
                for (int i = 1; i < _ViewMng._VisibleTargets.Count; i++)
                {
                    if (Vector3.Distance(transform.localPosition, _ViewMng._VisibleTargets[i].transform.localPosition) < Vector3.Distance(transform.localPosition, nowlockplayer.transform.localPosition))
                        nowlockplayer = _ViewMng._VisibleTargets[i];
                }
                _NowDestinationPos = nowlockplayer.transform.parent.localPosition;

                if (!_Hitting)
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, _NowDestinationPos, Time.smoothDeltaTime * _MoveSpeed);
                float angle = Mathf.Atan2(transform.localPosition.y - _NowDestinationPos.y, transform.localPosition.x - _NowDestinationPos.x) * Mathf.Rad2Deg;
                transform.localEulerAngles = new Vector3(0, 0, angle + 90);


            }
            else if (!_Miss)
            {
                if (!_OneUse)
                {
                    if (!_Turning)
                    {
                        if (!_Hitting)
                            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _NowDestinationPos, Time.smoothDeltaTime * _MoveSpeed);


                        if (Vector3.Distance(transform.localPosition, _NowDestinationPos) < _CorrectionDistance)
                        {
                            transform.localPosition = _NowDestinationPos;

                            ChangeTargetPos();

                            float turnangle = Mathf.Atan2(transform.localPosition.y - _NowDestinationPos.y, transform.localPosition.x - _NowDestinationPos.x) * Mathf.Rad2Deg;
                            _DestinationAngle = new Vector3(0, 0, turnangle + 90);

                            float oneangle = transform.localEulerAngles.z + 90;
                            if (oneangle < 0)
                                oneangle = oneangle + 360;
                            float twoangle = turnangle;
                            if (twoangle < 0)
                                twoangle = twoangle + 360;


                            if (oneangle >= 360)
                                oneangle = 0;

                            _PlusAngle = oneangle - twoangle;

                            if (Mathf.Abs(_PlusAngle) > 180.0f)
                            {
                                if (_PlusAngle > 0)
                                    _PlusAngle -= 360;
                                else
                                    _PlusAngle += 360;
                            }
                            if (Mathf.Abs(_PlusAngle) < 7)
                                _PlusAngle = -180;

                            //Debug.Log(oneangle.ToString() + "    " + twoangle.ToString());

                            _Turning = true;
                            //StartCoroutine(SetPatroling(2.0f));
                        }

                        float angle = Mathf.Atan2(transform.localPosition.y - _NowDestinationPos.y, transform.localPosition.x - _NowDestinationPos.x) * Mathf.Rad2Deg;

                        if (!_Turning)
                            transform.localEulerAngles = new Vector3(0, 0, angle + 90);
                    }
                    else
                    {
                        transform.localEulerAngles += new Vector3(0, 0, _PlusAngle * Time.smoothDeltaTime);
                        float myangle = transform.localEulerAngles.z;
                        float desangle = _DestinationAngle.z;
                        if (myangle < 0)
                            myangle += 360;
                        if (desangle < 0)
                            desangle += 360;
                        if (Mathf.Abs(desangle - myangle) < 8.0f)
                        {
                            transform.localEulerAngles = _DestinationAngle;
                            _Turning = false;
                        }
                    }
                }

            }
            _ViewObject.transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.z, 0);
        }
    }

    void ChangeTargetPos()
    {
        if(!_OneUse)
        {
            if (_Forward)
            {
                _NowListPosNumber++;
                if (_NowListPosNumber >= _MaxListPosNumber)
                {
                    _NowListPosNumber -= 2;
                    _Forward = false;
                }
            }
            else
            {
                _NowListPosNumber--;
                if (_NowListPosNumber < _LoopStartPosNumber)
                {
                    _NowListPosNumber += 2;
                    _Forward = true;
                }
            }
            _NowDestinationPos = _RouteList[_NowListPosNumber];
        }
        
    }

    IEnumerator SetPatroling(float time)
    {
        yield return new WaitForSeconds(time);

        _Turning = false;
    }

    IEnumerator SetMiss(float time)
    {
        yield return new WaitForSeconds(time);

        _Miss = false;
        _AlphaDown = true;
        _ViewObject.SetActive(false);
        _ViewMng._Find = false;
    }

    IEnumerator SetMissOne(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("destroy");
        _ViewObject.SetActive(false);
        _Sleep = true;
        _AlphaDown = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            HitBullet();
            other.GetComponent<SleepBullet>().Hit();
        }
    }

    public void HitBullet()
    {
        _ViewObject.SetActive(false);
        _Sleep = true;
        _MySprite.SetAnimation("sleep", "sleeping", false);

        if(_HaveSleepBullet)
        {
            GameObject obj = NGUITools.AddChild(PlayerMng.Data._PlayerArray[1]._SilencerUI, PlayerMng.Data._GetSleepBulletEffect);
            obj.transform.localPosition = new Vector3(119,-389,0);
            PlayerMng.Data._PlayerArray[1]._Silencer_BulletCount++;
            _HaveSleepBullet = false;
        }
        PlayerMng.Data._GameScore += 3000;
        GetComponent<CircleCollider2D>().enabled = false;
        
    }
}
