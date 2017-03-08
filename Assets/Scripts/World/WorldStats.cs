using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldStats : MonoBehaviour {


    GameObject selectedObject = null;
    RaycastHit hitInfo = new RaycastHit();
    public int worldHeight = 100;
    public int worldWidth = 100;
    GenWorld world;
    bool worldMade = false;
    static bool load = false;
    bool spawningCharacter;
    bool spawningTree;
    bool spawningStockpile;
    public GameObject character;
    public GameObject tree;
    public GameObject pile;
    // Use this for initialization
    void Start () {
        worldMade = false;
    }
      
    public void spawnChar()
    {
        spawningCharacter = true;
    }

    public void spawntree()
    {
        spawningTree = true;
    }

    public void spawnPile()
    {
        spawningStockpile = true;
    }

    // Update is called once per frame
    void Update () {
        checkSelection();

    }

    public void generateNewLand()
    {
        if (worldMade == false)
        {
            world = new GenWorld(worldWidth, worldHeight);
            worldMade = true;
        }
    }

    public void createNewWorld()
    {
        load = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadWorld(string x)
    {
        load = true;
        SceneManager.LoadScene(x);
    }

    public void saveWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void checkSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, 0);
            float dist;
            hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (!plane.Raycast(ray, out dist))
            {
                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "Character")
                    {
                        Debug.Log("Character selected!");
                        selectedObject = hitInfo.transform.gameObject;
                    }
                    else if (hitInfo.transform.gameObject.tag == "Object")
                    {
                        Debug.Log("Object selected!");
                        selectedObject = hitInfo.transform.gameObject;
                    }
                }
                else
                {
                    if (spawningCharacter)
                    {
                        Vector3 point = ray.GetPoint(dist);
                        Instantiate(character, new Vector3(point.x,point.y,99), gameObject.transform.rotation);
                        spawningCharacter = false;
                    }
                    if (spawningTree)
                    {
                        Vector3 point = ray.GetPoint(dist);
                        Instantiate(tree, new Vector3(point.x, point.y, 99), gameObject.transform.rotation);
                        spawningTree = false;
                    }
                    if (spawningStockpile)
                    {
                        Vector3 point = ray.GetPoint(dist);
                        Instantiate(pile, new Vector3(point.x, point.y, 99), gameObject.transform.rotation);
                        spawningStockpile = false;
                    }
                    Debug.Log("Nothing hit");
                }
            }
        }

    }



}
