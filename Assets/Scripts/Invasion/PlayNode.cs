using UnityEngine;
using System.Collections;

public class PlayNode : MonoBehaviour {

    public Vector2 _NowPos;
    public int _NodeNum;

    public GameObject _Sprite_Red;
    public GameObject _Sprite_Blue;
    public GameObject _Sprite_Orange;
    public GameObject _Sprite_Green;
	public void Init(int nodenum)
    {
        _NodeNum = nodenum;
        if (_NodeNum == 1)
            _Sprite_Red.SetActive(true);
        else if (_NodeNum == 2)
            _Sprite_Blue.SetActive(true);
        else if (_NodeNum == 3)
            _Sprite_Orange.SetActive(true);
        else if (_NodeNum == 4)
            _Sprite_Green.SetActive(true);
    }

    void Update()
    {
        _NowPos = gameObject.transform.localPosition;
    }
}
