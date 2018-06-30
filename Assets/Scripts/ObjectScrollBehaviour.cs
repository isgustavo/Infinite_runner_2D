using ODT.Scriptable;
using UnityEngine;

namespace ODT.IR
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObjectScrollBehaviour : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable isPause;

        [Header("Auto Init Object")]
        [SerializeField]
        private bool autoInit;
        [SerializeField]
        private Vector2 autoEndPointPosition;
        [SerializeField]
        private int autoScrollSpeed;

        private Vector2 endPointPosition;
        private float scrollSpeed;

        private SpriteRenderer sprite;
        private SpriteRenderer[] childrenSprites;

        private void OnEnable()
        {
            sprite = GetComponent<SpriteRenderer>();
            childrenSprites = GetComponentsInChildren<SpriteRenderer>();

            if (autoInit)
            {
                endPointPosition = autoEndPointPosition;
                scrollSpeed = autoScrollSpeed;
            }
        }

        public void Init(int layer, Color color, Vector2 startPoint, Vector2 endPoint, float speed)
        {
            sprite.sortingOrder = layer;
            sprite.color = color;
            transform.position = startPoint;
            endPointPosition = endPoint;
            scrollSpeed = speed;

            for (int i = 0; i < childrenSprites.Length; i++)
            {
                childrenSprites[i].sortingOrder = layer;
                childrenSprites[i].color = color;
            }
        }

        private void Update()
        {
            if (isPause.Value)
            {
                return;
            }

            if (transform.position.x > endPointPosition.x)
            {
                transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public float GetWidth()
        {
            return sprite.bounds.size.x;
        }

    }
}