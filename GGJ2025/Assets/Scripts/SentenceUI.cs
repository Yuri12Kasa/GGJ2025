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
        _sentence = GameManagerMauro.Instance.GetSentence();
        _text.text = _sentence;
    }
}
