using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance => _instance;
    private static TimeManager _instance;

    public UnityEvent OnTimeExpired;

    public float GetNormalizedTime => (_timer / sceneTime);

    public float SceneTime => sceneTime;
    [SerializeField] private float sceneTime = 20f;
    public float Timer => _timer;
    [SerializeField] private float _timer;

    private void Awake()
    {
        InitSingleton();
    }

    private void InitSingleton()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_timer < sceneTime)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
            OnTimeExpired.Invoke();
            gameObject.SetActive(false);
            EndGameForNow();
        }
    }

    public float GetTime()
    {
        return _timer;
    }

    public void EndGameForNow()
    {
        GameManagerMauro.Instance.NextPlayer();
    }
}
