using UnityEngine;

public class cell : MonoBehaviour
{

    private InputScript inputScript;
    [SerializeField] public bool isKeyCell;
    [SerializeField] public bool isVisited;
    [SerializeField]bool isHeadIn;
    private void Awake()
    {
        inputScript = FindObjectOfType<InputScript>();
    }
    private void OnMouseOver()
    {
        if (isKeyCell && !isVisited)
        {
            //Debug.Log(inputScript.checkColorMatch(gameObject.GetComponent<SpriteRenderer>().color));
            if (Input.GetMouseButtonDown(0) )
            {
                isVisited = true;
                Debug.Log("Mouse Over on " + gameObject.name);
                inputScript.setStartingNodeColor(gameObject.GetComponent<SpriteRenderer>().color);
                inputScript.createNewLine(this.gameObject);
                inputScript.cellsDict.Add(this, GetComponent<SpriteRenderer>().color);
                //inputScript.cellsList.Add(this);
            }
            else if (inputScript.IsLinePresent() && inputScript.checkColorMatch(gameObject.GetComponent<SpriteRenderer>().color) && inputScript.isAdjacentNode(this.gameObject))
            {
                isVisited = true;
                //inputScript.cellsList.Add(this);
                inputScript.cellsDict.Add(this, GetComponent<SpriteRenderer>().color);
                inputScript.endLineAtDestination(this.gameObject);
            }
            
        }

        else 
        {

            if (Input.GetMouseButton(0) && inputScript.IsLinePresent() && !isVisited && inputScript.isAdjacentNode(this.gameObject))
            {
                inputScript.cellsDict.Add(this, GetComponent<SpriteRenderer>().color);

                isVisited = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                //inputScript.cellsList.Add(this);
                inputScript.drawLine(this.gameObject);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LineHead")
        {
            Debug.Log("Line in");
        }    
    }

}
