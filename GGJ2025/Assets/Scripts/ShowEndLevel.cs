using System;
using TMPro;
using UnityEngine;

public class ShowEndLevel : MonoBehaviour
{
    public static ShowEndLevel Instance;
    [SerializeField] private TextMeshProUGUI _endLevelText;
    [SerializeField] private GameObject _endLevel; 
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _endLevel.SetActive(false);
    }

    public void ShowText(string text,Color color)
    {
        _endLevel.SetActive(true);
        _endLevelText.text = text;
        _endLevelText.color = color;
    }
}
