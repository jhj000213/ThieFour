using UnityEngine;
using System.Collections;

public class PattunNode : MonoBehaviour {

    GameObject _EditLine;
    UILabel _TimeLabel;

    public int _Sequence;
    public float _CreateTime;
    public int _PattunNum;
    public float _MusicMaxTime;
    float _BarLenght;

    bool _SelectMe;

    public void Init(int sequence,float time,int num,float musicmaxtime,float BarLenght)
    {
        _Sequence = sequence;
        _CreateTime = time;
        _PattunNum = num;
        _MusicMaxTime = musicmaxtime;
        _BarLenght = BarLenght;
        _EditLine = transform.parent.parent.gameObject;
        _TimeLabel = transform.Find("Label").GetComponent<UILabel>();
    }

    void Update()
    {
        _TimeLabel.text = _CreateTime.ToString();
        _TimeLabel.enabled = false;
        if(_SelectMe)
        {
            _TimeLabel.enabled = true;
            Vector2 mousePos = new Vector2(Input.mousePosition.x * (1920.0f / Screen.width), Input.mousePosition.y * (1080.0f / Screen.height));
            gameObject.transform.localPosition = new Vector3(mousePos.x-960.0f-_EditLine.transform.localPosition.x,100,0);
            float posx = (transform.localPosition.x / _BarLenght) * _MusicMaxTime;
            _CreateTime = posx;
        }
    }

    public void SelectMe()
    {
        _SelectMe = true;
    }

    public void DumfMe()
    {
        _SelectMe = false;

        float posx = (transform.localPosition.x / _BarLenght) * _MusicMaxTime;
        _CreateTime = posx;
    }
}
