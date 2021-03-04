using UnityEngine;

public class StartingNode : NodeParent
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
            Debug.Log("Entered");
            if (Input.GetMouseButton(0))
            {
                RL.oneStrechNodesVisited.Add(gameObject.GetComponent<NodeParent>());
                RL.createNewLine(new Vector3(transform.position.x, transform.position.y, -0.5f));
                RL.AssignStartingNodeTag(gameObject.tag);
            }

        }
    }
}

