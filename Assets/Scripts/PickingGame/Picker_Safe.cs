using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Picker_Safe : MonoBehaviour {

    public List<Vector3> _CreatePosList = new List<Vector3>();
    public GameObject _Noise;

    public Vector3 _MousePos;
    public GameObject _Mouse;

    float _NoiseRange;

    float _NowTime;
    const float _DelayTime = 2.0f;

    public bool _MakeNoise;

    void Start()
    {
        _NoiseRange = 300.0f;
        int num = Random.Range(0, _CreatePosList.Count);
        transform.localPosition = _CreatePosList[num];
        transform.localEulerAngles = new Vector3(0, 0, _CreatePosList[num].z);

        if (Mathf.Abs(transform.localEulerAngles.z - 0) <5.0f)
            _MousePos = transform.localPosition + new Vector3(0,-155,0);
        else if (Mathf.Abs(transform.localEulerAngles.z - 90) < 5.0f)
            _MousePos = transform.localPosition + new Vector3(155, 0, 0);
        else if (Mathf.Abs(transform.localEulerAngles.z - 180) < 5.0f)
            _MousePos = transform.localPosition + new Vector3(0, 155, 0);
        else if (Mathf.Abs(transform.localEulerAngles.z - 270) < 5.0f)
            _MousePos = transform.localPosition + new Vector3(-155, 0, 0);

        _MousePos = new Vector3(_MousePos.x, _MousePos.y, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        PlayerMng.Data._PickerHoldPosition = _MousePos;
        PlayerMng.Data._Picker_SafePosition = transform.localPosition;
    }

    void Update()
    {
        if(_MakeNoise)
        {
            _NowTime += Time.smoothDeltaTime;
            _NoiseRange += Time.smoothDeltaTime * 8.0f;
            if (_NowTime >= _DelayTime)
            {
                float size = _NoiseRange / 2000.0f;

                _NowTime -= _DelayTime;
                GameObject effect = NGUITools.AddChild(gameObject, _Noise);
                effect.GetComponent<TweenScale>().from = new Vector3(size, size, 1);
            }
        }
        
    }
}
