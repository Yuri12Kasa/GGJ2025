using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecordingSceneController : MonoBehaviour
{
    public static event Action OnClipEnd;
    
    [SerializeField] private Button _playAudioButton;
    
    private AudioClip _recordedClip;
    private int _currentPlayer;
    private float _time;

    private void OnEnable()
    {
        OnClipEnd += HideButton;
    }

    private void Start()
    {
        _currentPlayer = GameManagerMauro.Instance.GetCurrentPlayer();

        _playAudioButton.gameObject.SetActive(_currentPlayer != 0);

        if (GameManagerMauro.Instance.track.clip != null)
        {
            _recordedClip = GameManagerMauro.Instance.track.clip;
        }
    }

    public void WaitEndRecording()
    {
        if(_recordedClip)
            StartCoroutine(WaitUntilRecEnd());
    }
    
    private IEnumerator WaitUntilRecEnd()
    {
        yield return new WaitForSeconds(_recordedClip.length);
        OnClipEnd?.Invoke();
    }

    private void HideButton()
    {
        _playAudioButton.gameObject.SetActive(false);
    }
}
