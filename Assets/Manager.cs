using UnityEngine;
using UnityEditor;
using System.IO;
using SimpleJSON;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    private UIManager uiManager;
    string path;
    public int noOfVisitedNodes;
    public bool isWon;
    [SerializeField] private TextAsset nodeJson;
    private InputScript inputScript;
    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        inputScript = FindObjectOfType<InputScript>();
        //path = Application.persistentDataPath + "/node.json";
        path = Path.Combine(Application.streamingAssetsPath, "node.json");
        Debug.Log(path);
        //Debug.Log(AssetDatabase.GetAssetPath(nodeJson));
        //Debug.Log("DP" + Application.dataPath);
        load();
    }
    void load()
    {
        string jsonString = File.ReadAllText(path);
        //string json = File.ReadAllText(nodeJson.text); 
        //string jsonString = JsonUtility.FromJson<string>(nodeJson.text);
        //string jsonString = File.ReadAllText(nodeJson.ToString(), js);
        JSONObject nodeData = (JSONObject)JSON.Parse(jsonString);

        for (int i = 0; i < nodeData[SceneManager.GetActiveScene().name].Count; i++)
        {
            GameObject go = GameObject.Find(nodeData[SceneManager.GetActiveScene().name].AsArray[i].AsArray[0]);
            Color myColor = new Color();
            ColorUtility.TryParseHtmlString(nodeData[SceneManager.GetActiveScene().name].AsArray[i].AsArray[1], out myColor);
            go.GetComponent<SpriteRenderer>().color = myColor;

            if (nodeData[SceneManager.GetActiveScene().name].AsArray[i].AsArray[2] == true)
            {
                go.GetComponent<cell>().isKeyCell = true;
            }
        }
    }
    private void Update()
    {
        if (noOfVisitedNodes == 25)
        {
            isWon = true;
            uiManager.showWinScreen();
        }
        else if (inputScript.noOfVisitedKeyCells == 5 && noOfVisitedNodes < 25)
        {
            uiManager.showRestartPanel();
        }
    }
}
