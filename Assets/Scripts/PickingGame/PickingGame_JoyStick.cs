using UnityEngine;
using System.Collections;

public class PickingGame_JoyStick : MonoBehaviour {

    //public GameObject _JoyStickBall;
    public GameObject _AngleArrow;

    Vector2 _ZeroPoint = new Vector2(640.0f, 360.0f);

    public float _JoyStickRadius = 35.0f;
    public float _JoyStickBoardRadius = 75.0f;

    public float _JoyStickAngle;

    public int _PlayerNumber;

    public bool _Moving = false;

	void Update () 
    {
        if (Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_Y") != 0 || Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_X") != 0)
        {
            float rad = Mathf.Atan2(Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_Y"), -1, 1), Mathf.Clamp(Input.GetAxis("Player" + _PlayerNumber.ToString() + "_LeftWheel_X"), -1, 1));
            _JoyStickAngle = rad * Mathf.Rad2Deg;
            _AngleArrow.transform.localEulerAngles = new Vector3(0, 0, _JoyStickAngle);
        }
	}
}
