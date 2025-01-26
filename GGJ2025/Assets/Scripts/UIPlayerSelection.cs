using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIPlayerSelection : MonoBehaviour
{
    public UnityEvent OnValueChanged;
    
    [SerializeField] private TMP_Text _playerNumberText;
    [SerializeField] private int _maxPlayers;
    [SerializeField] private int _minPlayers;
    private int _playerNumber;

    private void Awake()
    {
        _playerNumber = _minPlayers;
        _playerNumberText.text = _playerNumber.ToString();
    }

    private void OnEnable()
    {
        OnValueChanged.AddListener(UpdatePlayersNumberText);
        OnValueChanged.AddListener(SetPlayersNumber);
    }

    private void OnDisable()
    {
        OnValueChanged.RemoveListener(UpdatePlayersNumberText);
        OnValueChanged.RemoveListener(SetPlayersNumber);
    }

    private void UpdatePlayersNumberText()
    {
        _playerNumberText.text = _playerNumber.ToString();
    }

    private void SetPlayersNumber()
    {
        GameManagerMauro.Instance.SetPlayer(_playerNumber);
    }

    public void AddPlayer()
    {
        var check = _playerNumber + 1 <= _maxPlayers;
        _playerNumber = check ? _playerNumber + 1 : _maxPlayers;
        OnValueChanged?.Invoke();
    }

    public void RemovePlayer()
    {
        var check = _playerNumber - 1 >= _minPlayers;
        _playerNumber = check ? _playerNumber - 1 : _minPlayers;
        OnValueChanged?.Invoke();
    }
}
