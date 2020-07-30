using UnityEngine;
using System.Collections;

public class Player_JoyStick_Move : MonoBehaviour {
    

    Vector3 _ZeroPoint = new Vector2(1080, 180);

    public Vector3 _NowPoint;

    public float _JoyStickRadius = 35.0f;
    public float _JoyStickBoardRadius = 75.0f;

    public float _JoyStickAngle;

    public bool _Moving = false;

    public int _PlayerNumber;
    void Update()
    {
        if(_PlayerNumber!=0)
        {
            _NowPoint = new Vector3(Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_X"), -1, 1),
                Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_Y"), -1, 1));
            _NowPoint *= 10;
            float rad = Mathf.Atan2(Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_Y"), -1, 1), 
                Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_X"), -1, 1));
            _JoyStickAngle = rad * Mathf.Rad2Deg;
        }
        
    }
}
