using UnityEngine;
using System.Collections;

public class SelectingJob : MonoBehaviour {

    public int _PlayerNumber;
    /// <summary>
    /// 1 - Hacker
    /// 2 - Silencer
    /// 3 - Picker
    /// 4 - Escaper
    /// </summary>
    public int _NowJobNumber;
    public UISprite _Icon;
    public bool _Selected;

    public int _Lanked;

    bool _CenterAxis_X;

    public GameObject[] _SlotEffect = new GameObject[4];

    public GameObject _HackerEffect;
    public GameObject _SilencerEffect;
    public GameObject _PickerEffect;
    public GameObject _EscaperEffect;
    public GameObject _FlashEffect;

    void Start()
    {
        _NowJobNumber = 1;
        _CenterAxis_X = true;
    }

    void Update()
    {
        if(_PlayerNumber!=0 && !_Selected)
        {
            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") == 0)
                _CenterAxis_X = true;
            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") > 0 && _CenterAxis_X)//오른쪽
            {
                _NowJobNumber++;
                if (_NowJobNumber == 5)
                    _NowJobNumber = 1;
                _CenterAxis_X = false;
            }
            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_Cross_X") < 0 && _CenterAxis_X)//왼쪽
            {
                _NowJobNumber--;
                if (_NowJobNumber == 0)
                    _NowJobNumber = 4;
                _CenterAxis_X = false;
            }
            

            if (Input.GetButtonDown("Player" + _PlayerNumber.ToString() + "_O"))
            {
                bool check = false;
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerMng.Data._NowLockedJob_Array[i] == _NowJobNumber)
                        check = true;
                }
                if (!check)
                {
                    _Selected = true;
                    PlayerMng.Data._NowLockedJob.Add(1);
                    PlayerMng.Data._NowLockedJob_Array[_Lanked - 1] = _NowJobNumber;
                    PlayerMng.Data._NowLockedPlayerNumber_Array[_Lanked - 1] = _PlayerNumber;

                    GameObject effect = NGUITools.AddChild(transform.parent.gameObject, _SlotEffect[_NowJobNumber - 1]);
                    if (_Lanked == 1)
                        effect.transform.localPosition = new Vector3(-480, 270, 0);
                    else if (_Lanked == 2)
                        effect.transform.localPosition = new Vector3(480, 270, 0);
                    else if (_Lanked == 3)
                        effect.transform.localPosition = new Vector3(-480, -270, 0);
                    else if (_Lanked == 4)
                        effect.transform.localPosition = new Vector3(480, -270, 0);

                    StartCoroutine(Effect_Flash(0.35f));
                }
            }
        }
        if (_NowJobNumber == 1)
            _Icon.spriteName = "hackericon";
        else if(_NowJobNumber == 2)
            _Icon.spriteName = "silencericon";
        else if (_NowJobNumber == 3)
            _Icon.spriteName = "pickericon";
        else if (_NowJobNumber == 4)
            _Icon.spriteName = "escapericon";
    }

    IEnumerator Effect_Flash(float time)
    {
        yield return new WaitForSeconds(time);

        _Icon.enabled = false;
        GameObject effect = NGUITools.AddChild(transform.parent.gameObject, _FlashEffect);
        //effect.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        GameObject icon = new GameObject();
        if (_NowJobNumber == 1)
            icon = _HackerEffect;
        else if (_NowJobNumber == 2)
            icon = _SilencerEffect;
        else if (_NowJobNumber == 3)
            icon = _PickerEffect;
        else if (_NowJobNumber == 4)
            icon = _EscaperEffect;
        GameObject effect2 = NGUITools.AddChild(transform.parent.gameObject, icon);

        if (_Lanked == 1)
        {
            effect.transform.localPosition = new Vector3(-480, 270, 0);
            effect2.transform.localPosition = new Vector3(-480, 270, 0);
        }
        else if (_Lanked == 2)
        {
            effect.transform.localPosition = new Vector3(480, 270, 0);
            effect2.transform.localPosition = new Vector3(480, 270, 0);
        }
        else if (_Lanked == 3)
        {
            effect.transform.localPosition = new Vector3(-480, -270, 0);
            effect2.transform.localPosition = new Vector3(-480, -270, 0);
        }
        else if (_Lanked == 4)
        {
            effect.transform.localPosition = new Vector3(480, -270, 0);
            effect2.transform.localPosition = new Vector3(480, -270, 0);
        }
        effect2.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }
}
