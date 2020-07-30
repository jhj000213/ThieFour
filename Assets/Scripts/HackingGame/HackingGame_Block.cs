using UnityEngine;
using System.Collections;

public class HackingGame_Block : MonoBehaviour {

    public int _VectorNum;

    public UISprite _LeftImage;
    public UISprite _RightImage;

    bool _Moving;
    Vector3 _TargetPos;

    public bool _VirusBlock;

    public void Init(int vnum,bool virus)
    {
        _VectorNum = vnum;
        _VirusBlock = virus;
        if (_VectorNum == 0)
        {
            _LeftImage.spriteName = "hacking_clearblock";
            if (_VirusBlock)
                _RightImage.spriteName = "hacking_failblock";
        }
        else
        {
            _RightImage.spriteName = "hacking_clearblock";
            if (_VirusBlock)
                _LeftImage.spriteName = "hacking_failblock";
        }
    }

    void Update()
    {
        if (_Moving)
            transform.localPosition = Vector3.Lerp(transform.localPosition, _TargetPos, Time.smoothDeltaTime * 20);
    }

    public void MoveDown()
    {
        //transform.localPosition -= new Vector3(0, 170, 0);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition - new Vector3(0, 170, 0), Time.smoothDeltaTime * 50);
        _Moving = true;
        _TargetPos = transform.localPosition - new Vector3(0, 170, 0);
    }
}
