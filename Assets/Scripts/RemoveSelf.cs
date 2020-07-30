using UnityEngine;
using System.Collections;

public class RemoveSelf : MonoBehaviour {

    public float _DelayTime;

    float _NowTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_DelayTime);

        Destroy(gameObject);
    }

    void Update()
    {
        _NowTime += Time.smoothDeltaTime;
        if (_NowTime >= _DelayTime)
            Destroy(gameObject);
    }
}
