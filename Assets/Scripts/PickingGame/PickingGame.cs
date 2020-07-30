using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PickingGame : MonoBehaviour {

    public int _Life = 3;

    bool _Failed;

    public PickingGame_JoyStick _JoyStick;
    public Player_InHouse _PickerCharacter;

    public GameObject _BlockGrid;
    public GameObject _BlockObject;

    public GameObject _PickingCamera;
    public GameObject _UnlockEffect;
    public GameObject _FailEffect;

    List<GameObject> _XList = new List<GameObject>();

    public GameObject _PickingSpark;

    public GameObject[] _Picker_Life = new GameObject[3];
    public GameObject _Life_X;

    public int _PlayerNumber;

    int _Point;
    public UILabel _PointLabal;

    public UI2DSprite _PickingGaze;

    public Picker_Safe _SafeObject;

    public bool _UnLocked;

    public List<PickingGame_Block> _BlockList = new List<PickingGame_Block>();

    void Start()
    {
        for(int i=0;i<2;i++)
        {
            CreateBlock();
        }
    }

    void Update()
    {
        if(_Failed)
        {

        }
        else
        {
            _PointLabal.text = _Point.ToString();
            _PickingGaze.fillAmount = (float)(_Point / 20000.0f);

            if (!_UnLocked)
            {
                float _JoyStickAngle = _JoyStick._JoyStickAngle;
                if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_O"))
                {
                    CrashBlock(1, _JoyStickAngle);
                }
                else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_X"))
                {
                    CrashBlock(2, _JoyStickAngle);
                }
                else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_squar"))
                {
                    CrashBlock(3, _JoyStickAngle);
                }
                else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_triangle"))
                {
                    CrashBlock(4, _JoyStickAngle);
                }
            }


            if (_Point >= 20000.0f)
            {
                UnLocked();
                _Point -= 20000;
            }
        }
        
    }

    void CrashBlock(int num,float angle)
    {

        if (num == _BlockList[0]._BlockNum && Mathf.Abs(angle - _BlockList[0]._BlockAngle)<=30.0f)
            HitBlock();
        else
            WrongBlock();
        //Destroy(_BlockList[0].gameObject);
        _BlockList[0]._Same = num == _BlockList[0]._BlockNum && Mathf.Abs(angle - _BlockList[0]._BlockAngle) <= 30.0f;
        SparkEffect(_BlockList[0].transform.localEulerAngles.z*Mathf.Deg2Rad);
        _BlockList[0].CrashBlock();
        _BlockList.RemoveAt(0);
        CreateBlock();


    }

    void SparkEffect(float angle)
    {
        GameObject effect = NGUITools.AddChild(_BlockGrid.transform.parent.gameObject, _PickingSpark);
        Vector3 pos = new Vector3(Mathf.Cos(angle) * 300, Mathf.Sin(angle) * 300, 0);
        effect.transform.localPosition = pos;
        effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void CreateBlock()
    {
        float angle = Random.Range(-149.0f, 149.0f);
        int num = Random.Range(1, 5);

        GameObject obj = NGUITools.AddChild(_BlockGrid, _BlockObject);
        obj.GetComponent<PickingGame_Block>().Init(angle, num);
        obj.transform.localScale = new Vector3(1 - 0.25f * (1 - _BlockList.Count), 1 - 0.25f * (1 - _BlockList.Count), 1);
        for (int i = 0; i < _BlockList.Count;i++ )
        {
            _BlockList[i].SetSpriteDepthPlus();
            _BlockList[i].Smalling();
        }
            _BlockList.Add(obj.GetComponent<PickingGame_Block>());
    }

    void UnLocked()
    {
        _UnLocked = true;
        _SafeObject._MakeNoise = false;
        PlayerMng.Data._GameScore += 10000;
        GameObject obj = NGUITools.AddChild(_PickingCamera, _UnlockEffect);
        StartCoroutine(UnLocked_C(3.5f));
    }

    IEnumerator UnLocked_C(float time)
    {
        yield return new WaitForSeconds(time);

        _PickerCharacter.PickingClear();
        _PickerCharacter._CameraMng._PickerCamera_Picking.SetActive(false);
        _PickerCharacter._CameraMng._PickerCamera_InHouse.SetActive(true);
        _PickerCharacter._MyRigidBody.isKinematic = false;
        _PickerCharacter._PickerTresureItem.SetActive(true);
        _PickerCharacter._MyCharacterFrameWork.SetAnimation("unlock", "stay", false);
        _SafeObject._Mouse.SetActive(false);
    }

    void HitBlock()
    {
        _Point += 250;
        PlayerMng.Data._GameScore += 200;
    }
    void WrongBlock()
    {
        PlayerMng.Data._GameScore -= 500;
        if(_Life>0)
        {
            _Point -= 500;
            _Life--;
            
            GameObject obj = NGUITools.AddChild(_Picker_Life[_Life], _Life_X);
            _XList.Add(obj);
            if(_Life==0)
            {
                GameObject obj1 = NGUITools.AddChild(_PickingCamera, _FailEffect);

                StartCoroutine(MissionFail(1.5f,obj1));
                _Failed = true;
                for(int i=0;i<_XList.Count;i++)
                {
                    Destroy(_XList[i]);
                }
                _XList.Clear();
            }
        }
    }

    IEnumerator MissionFail(float time,GameObject obj)
    {
        yield return new WaitForSeconds(time);
        PlayerMng.Data._GameScore -= 5000;
        Destroy(obj);
        _Life = 3;
        _Point = 0;
        _Failed = false;
        _PickerCharacter._CameraMng._PickerCamera_Picking.SetActive(false);
        _PickerCharacter._CameraMng._PickerCamera_InHouse.SetActive(true);
        _PickerCharacter._MyRigidBody.isKinematic = false;
        _PickerCharacter._MyCharacterFrameWork.SetAnimation("stay", true);
        _PickerCharacter._DoingMyWork1 = false;
    }
}
