using UnityEngine;
using System.Collections;

public class LoadingSceneMng : MonoBehaviour {

    public GameObject _LoadPoint;
    public UI2DSprite _LoadGaze;

    float _NowTime;

    void Update()
    {
        _NowTime += Time.smoothDeltaTime;
        _LoadGaze.fillAmount = _NowTime/2;
        _LoadPoint.transform.localPosition = new Vector3((_NowTime / 2) * 1848, 0, 0);
        if (_LoadPoint.transform.localPosition.x >= 1848.0f)
            _LoadPoint.transform.localPosition = new Vector3(1848, 0, 0);
        _LoadPoint.transform.localPosition -= new Vector3(8, 0, 0);
    }
}
