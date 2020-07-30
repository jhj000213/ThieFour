using UnityEngine;
using System.Collections;

public class LobbyMng : MonoBehaviour {
    public GameObject _EditPanel;
    public GameObject _PlayPanel;
    public GameObject _FileNamePanel;
    public GameObject _CorrectPanel;
    public GameObject _EditLineSize;

    public GameObject _HackingPanel;
    public GameObject _PickingPanel;
    public GameObject _InHousePanel;

    public Editing _Editor;
    public InvasionMng _InvasionMng;
    public SoundMng _SoundMng;

    public UILabel _FileNameLabel;

    public string _FileName;

    public void OpenEditor()
    {
        _Editor._Starting = true;
        _EditPanel.SetActive(true);
        _Editor._NowTime = 0.0f;
        _SoundMng._MySource.Play();
    }
    public void OpenPlay()
    {
        _InvasionMng._Starting = true;
        _InvasionMng.Init();
        _PlayPanel.SetActive(true);
        _InvasionMng._NowTime = 0.0f;
        _InvasionMng.StartMusic();
    }

    public void OpenEditLineSize()
    {
        _EditLineSize.SetActive(true);
    }
    public void CloseTap()
    {
        _EditPanel.SetActive(false);
        _PlayPanel.SetActive(false);
        _EditLineSize.SetActive(false);
        _InvasionMng._Starting = false;
        _Editor._Starting = false;
        _SoundMng._MySource.Stop();
        _HackingPanel.SetActive(false);
        _PickingPanel.SetActive(false);
        _InHousePanel.SetActive(false);
    }

    public void OpenFileNameTab()
    {
        _FileNamePanel.SetActive(true);
    }
    public void SetFileName()
    {
        _FileName = _FileNameLabel.text;
        _Editor._FileName = _FileName;
        _InvasionMng._FileName = _FileName;
        _SoundMng._FileName = _FileName;
        
        _FileNamePanel.SetActive(false);
    }

    public void OpenCorrect()
    {
        _CorrectPanel.SetActive(true);
        _SoundMng._MySource.time = 0.0f;
        _SoundMng.Init();
        _SoundMng._StartButton.SetActive(true);
        _SoundMng._PauseButton.SetActive(false);
    }

    public void OpenHacking()
    {
        _HackingPanel.SetActive(true);
    }
    public void OpenPicking()
    {
        _PickingPanel.SetActive(true);
    }
    public void OpenInHouse()
    {
        _InHousePanel.SetActive(true);
    }
}
