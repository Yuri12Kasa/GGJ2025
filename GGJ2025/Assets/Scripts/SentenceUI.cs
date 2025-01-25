using TMPro;
using UnityEngine;

public class SentenceUI : MonoBehaviour
{
    private TMP_Text _text;
    private string _sentence;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (GameManagerMauro.Instance != null)
        {
            _sentence = GameManagerMauro.Instance.GetSentence();
            _text.text = _sentence;
        }
        else
        {
            Debug.LogWarning("GameManagerMauro.Instance is null");
        }
        
    }
}
