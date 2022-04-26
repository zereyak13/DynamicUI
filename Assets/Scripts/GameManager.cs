using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject midCanvas;
    [SerializeField] private List<GameObject> objPool;

    private List<GameObject> objToSpawn;
    private float areaOfMidCanvas;

    private int objectCount;
    private int objectPerRow;
    private int level;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        level = PlayerPrefs.GetInt("Level");

        Vector2 vec = midCanvas.GetComponent<RectTransform>().sizeDelta;
        areaOfMidCanvas = vec.x * vec.y;

        objToSpawn = new List<GameObject>();
    }
    void Start()
    {
        SetObjectNumberAndDesing();
        SetSpawnList();
        SpawnObject();
        OrderPositionOfObjects();


        //Debug.Log("Area of midCanvas: " + areaOfMidCanvas);
        //Debug.Log("ObjectSize: " + CalculateObjectSize(objectCount));
        //Debug.Log("EmptySpaceSizeX" + CalculateEmptySpaceSizeX(objectPerRow));
        //Debug.Log("EmptySpaceSizeY" + CalculateEmptySpaceSizeY(objectPerRow));
    }

   
    float CalculateObjectSize(int objectCount)
    {
        float objectSize = Mathf.Sqrt(areaOfMidCanvas / (objectCount * 1.8f));

        return objectSize;
    }

    float CalculateEmptySpaceSizeX(int objectCountPerRow)
    {
        int emptySpaceCount = objectCountPerRow + 1;
        float midCanvasWidth = midCanvas.GetComponent<RectTransform>().sizeDelta.x;
        float emptySpaceX = (midCanvasWidth - (objectCountPerRow * CalculateObjectSize(objectCount))) / emptySpaceCount;
        return emptySpaceX;
    }
    float CalculateEmptySpaceSizeY(int objectCountPerRow) //ObjectCountPerColumn = objectCount/objectPerRow ;
    {
        int objectCountPerColumn = objectCount / objectPerRow;
        int emptySpaceCount = objectCountPerColumn + 1;
        float midCanvasHeight = midCanvas.GetComponent<RectTransform>().sizeDelta.y;
        float emptySpaceY = (midCanvasHeight - (objectCountPerColumn * CalculateObjectSize(objectCount))) / emptySpaceCount;
        return emptySpaceY;
    }

    public void SetObjectNumberAndDesing() // objectCount && objectPerRow
    {     
        switch (level)
        {
            case 0:
                objectCount = 4;
                objectPerRow = 2;
                break;
            case 1:
                objectCount = 6;
                objectPerRow = 3;
                break;
            case 2:
                objectCount = 8;
                objectPerRow = 4;
                break;
            case 3:
                objectCount = 12;
                objectPerRow = 4;
                break;
            case 4:
                objectCount = 16;
                objectPerRow = 4;
                break;
            case 5:
                objectCount = 20;
                objectPerRow = 5;
                break;
            case 6:
                objectCount = 24;
                objectPerRow = 6;
                break;
            case 7:
                objectCount = 28;
                objectPerRow = 7;
                break;

        }
    }

    private void SetSpawnList()
    {
        //listey uzunluðunu (objectCount/2)-1 kadar yap
        for (int i = 0; i < objectCount/2; i ++)
        {
            //2 kere ekle
            objToSpawn.Add(objPool[i]);
            objToSpawn.Add(objPool[i]);
        }

    }
    private void SpawnObject()
    {
        for(int i = 0; i < objectCount; i++)
        {
            int rnd = Random.Range(0, objToSpawn.Count);


            GameObject cloneGO =Instantiate(objToSpawn[rnd], Vector3.zero, Quaternion.identity);

            cloneGO.transform.SetParent(midCanvas.transform);
            cloneGO.GetComponent<RectTransform>().localScale = Vector3.one;
            float objectSize = CalculateObjectSize(objectCount);
            cloneGO.GetComponent<RectTransform>().sizeDelta = new Vector2(objectSize, objectSize);
            cloneGO.transform.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(objectSize, objectSize);
            objToSpawn.RemoveAt(rnd);

        }   
    }
    private void OrderPositionOfObjects()
    {
        int index = 1;
        int indexForColumn = 1;
        foreach (Transform t in midCanvas.transform)
        {
            if (index > objectPerRow)
            {
                index = 1;
                indexForColumn ++;
            }
            float vecX = CalculateEmptySpaceSizeX(objectPerRow);
            float vecY = CalculateEmptySpaceSizeY(objectCount / objectPerRow);
            Vector2 vec = new Vector2(vecX*index, vecY * indexForColumn+(CalculateObjectSize(objectCount)*(indexForColumn-1)));
            //Debug.Log("ýndex"+index);
           
            if (index <= objectPerRow)
            {
                //if(index == 1)
                //{
                //    t.gameObject.GetComponent<RectTransform>().anchoredPosition = vec;
                //}
                //else
              
                t.gameObject.GetComponent<RectTransform>().anchoredPosition = vec + new Vector2(CalculateObjectSize(objectCount) * (index - 1), 0);
                index++;
            }
           
        }
    }


    public int GetObjectCount()
    {
        return objectCount;
    }
  
}
