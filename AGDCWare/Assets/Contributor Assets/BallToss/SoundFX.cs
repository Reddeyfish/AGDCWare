using UnityEngine;
using System.Collections;

namespace DerekEdrich
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Light))]
    public class SoundFX : MonoBehaviour
    {

        Material mat;
        Light internalLight;
        // Use this for initialization
        void Awake()
        {
            internalLight = GetComponent<Light>();
            MeshRenderer rend = GetComponent<MeshRenderer>();
            mat = rend.material = Instantiate(rend.material);

            Matrix4x4 matrix = Camera.main.cameraToWorldMatrix;
            mat.SetMatrix("_InverseView", matrix);

            Vector3 color = Random.insideUnitSphere;
            float maxComponent = Mathf.Max(color.x, color.y, color.z);
            color *= 1 / maxComponent;
            Color myColor = new Color(color.x, color.y, color.z);

            internalLight.color = myColor;
            mat.SetColor("_HighlightColor", myColor);
        }

        void Start()
        {
            Initialize(15, 3);
            

        }

        public void Initialize(float radius, float duration)
        {
            StartCoroutine(Animation(radius, duration));
        }

        IEnumerator Animation(float radius, float duration)
        {
            float time = 0;
            internalLight.range = radius;
            while (time < duration)
            {
                float radiusValue = radius * time / duration;
                transform.localScale = radiusValue * 2 * Vector3.one;
                mat.SetFloat("_Radius", radiusValue);
                internalLight.intensity = (1 - time / duration) / 5;
                yield return null;
                time += Time.deltaTime;
            }
            Destroy(this.gameObject);
        }
    }
}