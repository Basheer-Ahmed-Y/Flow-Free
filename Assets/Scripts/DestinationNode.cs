using UnityEngine;

public class DestinationNode : NodeParent
{
    private RenderingLine RL;
    private void Awake()
    {
        isVisited = false;
        RL = FindObjectOfType<RenderingLine>();
    }
    private void OnMouseEnter()
    {
        if (!isVisited && RL.IsLinePresent())
        {
            RL.oneStrechNodesVisited.Add(gameObject.GetComponent<NodeParent>());
            RL.endLineAtDestination(transform.position, gameObject.tag);
        }
    }

}
