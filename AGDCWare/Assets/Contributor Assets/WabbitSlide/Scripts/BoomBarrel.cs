using UnityEngine;

namespace buckslice {
    public class BoomBarrel : MonoBehaviour {
        private Rigidbody rbody;
        private Transform bunny;

        void Awake() {
            rbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update() {
            if(bunny && bunny.position.z - 50.0f > transform.position.z) {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision col) {
            if (col.gameObject.tag == "Player") {
                rbody.freezeRotation = true;
                Collider[] explodees = Physics.OverlapSphere(transform.position, 20f);
                foreach (Collider c in explodees) {
                    Rigidbody rb = c.GetComponent<Rigidbody>();
                    if (rb && rb != rbody) {
                        rb.AddExplosionForce(30f, transform.position, 40f, 3f, ForceMode.Impulse);
                    }
                }

                GetComponent<ParticleSystem>().Play();
                Destroy(gameObject, 1.5f);
            }
        }

        public void init(BunnyController bc) {
            rbody.velocity = new Vector3(0.0f, -0.5f, 0.5f) * bc.getSpeed();
            bunny = bc.transform;
        }
    }
}