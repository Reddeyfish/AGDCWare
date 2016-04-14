using UnityEngine;

namespace buckslice {
    public class BunnyController : MonoBehaviour {
        public float sideSpeed = 15f;

        private float speedLimit = 100f;
        public Rigidbody rbody { get; private set; }
        private AudioSource source;
        private Transform model;
        private int carrotCount;
        private float startTime = -1.0f;

        // Use this for initialization
        void Start() {
            rbody = GetComponent<Rigidbody>();
            source = GetComponent<AudioSource>();
            model = transform.Find("Model");
        }

        // Update is called once per frame
        void Update() {
            // lerp model to point along velocity vector
            model.forward = Vector3.Slerp(model.forward, rbody.velocity, 4f * Time.deltaTime);
            if (rbody.velocity.sqrMagnitude > speedLimit * speedLimit) {
                rbody.velocity = rbody.velocity.normalized * speedLimit;
            }
        }

        void FixedUpdate() {
            rbody.AddForce(transform.right * Input.GetAxis("Horizontal") * sideSpeed);
        }

        // only trigger is carrots
        void OnTriggerEnter(Collider col) {
            source.Play();
            carrotCount++;
            if (carrotCount >= 5) {
                // gain a life in main game?
            }
            speedLimit += 10f;
            Destroy(col.gameObject);
            rbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
        }

        void OnCollisionEnter(Collision c) {
            if (startTime < 0.0f && Time.timeSinceLevelLoad > 1.0f) {
                startTime = Time.timeSinceLevelLoad;
            }
        }

        public float getStartTime() {
            return startTime;
        }

        public float getSpeed() {
            return rbody.velocity.magnitude;
        }
    }
}