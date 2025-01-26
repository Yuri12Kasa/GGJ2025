using System.Collections.Generic;
using UnityEngine;

public class MoveFromLoudness : MonoBehaviour
{
    public float maxSpeed = 2;
    public float moveSpeed = 0.1f;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("No rigidbody found");
        }

        if (detector == null)
        {
            Debug.LogError("No detector found");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private float GetLoudnessFromMicrophone()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;
            
        return loudness;
    }

    public void AddModifier(List<TrackModifier> trackModifier)
    {
        GameManagerMauro.Instance.AddTrackModifier(trackModifier);
    }
    private void Move()
    {
        float loudness = GetLoudnessFromMicrophone();
        if (loudness == 0)
        {
            float yVelocity = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, Mathf.Clamp(yVelocity, -maxSpeed, maxSpeed), _rigidbody.linearVelocity.z);
        }
        else
        {
            float yVelocity = _rigidbody.linearVelocity.y + (moveSpeed * Time.deltaTime * loudness * loudnessSensibility);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, Mathf.Clamp(yVelocity, -maxSpeed, maxSpeed), _rigidbody.linearVelocity.z);
        }
    }
}