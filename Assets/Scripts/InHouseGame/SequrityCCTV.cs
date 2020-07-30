using UnityEngine;
using System.Collections;

public class SequrityCCTV : MonoBehaviour {

    public bool _On;
    public GameObject _ViewCamera;
    public UISprite _MySprite;

    public GameObject _MakeGuardPos;

    Player_InHouse _LockedPlayer;

    FieldOfView _CCTVView;
    bool _MakeGuard;

    float _MakeDelayTime;
    float _Delay = 5.2f;

    public void CCTVOff()
    {
        _On = false;
        _ViewCamera.SetActive(false);
        _MySprite.spriteName = "sequrity_cctvoff";
        
    }

    void Start()
    {
        _CCTVView = _ViewCamera.GetComponent<FieldOfView>();
    }

    void Update()
    {
        _MakeDelayTime -= Time.smoothDeltaTime;
        if(_MakeDelayTime<=0.0f)
        {
            if (_CCTVView._Find)
            {
                _LockedPlayer = _CCTVView._VisibleTargets[0];

                GameObject obj = NGUITools.AddChild(transform.parent.gameObject, PlayerMng.Data._CCTVDangerEffect);
                obj.transform.localPosition = gameObject.transform.localPosition + new Vector3(0, 100, 0);
                _MakeDelayTime = _Delay;
                StartCoroutine(MakeGuard(1.0f));
                StartCoroutine(SearchStart(5.0f));
            }
        }
        
    }

    IEnumerator MakeGuard(float time)
    {
        yield return new WaitForSeconds(time);
        //float angle = (transform.localEulerAngles.z)*Mathf.Deg2Rad;
        //Vector3 plus = new Vector3(Mathf.Cos(angle)*_MakeGuardPos.transform.localPosition.x*2, Mathf.Sin(angle)*_MakeGuardPos.transform.localPosition.y*2,0);

        GameObject guard = NGUITools.AddChild(PlayerMng.Data._UIRoot.transform.parent.gameObject, PlayerMng.Data._SequrityGuard);
        Vector3 pos = transform.localPosition;
        float angle1 = Mathf.Atan2(_LockedPlayer.transform.parent.localPosition.y - pos.y, _LockedPlayer.transform.parent.localPosition.x - pos.x);
        angle1 *= Mathf.Rad2Deg;
        guard.transform.localEulerAngles = new Vector3(0, 0, angle1-90);
        guard.transform.localPosition = _MakeGuardPos.transform.localPosition;
        PlayerMng.Data._GameScore -= 500;
        _LockedPlayer = null;
        guard.GetComponent<SequrityGuard>().CCTVCalled();
        guard.GetComponent<SequrityGuard>().Init();

        //guard.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, 90);
    }

    IEnumerator SearchStart(float time)
    {
        yield return new WaitForSeconds(time);

        _CCTVView._Find = false;
    }
}
