using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public List<GameObject> _buildingPrefabs;
    public List<GameObject> _buildingListLeft;
    public List<GameObject> _buildingListRight;

    public Transform _buildingChainLeft;
    public Transform _buildingChainRight;

    public Transform _buildingFrontierLeft;
    public Transform _buildingFrontierRight;

    public GameObject _player;

    public float _spawnDst;
    public float _deSpawnDst;

    MeshCollider col;


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerManager>().gameObject;
        SpawnBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        //col.bounds.extents.z;
    }


    public void SpawnBuildings()
    {
        SpawnLeft();
        SpawnRight();
    }

    public void SpawnLeft()
    {
        for (int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, _buildingPrefabs.Count);
            GameObject newBuilding = Instantiate<GameObject>(_buildingPrefabs[index], _buildingChainLeft);
            _buildingListLeft.Add(newBuilding);

            float length = newBuilding.GetComponent<MeshCollider>().bounds.extents.z;
            float width = newBuilding.GetComponent<MeshCollider>().bounds.extents.x;
            newBuilding.transform.position = new Vector3(_buildingFrontierLeft.position.x - (1.15f * width), _buildingFrontierLeft.position.y, _buildingFrontierLeft.position.z + length);
            _buildingFrontierLeft.position = new Vector3(_buildingFrontierLeft.position.x, _buildingFrontierLeft.position.y, _buildingFrontierLeft.position.z + (2 * length));

        }
    }

    public void SpawnRight()
    {
        for (int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, _buildingPrefabs.Count);
            GameObject newBuilding = Instantiate<GameObject>(_buildingPrefabs[index], _buildingChainRight);
            _buildingListRight.Add(newBuilding);

            float length = newBuilding.GetComponent<MeshCollider>().bounds.extents.z;
            float width = newBuilding.GetComponent<MeshCollider>().bounds.extents.x;
            newBuilding.transform.position = new Vector3(_buildingFrontierRight.position.x + (1.15f * width), _buildingFrontierRight.position.y, _buildingFrontierRight.position.z + length);
            _buildingFrontierRight.position = new Vector3(_buildingFrontierRight.position.x, _buildingFrontierRight.position.y, _buildingFrontierRight.position.z + (2 * length));

        }
    }

    public void UpdateBuildings()
    {

        //List<GameObject> temp = new List<GameObject>();
        DestroyOld();

        //create new
        CreateNew();


    }

    public void DestroyOld()
    {
        for (int i = 0; i < _buildingListLeft.Count; i++)
        {
            if (Vector3.Distance(_buildingListLeft[i].transform.position, _player.transform.position) > _deSpawnDst)
            {
                Destroy(_buildingListLeft[i]);
                _buildingListLeft.Remove(_buildingListLeft[i]);
            }
        }
    }

    public void CreateNew()
    {

        Debug.Log("DISANTCE == " + Vector3.Distance(_buildingFrontierLeft.transform.position, _player.transform.position));

        while(Vector3.Distance(_buildingFrontierLeft.transform.position, _player.transform.position) < _spawnDst)
        {
            AddToLeft();
        }
    }

    public void AddToLeft()
    {
        int index = Random.Range(0, _buildingPrefabs.Count);
        GameObject newBuilding = Instantiate<GameObject>(_buildingPrefabs[index], _buildingChainLeft);
        _buildingListLeft.Add(newBuilding);

        float length = newBuilding.GetComponent<MeshCollider>().bounds.extents.z;
        float width = newBuilding.GetComponent<MeshCollider>().bounds.extents.x;
        newBuilding.transform.position = new Vector3(_buildingFrontierLeft.position.x - (1.15f * width), _buildingFrontierLeft.position.y, _buildingFrontierLeft.position.z + length);
        _buildingFrontierLeft.position = new Vector3(_buildingFrontierLeft.position.x, _buildingFrontierLeft.position.y, _buildingFrontierLeft.position.z + (2 * length));
    }





}
