using UnityEngine;
using System.Collections.Generic;
public class InputScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 v3Pos;
    private Vector3 eachLineOrgin;
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineHead;
    [SerializeField] private Material material;
    private Color startingNodeColor;
    public string presentTag;
    public int presentNodeNumber;
    //public Vector3 previousPoint;
    [SerializeField] public List<LineRenderer> linesList;
    public List<cell> cellsList;
    public int noOfVisitedKeyCells;
    private Manager manager;
    public IDictionary<cell, Color> cellsDict; 
    public enum movementDirections
    {
        up,
        down,
        left,
        right
    }



    public movementDirections movementDirection;
    //[SerializeField] private GameObject lineHead;

    private void Awake()
    {

        
    }
    private void Start()
    {
        cellsDict = new Dictionary<cell, Color>();
        manager = FindObjectOfType<Manager>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && lineRenderer)
        {
            renderLine();
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach(LineRenderer line in linesList)
            {
                Destroy(line.gameObject);
            }
            foreach (cell cell in cellsDict.Keys)
            {
                cell.GetComponent<SpriteRenderer>().color = cellsDict[cell];
                cell.isVisited = false;
            }
            //foreach (cell cell in cellsList)
            //{
            //    cell.GetComponent<SpriteRenderer>().color = Color.white;
            //    cell.isVisited = false;
            //}
            cellsDict.Clear();
            //cellsList.Clear();
            linesList.Clear();
        }
    }

    public void createNewLine(GameObject go)
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        eachLineOrgin = go.transform.position;
        presentTag = go.tag;
        presentNodeNumber = int.Parse(go.name);
        Debug.Log("Present Node Numbber : " + presentNodeNumber);
        linesList.Add(lineRenderer);
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.numCapVertices = 50;

        lineRenderer.SetPosition(0, go.transform.position);
        lineRenderer.SetPosition(1, go.transform.position);
        //Instantiate(lineHead, Pos, Quaternion.identity);
    }

    void renderLine()
    {
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        float f;
        v3Pos = mousePos - eachLineOrgin;
        v3Pos.Normalize();
        f = Vector3.Dot(v3Pos, Vector3.up);


        if (f >= 0.5 && mousePos.y<2f)
        {
            movementDirection = movementDirections.up;
            lineRenderer.SetPosition(1, new Vector3(eachLineOrgin.x, mousePos.y, 0f));
        }
        else if (f <= -0.5 && mousePos.y <2f)
        {
            movementDirection = movementDirections.down;
            lineRenderer.SetPosition(1, new Vector3(eachLineOrgin.x, mousePos.y, 0f));
        }
        else if(mousePos.x < 2f)
        {
            f = Vector3.Dot(v3Pos, Vector3.right);

            if (f >= 0.5)
            {
                movementDirection = movementDirections.right;
                lineRenderer.SetPosition(1, new Vector3(mousePos.x, eachLineOrgin.y, 0f));
            }
            else if (f <= -0.5)
            {
                movementDirection = movementDirections.left;
                lineRenderer.SetPosition(1, new Vector3(mousePos.x, eachLineOrgin.y, 0f));
            }
        }
    }
    public bool IsLinePresent()
    {
        if (lineRenderer != null) return true;
        else return false;
    }

    public Vector3 getLineRendererPos()
    {
        if (lineRenderer != null) return lineRenderer.GetPosition(1);
        else return Vector3.zero;
    }
    public void drawLine(GameObject Node)
    {
        Debug.Log("NextNode number :" + int.Parse(Node.name));
        lineRenderer.SetPosition(1, Node.transform.position);
        lineRenderer = null;
        //previousPoint = NodePos;
        //Debug.Log("Previous Point = " + previousPoint);
        createNewLine(Node);
    }

    public void setStartingNodeColor(Color color)
    {
        startingNodeColor = color;
    }

    public bool checkColorMatch(Color color)
    {
        if (startingNodeColor == color) return true; else return false;
    }
    public void endLineAtDestination(GameObject cell)
    {
        Debug.Log("MAtch");
        manager.noOfVisitedNodes += cellsDict.Count;
        cellsDict.Clear();
        linesList.Clear();
        lineRenderer.SetPosition(1, cell.transform.position);
        lineRenderer = null;
        noOfVisitedKeyCells++;
    }

    public bool isAdjacentNode(GameObject node)
    {
        int nodenum = int.Parse(node.name);
        if (Mathf.Abs(nodenum - presentNodeNumber) == 5 || Mathf.Abs(nodenum - presentNodeNumber) == 1) return true;
        else return false;
    }


    //public bool isAdjacentNode(Vector3 pos)
    //{
    //    if (previousPoint == null)
    //    {
    //        return true;
    //    }
    //    //else if (Mathf.Abs(pos.x - previousPoint.x) == 1.15f || Mathf.Abs(pos.y - previousPoint.y) == 1.15f)
    //    //{
    //    //    return true;
    //    //}

    //    else if (movementDirection == movementDirections.left || movementDirection == movementDirections.right)
    //    {
    //        if (Vector3.Angle(previousPoint, pos) == 0f || Vector3.Angle(previousPoint, pos) == 180f)
    //        {
    //            Debug.Log("Angle btw " + Vector3.Angle(previousPoint, pos));
    //            return true;
    //        }
    //        return false;
    //    }

    //    else if (movementDirection == movementDirections.up || movementDirection == movementDirections.down)
    //    {
    //        if (Vector3.Angle(previousPoint, pos) == 90f || Vector3.Angle(previousPoint, pos) == 270f)
    //        {
    //            Debug.Log("Angle btw " + Vector3.Angle(previousPoint, pos));
    //            return true;
    //        }
    //        return false;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
}
