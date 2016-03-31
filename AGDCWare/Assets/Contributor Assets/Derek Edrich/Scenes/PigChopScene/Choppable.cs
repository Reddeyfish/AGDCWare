using UnityEngine;
using System.Collections;

namespace DerekEdrich
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Choppable : AbstractSwipable, ISpawnable
    {
        [SerializeField]
        protected Sprite chopped;
        SpriteRenderer rend;
        Sprite unchopped;

        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
            unchopped = rend.sprite;
        }

        public void Create()
        {
            rend.sprite = unchopped;
            Vector3 baseScale = transform.localScale;
            Callback.DoLerp((float l) => transform.localScale = l * baseScale, 0.1f, this);
        }

        public override void Notify(SwipableMessage m)
        {
            rend.sprite = chopped;
        }
    }
}