using System;
using UnityEngine;

public class ScrollingObjectBehaviour : MonoBehaviour 
{
    [SerializeField]
    private BoolVariable isPause;

    [SerializeField]
    private Transform endPoint;

    public int distanceXToOtherObject { get; private set;}

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;

    public float ObjectWidth
    {
        get
        {
            return bc2D.size.x;
        }
    }

    public float ObjectEndXPoint
    {
        get
        {
            return transform.position.x + (ObjectWidth * .5f);
        }
    }

    private SpriteRenderer[] sprites;

    private float objectSpeed;

    public void Set (int layer, int speed, bool hasColor, Color color, int distanceX = 0)
    {
        objectSpeed = speed;
        distanceXToOtherObject = distanceX;
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sortingOrder = layer;
            if(hasColor)
            {
                sprites[i].color = color; 
            }
        }
    }

    public Action<ScrollingObjectBehaviour> OnDisableObjectAction = delegate { };

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();

    }

    private void LateUpdate()
    {
        if (!isPause.Value)
        {
            if (ObjectEndXPoint < endPoint.position.x)
            {
                OnDisableObjectAction(this);
                gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isPause.Value)
        {
            rb2D.velocity = new Vector2(-objectSpeed, 0);
        } else
        {
            rb2D.velocity = Vector2.zero;
        }
    }
}
