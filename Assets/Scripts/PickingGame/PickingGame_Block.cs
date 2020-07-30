using UnityEngine;
using System.Collections;

public class PickingGame_Block : MonoBehaviour {

    public float _BlockAngle;
    public int _BlockNum;

    public UI2DSprite _BicCircle;
    public UISprite _PinPoint;

    public Vector3 _Size = new Vector3(1, 1, 1);
    bool _Crash;
    public bool _Same;

    public void Init(float angle,int num)
    {
        
        _BlockNum = num;
        transform.localEulerAngles = new Vector3(0, 0, angle);
        _BlockAngle = transform.localEulerAngles.z;
        if(_BlockAngle>180.0f)
        {
            float a = _BlockAngle - 180.0f;
            float b = -1 * (180.0f - a);
            _BlockAngle = b;
        }
        if(num==1)
            _PinPoint.spriteName = "picker_block_o";
        else if(num==2)
            _PinPoint.spriteName = "picker_block_x";
        else if(num==3)
            _PinPoint.spriteName = "picker_block_squar";
        else if(num==4)
            _PinPoint.spriteName = "picker_block_tri";
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _Size, Time.smoothDeltaTime*4);
        if (_Crash)
        {
            transform.localEulerAngles += new Vector3(0, 0, 20);
            if (_Same)
                _PinPoint.spriteName = "picker_block_white";
            else
                _PinPoint.spriteName = "picker_block_red";
        }
    }

    public void Smalling()
    {
        _Size -= new Vector3(0.25f, 0.25f, 0);
        
    }

    public void CrashBlock()
    {
        _Size -= new Vector3(0.25f, 0.25f, 0);
        StartCoroutine(Destroy(0.25f));
        _Crash = true;
    }
    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    public void SetSpriteDepthPlus()
    {
        _BicCircle.depth += 3;
        _PinPoint.depth += 3;
    }
}
