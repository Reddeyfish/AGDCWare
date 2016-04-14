using UnityEngine;

namespace buckslice {
    public class BoostCarrot : MonoBehaviour {

        public Transform bunny { get; set; }

        // Use this for initialization
        void Awake() {
            // slam down onto level
            RaycastHit hit;
            Physics.Raycast(new Ray(transform.position, Vector3.down), out hit);
            transform.position = hit.point;
            transform.up = transform.TransformDirection(hit.normal);
            transform.position += transform.up * 0.8f;
        }

        void Update() {
            if (bunny.position.z - 10.0f > transform.position.z) {
                Destroy(gameObject);
            }

            transform.Rotate(0, 90 * Time.deltaTime, 0);
            float s = Mathf.Sin(Time.time * 6f) * Time.deltaTime * 5f;
            transform.position += transform.up * s;
        }

    }
}