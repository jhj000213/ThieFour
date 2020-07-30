using UnityEngine;
using System.Collections;

public class InHouse_Character : MonoBehaviour {


    public UISprite _MySprite;
    public string _NowState;
    public string _MyJob;
    public int _MaxFrame;
    public bool _CanMove;
    int _NowFrame;
    float _Framelate = 0.125f;
    float _NowFrameTime;

    bool _AnimationLoop;
    string _NextAni;

    void Start()
    {
        _CanMove = true;
        _MySprite = GetComponent<UISprite>();
        //_NowState = "stay";
        _MaxFrame = 6;
        _MySprite.material.renderQueue = 5000;
    }
    void Update()
    {
        if (_NowState == "hacking")
            _Framelate = 0.0625f;
        else
            _Framelate = 0.125f;
        _NowFrameTime += Time.smoothDeltaTime;

        if (_NowFrameTime >= _Framelate)
        {
            _NowFrameTime -= _Framelate;
            _NowFrame++;
            if (_NowFrame >= _MaxFrame)
            {
                _NowFrame = 0;
                if (!_AnimationLoop)
                {
                    if (_NextAni == "stay")
                        SetAnimation(_NextAni, true);
                    else if (_NextAni == "hacking")
                        SetAnimation(_NextAni, false);
                    else if (_NextAni == "reload")
                        SetAnimation(_NextAni, "zoom", false);
                    else if (_NextAni == "zoom")
                        SetAnimation(_NextAni, true);
                    else if (_NextAni == "sleeping")
                        SetAnimation(_NextAni, false);
                    else if (_NextAni == "opening")
                        SetAnimation(_NextAni, false);
                    else if (_NextAni == "hit")
                        SetAnimation(_NextAni, true);
                }
            }
            _MySprite.spriteName = "character_" + _NowState + "_" + _MyJob +"_"+ _NowFrame;
        }
        _MySprite.MakePixelPerfect();
    }

    public void SetAnimation(string state,bool canmove)
    {
        _NowFrame = 0;
        _NowState = state;
        if(_MyJob!="guard")
        {
            if (state == "walk")
                _MaxFrame = 8;
            if (state == "run")
                _MaxFrame = 6;
            if (state == "hacking")
                _MaxFrame = 8;
            if (state == "openlaptop")
                _MaxFrame = 22;
            if (state == "closelaptop")
                _MaxFrame = 10;
            if (state == "setmachine")
                _MaxFrame = 22;
            if (state == "onsna")
                _MaxFrame = 8;
            if (state == "shoot")
                _MaxFrame = 4;
            if (state == "reload")
                _MaxFrame = 14;
            if (state == "offsna")
                _MaxFrame = 6;
            if (state == "zoom")
                _MaxFrame = 1;
            if (state == "openingready")
                _MaxFrame = 5;
            if (state == "opening")
                _MaxFrame = 2;
            if (state == "unlock")
                _MaxFrame = 41;
            if (state == "breakwindow")
                _MaxFrame = 13;
        }
        else
        {
            if (state == "stay")
                _MaxFrame = 1;
            if (state == "walk")
                _MaxFrame = 8;
            if (state == "run")
                _MaxFrame = 6;
            if (state == "sleep")
                _MaxFrame = 2;
            if (state == "sleeping")
                _MaxFrame = 1;
            if (state == "hit")
                _MaxFrame = 4;
        }
        
        _AnimationLoop = true;
        _CanMove = canmove;
    }
    public void SetAnimation(string state, string nextani, bool canmove)
    {
        _NowFrame = 0;
        _NowState = state;
        if (_MyJob != "guard")
        {
            if (state == "walk")
                _MaxFrame = 8;
            if (state == "run")
                _MaxFrame = 6;
            if (state == "hacking")
                _MaxFrame = 8;
            if (state == "openlaptop")
                _MaxFrame = 22;
            if (state == "closelaptop")
                _MaxFrame = 10;
            if (state == "setmachine")
                _MaxFrame = 22;
            if (state == "onsna")
                _MaxFrame = 8;
            if (state == "shoot")
                _MaxFrame = 4;
            if (state == "reload")
                _MaxFrame = 14;
            if (state == "offsna")
                _MaxFrame = 6;
            if (state == "zoom")
                _MaxFrame = 1;
            if (state == "openingready")
                _MaxFrame = 5;
            if (state == "opening")
                _MaxFrame = 2;
            if (state == "unlock")
                _MaxFrame = 41;
            if (state == "breakwindow")
                _MaxFrame = 13;
        }
        else
        {
            if (state == "stay")
                _MaxFrame = 1;
            if (state == "walk")
                _MaxFrame = 8;
            if (state == "run")
                _MaxFrame = 6;
            if (state == "sleep")
                _MaxFrame = 2;
            if (state == "sleeping")
                _MaxFrame = 1;
            if (state == "hit")
                _MaxFrame = 4;
        }
        _NextAni = nextani;
        _AnimationLoop = false;
        _CanMove = canmove;
    }
}
