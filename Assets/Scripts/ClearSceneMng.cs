using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClearSceneMng : MonoBehaviour {
    public GameObject _Panel;
    public UISprite _Rank;
    public GameObject _ClearLabel;
    public GameObject _ScoreLabel;
    public GameObject _TimeLabel;
    public GameObject _ToolTip;

    public GameObject _ScoreNumberGrid;
    public GameObject _TimeNumberGrid;

    public UISprite[] _ScoreNumber;
    public UISprite[] _TimeNumber;

    bool _Boom;
    float _NowTime;

    public GameObject[] _FireEffectArray = new GameObject[4];

    void SetTime()
    {
        //StaticDataMng._ClearTime = 76.0f;
        float min = StaticDataMng._ClearTime / 60.0f;
        float second = StaticDataMng._ClearTime % 60.0f;
        int score = StaticDataMng._Score;

        _TimeNumber[0].spriteName = "clear_number_"+((int)(second % 10.0f)).ToString();
        _TimeNumber[1].spriteName = "clear_number_"+((int)(second / 10.0f)).ToString();
        _TimeNumber[2].spriteName = "clear_number_"+((int)(min % 10.0f)).ToString();
        _TimeNumber[3].spriteName = "clear_number_"+((int)(min / 10.0f)).ToString();


        _ScoreNumber[0].spriteName = "clear_number_" + ((int)(score % 10));
        _ScoreNumber[1].spriteName = "clear_number_" + ((int)((score % 100)/10));
        _ScoreNumber[2].spriteName = "clear_number_" + ((int)((score % 1000) / 100));
        _ScoreNumber[3].spriteName = "clear_number_" + ((int)((score % 10000) / 1000));
        _ScoreNumber[4].spriteName = "clear_number_" + ((int)((score % 100000) / 10000));
    }

    void Update()
    {
        if(_Boom)
        {
            _NowTime -= Time.smoothDeltaTime;
            if(_NowTime<=0.0f)
            {
                int num = Random.Range(0, 4);
                GameObject obj = NGUITools.AddChild(_Panel, _FireEffectArray[num]);
                obj.transform.localPosition = new Vector3(Random.Range(-540.0f, 540.0f), Random.Range(-300.0f, 300.0f), 0);
                obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                _NowTime = Random.Range(0.4f, 1.6f);
            }
        }

        for (int i = 1; i < 12; i++)
        {
            if (Input.GetButtonDown("Player" + i.ToString() + "_O"))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);

        _Rank.gameObject.SetActive(true);
        StartCoroutine(Step1(1.0f));
        SetTime();
    }


    IEnumerator Step1(float time)
    {
        yield return new WaitForSeconds(time);

        _Boom = true;
        _ClearLabel.SetActive(true);
        StartCoroutine(Step2(1.0f));
    }
    IEnumerator Step2(float time)
    {
        yield return new WaitForSeconds(time);

        _ScoreLabel.SetActive(true);
        _ScoreNumberGrid.SetActive(true);
        StartCoroutine(Step3(1.0f));
    }
    IEnumerator Step3(float time)
    {
        yield return new WaitForSeconds(time);

        _TimeLabel.SetActive(true);
        _TimeNumberGrid.SetActive(true);
        StartCoroutine(Step4(1.0f));
    }
    IEnumerator Step4(float time)
    {
        yield return new WaitForSeconds(time);

        _ToolTip.SetActive(true);
        //StartCoroutine(Step4(1.0f));
    }
}