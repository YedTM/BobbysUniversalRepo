using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sRenderer;

    bool isColliding = false;

    [SerializeField]
    Vector2 rectSize = Vector2.one;

    [SerializeField]
    bool useRenBound = true;

    public Vector2 RectMin
    {
        get
        {
            if (useRenBound) return transform.position - sRenderer.bounds.extents;
            return (Vector2)transform.position - (rectSize / 2);
        }
    }

    public Vector2 RectMax
    {
        get
        {
            if (useRenBound) return transform.position + sRenderer.bounds.extents;
            else return (Vector2)transform.position + (rectSize / 2);
        }
    }

    public bool IsColliding
    {
        set
        {
            isColliding = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
            sRenderer.color = Color.red;
        }
        else
        {
            sRenderer.color = Color.white;
        }

        if (useRenBound)
        {
            rectSize = sRenderer.bounds.extents * 2;
        }
    }

    private void OnDrawGizmos()
    {
        if (isColliding)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.grey;
        }

        Gizmos.DrawWireCube(transform.position, rectSize);
    }
}
