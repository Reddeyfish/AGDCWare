using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DerekEdrich
{

    public class Swipe : MonoBehaviour
    {

        int swipableLayerMask;

        [SerializeField]
        protected float minDistance;

        // Use this for initialization
        void Start()
        {
            swipableLayerMask = LayerMask.GetMask(Tags.Layers.swipable);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {

                StartCoroutine(OnClick());
            }
        }

        IEnumerator OnClick()
        {
            Vector2 startPoint = Format.mousePosInWorld();
            Vector2 previousPoint = startPoint;

            HashSet<Transform> alreadyHitObjects = new HashSet<Transform>();

            while (Input.GetMouseButton(0))
            {
                Vector2 newPoint = Format.mousePosInWorld();
                if (Vector2.Distance(startPoint, newPoint) < minDistance)
                    yield return null;
                else
                    break;
            }

            while (Input.GetMouseButton(0))
            {
                Vector2 newPoint = Format.mousePosInWorld();

                RaycastHit2D[] hitObjects = Physics2D.LinecastAll(previousPoint, newPoint, swipableLayerMask);


                foreach (RaycastHit2D hit in hitObjects)
                {
                    if (!alreadyHitObjects.Contains(hit.transform))
                    {
                        alreadyHitObjects.Add(hit.transform);
                        foreach (ISwipable swipe in hit.transform.GetComponentsInChildren<ISwipable>())
                        {
                            swipe.Notify(new SwipableMessage());
                        }
                    }
                }

                previousPoint = newPoint;

                yield return null;
            }
        }
    }

    public interface ISwipable
    {
        void Notify(SwipableMessage m);
    }

    public class SwipableMessage
    {

    }

}