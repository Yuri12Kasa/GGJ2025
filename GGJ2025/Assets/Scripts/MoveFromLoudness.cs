using UnityEngine;

namespace Yuri
{
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
            
            float yVelocity = Mathf.Clamp(_rigidbody.linearVelocity.y * Time.fixedTime * moveSpeed, -maxSpeed, maxSpeed);
            _rigidbody.linearVelocity += Vector3.up * yVelocity;
        }

        private float GetLoudnessFromMicrophone()
        {
            float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

            if (loudness < threshold)
                loudness = 0;
            
            return loudness;
        }

        private void Move()
        {
            float loudness = GetLoudnessFromMicrophone();
            if (loudness == 0)
                return;

            float yVelocity = Mathf.Clamp(loudness * moveSpeed * Time.fixedTime, 0, maxSpeed);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, yVelocity, _rigidbody.linearVelocity.z);
        }
    }
}
