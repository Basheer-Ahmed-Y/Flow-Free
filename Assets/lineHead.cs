using UnityEngine;

public class lineHead : MonoBehaviour
{
    private InputScript inputScript;

    private void Awake()
    {
        inputScript = FindObjectOfType<InputScript>();
    }
    private void Update()
    {
        transform.position = new Vector3(inputScript.getLineRendererPos().x, inputScript.getLineRendererPos().y, 0f);
    }
}
