using UnityEngine;
using System.Collections;

public class MainSceneMng : MonoBehaviour {

    public SceneChangeMng _SceneMng;

    public GameObject _OptionPanel;
    public UISprite _SoundCheck;

    public UI2DSprite _OptionBg_UI;
    public GameObject _OptionBg;
    public GameObject _OptionBg_Off;
    GameObject _OptionBg_temp;

    public GameObject _LockPoint;
    int _LockNumber;
    int _MinLockList;
    int _MaxLockList;
    public GameObject[] _TargetPosition;

    bool _OnAlpha;
    public AudioClip _Audio;

    bool[] _CenterAxis_X = new bool[11];

	void Start()
    {
        StaticDataMng._SoundOn = true;
        StartCoroutine(OnAlpha_Loop());
        _MinLockList = 0;
        _MaxLockList = 3;
    }

    public void OnOptionPanel()
    {
        _OptionPanel.SetActive(true);
        GameObject obj = NGUITools.AddChild(_OptionPanel, _OptionBg);
        obj.transform.localPosition = new Vector3(350, 135, 0);
        _OptionBg_temp = obj;
        StartCoroutine(OnAlpha(0.7f));
        _MinLockList = 3;
        _MaxLockList = 5;
        _LockNumber = 3;
    }

    IEnumerator OnAlpha(float time)
    {
        yield return new WaitForSeconds(time);

        _OnAlpha = true;
        _OptionBg_UI.GetComponent<Animator>().SetTrigger("onalpha");
    }

    IEnumerator OnAlpha_Loop()
    {
        

        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            if (_OnAlpha)
                _OptionBg_UI.alpha += 0.0000001f;
        }
    }

    void Update()
    {
        _LockPoint.transform.localPosition = Vector3.MoveTowards(_LockPoint.transform.localPosition, _TargetPosition[_LockNumber].transform.localPosition, 180);
        for (int i = 1; i < 12;i++ )
        {
            if (Input.GetAxis("Player" + i.ToString() + "_Cross_X") == 0)
                _CenterAxis_X[i - 1] = true;
            if (Input.GetAxis("Player" + i.ToString() + "_Cross_X") > 0 && _CenterAxis_X[i - 1])//오른쪽
            {
                _LockNumber++;
                if (_LockNumber >= _MaxLockList)
                    _LockNumber = _MinLockList;
                _CenterAxis_X[i - 1] = false;
                AudioSource.PlayClipAtPoint(_Audio, Vector2.zero, 1);
            }
            else if (Input.GetAxis("Player" + i.ToString() + "_Cross_X") < 0 && _CenterAxis_X[i - 1])//왼쪽
            {
                _LockNumber--;
                if (_LockNumber < _MinLockList)
                    _LockNumber = _MaxLockList-1;
                AudioSource.PlayClipAtPoint(_Audio, Vector2.zero, 1);
                _CenterAxis_X[i - 1] = false;
            }
            if (Input.GetButtonDown("Player" + i.ToString() + "_O"))
            {
                if (_LockNumber == 0)
                {
                    _SceneMng.SceneChange(2);
                }
                else if (_LockNumber == 1)
                {
                    OnOptionPanel();
                }
                else if (_LockNumber == 2)
                {
                    _SceneMng.Exit();
                }
                else if (_LockNumber == 3)
                {
                    CheckSoundBlock();
                }
                else if (_LockNumber == 4) 
                {
                    OffOptionPanel();
                }
            }
        }
            
    }

    public void OffOptionPanel()
    {
        _MaxLockList = 3;
        _MinLockList = 0;
        _LockNumber = 0;
        _OptionBg_UI.alpha = 0;
        _OptionPanel.SetActive(false);
        Destroy(_OptionBg_temp);
        _OptionBg_temp = null;
        _OnAlpha = false;

        GameObject obj = NGUITools.AddChild(_OptionPanel.transform.parent.gameObject, _OptionBg_Off);
        obj.transform.localPosition = new Vector3(350, 135, 0);
    }

    public void CheckSoundBlock()
    {
        if(StaticDataMng._SoundOn==true)
        {
            _SoundCheck.spriteName = "sound_on";
            StaticDataMng._SoundOn = false;
        }
        else
        {
            _SoundCheck.spriteName = "sound_off";
            StaticDataMng._SoundOn = true;
        }
    }
}
