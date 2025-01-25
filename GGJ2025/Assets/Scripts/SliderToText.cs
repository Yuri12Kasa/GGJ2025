using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    [SerializeField] private TMP_Text _playersNumber;
    private Slider _playerSelector;

    private void Awake()
    {
        _playerSelector = GetComponent<Slider>();
        _playerSelector.wholeNumbers = true;
    }

    public void UpdatePlayersNumberText()
    {
        _playersNumber.text = _playerSelector.value.ToString();
    }

    public void SetPlayersNumber()
    {
        GameManagerMauro.Instance.SetPlayer((int)_playerSelector.value);
    }
}
