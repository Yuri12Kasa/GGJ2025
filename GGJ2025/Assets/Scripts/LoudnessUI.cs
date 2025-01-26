using UnityEngine;
using UnityEngine.UI;

public class LoudnessUI : MonoBehaviour
{
    public MoveFromLoudness bubble;
    public Image imageFill;
    private float _maxSpeed;

    private void Awake()
    {
        _maxSpeed = bubble.maxSpeed;
    }

    private void Update()
    {
        imageFill.fillAmount = bubble.GetNormalizedLoudness() / _maxSpeed;
    }
}
