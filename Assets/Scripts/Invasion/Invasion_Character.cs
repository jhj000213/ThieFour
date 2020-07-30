using UnityEngine;
using System.Collections;

public class Invasion_Character : MonoBehaviour {

    public string _MyColor;

    UISprite _MySprite;
    string _NowState;
    int _MaxFrame;
    int _NowFrame;
    float _Framelate = 0.125f;
    float _NowFrameTime;

    float _MoveSpeed;

    Vector3 _NowPos;

    Vector3 _LadderPos;
    bool _Clear;
    bool _Laddering;

    void Start()
    {
        _NowPos = transform.localPosition;
        _MySprite = GetComponent<UISprite>();
        _NowState = "run";
        _MaxFrame = 8;
        _Framelate = 0.125f;

        _LadderPos = new Vector3(0,-360,0);
    }
    void Update()
    {
        
        _NowFrameTime += Time.smoothDeltaTime;
        _MySprite.MakePixelPerfect();

        if(_NowFrameTime>=_Framelate)
        {
            _NowFrameTime -= _Framelate;
            _NowFrame++;
            if (_NowFrame >= _MaxFrame)
            {
                _NowFrame = 0;
                if (_NowState == "ladding")
                {
                    _NowFrame++;
                    transform.localPosition += new Vector3(0,143,0);
                }
                if (_NowState == "miss")
                {
                    _NowState = "run";
                    _MaxFrame = 8;
                    _Framelate = 0.125f;
                }
            }
            _MySprite.spriteName = "character_" + _MyColor + "_" + _NowState + "_" + _NowFrame;
        }
        if(!_Clear)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _NowPos, Time.smoothDeltaTime * 90);
        }
        else
        {
            if (!_Laddering)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _LadderPos, Time.smoothDeltaTime * _MoveSpeed);
                if (Mathf.Abs(transform.localPosition.x - _LadderPos.x) < 5.0f)
                {
                    _Laddering = true;
                    _NowState = "ladding";
                    _NowFrame = 0;
                    _MaxFrame = 9;
                    _Framelate = 0.125f;
                }
            }
            else
            {

            }
        }
    }

    public void MissNode()
    {
        _NowState = "miss";
        _NowFrame = 0;
        _MaxFrame = 2;
        _Framelate = 0.25f;
        _NowPos += new Vector3(-90,0,0);
    }

    public void StartUpRadder()
    {
        _Clear = true;
        _MoveSpeed = transform.localPosition.x;
        if (_MoveSpeed >= -90.0f)
            _MoveSpeed = -90.0f;
        _MoveSpeed *= -1;

        if (_MoveSpeed >= 180)
            _MoveSpeed = 180.0f;

        _NowState = "walk";
        _MaxFrame = 6;
        _NowFrame = 0;
    }

}
