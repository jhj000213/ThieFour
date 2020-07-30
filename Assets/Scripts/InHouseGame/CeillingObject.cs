using UnityEngine;
using System.Collections;

public class CeillingObject : MonoBehaviour {

    public bool _OffAlpha;
    UI2DSprite _MySprite;
    float _Alpha;

    void Start()
    {
        _MySprite = GetComponent<UI2DSprite>();
        _Alpha = 1.0f;
    }
    void Update()
    {
        if (_OffAlpha)
        {
            _Alpha -= Time.smoothDeltaTime * 2.0f;
            if (_Alpha <= 0)
                _Alpha = 0;
        }
        else
        {
            _Alpha += Time.smoothDeltaTime * 2.0f;
            if (_Alpha >=1)
                _Alpha = 1;
        }
         _MySprite.alpha = _Alpha;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "player")
    //    {
    //        _OffAlpha = true;
    //        //Debug.Log("in");
    //    }
    //}
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            _OffAlpha = true;
            //Debug.Log("in");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            _OffAlpha = false;
            //Debug.Log("off");
        }
    }
}
