using UnityEngine;
using System.Collections;

public class SelectingPlayer : MonoBehaviour {

    public int _PlayerNumber;
    public bool _Selected;
    public int _NowSelectCameraTable;
    public int _Lanked;

    Vector3 _TargetPos;

    public GameObject _EffectParent;
    void Start()
    {
        //_NowSelectCameraTable = 1;
        _TargetPos = transform.localPosition;
    }
    void Update()
    {
        if(_PlayerNumber!=0 && _Selected==false)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _TargetPos,  270);

            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X")>0)//오른쪽
            {
                if (_NowSelectCameraTable == 1)
                {
                    _TargetPos = new Vector3(490, 283, 0);
                    _NowSelectCameraTable = 2;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        Debug.Log("??");
                        for (int i = _NowSelectCameraTable; i < _NowSelectCameraTable + 4; i++)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
                else if (_NowSelectCameraTable == 3)
                {
                    _TargetPos = new Vector3(490, -283, 0);
                    _NowSelectCameraTable = 4;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i < _NowSelectCameraTable + 4; i++)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") < 0)//왼쪽
            {
                if (_NowSelectCameraTable == 2)
                {
                    _TargetPos = new Vector3(-490, 283, 0);
                    _NowSelectCameraTable = 1;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i > _NowSelectCameraTable - 4; i--)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
                else if (_NowSelectCameraTable == 4)
                {
                    _TargetPos = new Vector3(-490, -283, 0);
                    _NowSelectCameraTable = 3;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i > _NowSelectCameraTable - 4; i--)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_Y") > 0)//위
            {
                if (_NowSelectCameraTable == 3)
                {
                    _TargetPos = new Vector3(-490, 283, 0);
                    _NowSelectCameraTable = 1;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i > _NowSelectCameraTable - 4; i--)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
                else if (_NowSelectCameraTable == 4)
                {
                    _TargetPos = new Vector3(490, 283, 0);
                    _NowSelectCameraTable = 2;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i > _NowSelectCameraTable - 4; i--)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_Y") < 0)//아래
            {
                if (_NowSelectCameraTable == 1)
                {
                    _TargetPos = new Vector3(-490, -283, 0);
                    _NowSelectCameraTable = 3;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i < _NowSelectCameraTable + 4; i++)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
                else if (_NowSelectCameraTable == 2)
                {
                    _TargetPos = new Vector3(490, -283, 0);
                    _NowSelectCameraTable = 4;

                    if (PlayerMng.Data._NowLockedCamera_Array[_NowSelectCameraTable - 1] != 0)
                    {
                        for (int i = _NowSelectCameraTable; i < _NowSelectCameraTable + 4; i++)//왼쪽이면 - 오른쪽이면 + 위면 - 아래면 +
                        {
                            int num = i;
                            if (num > 3)
                                num -= 4;
                            else if (num < 0)
                                num += 4;
                            if (PlayerMng.Data._NowLockedCamera_Array[num] == 0)
                            {
                                if (num == 0)
                                    _TargetPos = new Vector3(-490, 283, 0);
                                else if (num == 1)
                                    _TargetPos = new Vector3(490, 283, 0);
                                else if (num == 2)
                                    _TargetPos = new Vector3(-490, -283, 0);
                                else if (num == 3)
                                    _TargetPos = new Vector3(490, -283, 0);
                                _NowSelectCameraTable = num + 1;
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_O"))
            {
                bool check = false;
                for (int i = 0; i < PlayerMng.Data._NowLockedCamera.Count; i++)
                {
                    if (PlayerMng.Data._NowLockedCamera_Array[i] == _NowSelectCameraTable)
                        check = true;
                }
                if(!check)
                {
                    _Selected = true;
                    PlayerMng.Data._NowLockedCamera_Array[_Lanked - 1] = _NowSelectCameraTable;
                    
                    PlayerMng.Data._JobSelector[_NowSelectCameraTable - 1].GetComponent<SelectingJob>()._PlayerNumber = _PlayerNumber;
                    PlayerMng.Data._CameraTable[_NowSelectCameraTable - 1].SetActive(true);

                    StartCoroutine(SelectStep1(0.4f));
                }
            }
        }
    }

    IEnumerator SelectStep1(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject effect = NGUITools.AddChild(_EffectParent, PlayerMng.Data._CameraSelectEffect_1);
        effect.transform.localPosition = _TargetPos;
        StartCoroutine(SelectStep2(1.2f));
    }
    IEnumerator SelectStep2(float time)
    {
        yield return new WaitForSeconds(time);


        PlayerMng.Data._NowLockedCamera.Add(1);
    }
}
