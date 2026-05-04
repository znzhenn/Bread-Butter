using System.Collections;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindFirstObjectByType<InventoryController>();

        //yield return null;

        LoadGame();

        // SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        // Debug.Log(File.ReadAllText(saveLocation));
        // Debug.Log("Loaded inventory count: " + saveData.inventorySaveData.Count);
        
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.name,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
            
            // if(saveData.inventorySaveData != null && saveData.inventorySaveData.Count > 0)
            // {
            //     inventoryController.SetInventoryItems(saveData.inventorySaveData);
            // }
            // else
            // {
            //     Debug.Log("Save file has no inventory data → skipping load");
            // }
        }
        else
        {
            SaveGame();
        }
    }
}
