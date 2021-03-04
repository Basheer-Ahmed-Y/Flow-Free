using UnityEngine;
using System.Collections.Generic;
public class RenderingLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 mousePos;
    private string startingTag;
    private string endingTag;
    private int noOfLines;
    private List<int> oneStrechLineList;
    public List<NodeParent> oneStrechNodesVisited;

    [SerializeField] private Material material;

    private void Awake()
    {
        oneStrechLineList = new List<int>();
        oneStrechNodesVisited = new List<NodeParent>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //if (lineRenderer == null)
        //    //{
        //    //    createNewLine(mousePos);
        //    //}

        //    //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //mousePos.z = -0.5f;
        //    //lineRenderer.SetPosition(0, mousePos);
        //    //lineRenderer.SetPosition(1, mousePos);
        //}

        if (Input.GetMouseButton(0) && lineRenderer)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -0.5f;
            lineRenderer.SetPosition(1, mousePos);
        }
    }

    public void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0) && lineRenderer)
        {
            clearOneStrechLineList();
        }
    }

    public void createNewLine(Vector3 Pos)
    {
        lineRenderer = new GameObject("Line " + noOfLines).AddComponent<LineRenderer>();
        oneStrechLineList.Add(noOfLines);
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.numCapVertices = 50;

        lineRenderer.SetPosition(0, Pos);
        lineRenderer.SetPosition(1, Pos);
    }

    public void drawLine(Vector3 NodePos)
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = -0.5f;
        lineRenderer.SetPosition(1, NodePos);
        lineRenderer = null;
        noOfLines++;
        createNewLine(NodePos);
        //lineRenderer.SetPosition(0, NodePos);
        //lineRenderer.SetPosition(1, NodePos);
    }

    public void endLineAtDestination(Vector3 endPos, string endingTag)
    {
        if (startingTag == endingTag)
        {
            Debug.Log("MAtch");
            lineRenderer.SetPosition(1, endPos);
            lineRenderer = null;
            noOfLines++;
            markAsVisited();
            clearOneStrechNodesVisited();
        }
        else { clearOneStrechLineList(); clearOneStrechNodesVisited();  }
    }
    public void AssignStartingNodeTag(string Tag)
    {
        startingTag = Tag;
        Debug.Log("Starting Tag : " + startingTag);
    }
    public bool IsLinePresent()
    {
        if (lineRenderer != null) return true;
        else return false;
    }

    void clearOneStrechLineList()
    {
        lineRenderer = null;
        foreach(int i in oneStrechLineList)
        {
            Destroy(GameObject.Find("Line " + i));
        }
        oneStrechLineList.Clear();
    }

    void markAsVisited()
    {
        foreach (NodeParent nodes in oneStrechNodesVisited)
        {
            nodes.isVisited = true;
        }
    }

    void clearOneStrechNodesVisited()
    {
        oneStrechNodesVisited.Clear();
    }
    //public string getStartingTag()
    //{
    //    return startingTag;
    //}

    //public string getEndingTag()
    //{
    //    return endingTag;
    //}

}

