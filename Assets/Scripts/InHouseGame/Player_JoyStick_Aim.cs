using UnityEngine;
using System.Collections;

public class Player_JoyStick_Aim : MonoBehaviour {

    //public GameObject _JoyStickBall;

    Vector2 _ZeroPoint = new Vector2(200, 180);


    public float _JoyStickRadius = 35.0f;
    public float _JoyStickBoardRadius = 75.0f;

    public float _JoyStickAngle;

    public bool _Moving = false;
    public int _PlayerNumber;
    void Update()
    {
        if (_PlayerNumber != 0)
        {
            if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_RightWheel_Y") != 0 || Input.GetAxis("Player" + _PlayerNumber.ToString() + "_RightWheel_X") != 0)
            {
                float rad = Mathf.Atan2(Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_RightWheel_Y"), -1, 1), Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_RightWheel_X"), -1, 1));
                _JoyStickAngle = rad * Mathf.Rad2Deg;
            }
        }
    }
}
