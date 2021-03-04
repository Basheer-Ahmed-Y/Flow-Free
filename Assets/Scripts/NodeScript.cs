using UnityEngine;

public class NodeScript : NodeParent
{
    private RenderingLine RL;
    private void Awake()
    {
        isVisited = false;
        RL = FindObjectOfType<RenderingLine>();
    }
    private void OnMouseEnter()
    {
        if (!isVisited)
        {
            RL.oneStrechNodesVisited.Add(gameObject.GetComponent<NodeParent>());
            if (RL.IsLinePresent()) RL.drawLine(new Vector3(transform.position.x, transform.position.y, -0.5f));
        }
    }
}

