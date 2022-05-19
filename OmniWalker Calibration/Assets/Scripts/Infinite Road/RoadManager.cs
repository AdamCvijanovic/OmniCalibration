using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{


    public static RoadManager SharedInstance;

    public BuildingManager buildingManager;

    //object pool
    public List<GameObject> _pooledObjects;
    public GameObject _objectToPool;
    public int amountToPool;
    public float segmentOffset;
    public int roadPosition;

    //Navmesh
    NavigationBaker _navBaker;
    public GameObject NavCube;

    //Physics
    public BoxCollider advanceCollider;
    public BoxCollider EndBlock;



    void Awake()
    {
        SharedInstance = this;
        _navBaker = FindObjectOfType<NavigationBaker>();
    }
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        InstantiateRoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateRoads()
    {
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = _pooledObjects.Count; i < amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool);
            //_navBaker.AddSurface(tmp);
            //tmp.SetActive(false);
            _pooledObjects.Insert(0, tmp);

            if (tmp != null)
            {
                tmp.transform.parent = this.transform;
                tmp.transform.position = this.transform.position;
                tmp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + segmentOffset * roadPosition);
                tmp.transform.rotation = this.transform.rotation;
                roadPosition++;

            }

            //we only deactivate half the roads
            if(i > amountToPool / 2)
            {
                //tmp.SetActive(false);
            }

        }
    }

    public GameObject GetPooledObject()
    {
        //If an object in the pool is not currently active we retrienve it
        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }

    public void IncrementRoad(GameObject playerObj, GameObject roadObj)
    {
        //Increment buildings too
        buildingManager.UpdateBuildings();

        float minDst = 70;
        float dst = Vector3.Distance(playerObj.transform.position, FarthestSegment().transform.position);


        if(dst > minDst)
        {
            RetractSegment(playerObj);
            AdvanceSegment(playerObj);
        }
        
        
    }

    //depreciated
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("BoundaryCrossed");

            //RetractSegment();
            //AdvanceSegment();
        }
    }

    public void RetractSegment(GameObject player)
    {


        GameObject tmp = FarthestSegment();
        //tmp.SetActive(false);

        RoadSegment roadSegment = tmp.GetComponent<RoadSegment>();
        roadSegment.SetTransform();

        //advanceCollider.center = new Vector3(advanceCollider.center.x, advanceCollider.center.y, advanceCollider.center.z + (segmentOffset));
        //_pooledObjects.Remove(tmp);
        //_pooledObjects.Insert(0, tmp);

        //roadPosition++;
        //tmp.GetComponent<RoadSegment>().DeActivateSegment();

        GameObject newEnd = FarthestSegment();
        EndBlock.transform.position = newEnd.transform.position;
        AdvanceNavmesh(player);


    }

    public void AdvanceSegment(GameObject player)
    {
        GameObject tmp = GetPooledObject();
        if (tmp != null)
        {
            RoadSegment roadSegment = tmp.GetComponent<RoadSegment>();
            roadSegment.ActivateSegment();
            roadPosition++;
            roadSegment.SetTransform();
            //advanceCollider.center = new Vector3(advanceCollider.center.x,advanceCollider.center.y , advanceCollider.center.z + (segmentOffset));
            _pooledObjects.Insert(0, tmp);

            AdvanceNavmesh(player);
        }
        else
        {
            //Debug.Log("Should be old segment here");
        }
       
    }

    public GameObject FarthestSegment()
    {
        float lowestZ = _pooledObjects[0].transform.position.z;
        GameObject tmp = _pooledObjects[0];

        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (_pooledObjects[i].transform.position.z <= lowestZ)
            {
                lowestZ = _pooledObjects[i].transform.position.z;
                tmp = _pooledObjects[i];
            }
        }

        return tmp;
    }

    public void AdvanceNavmesh(GameObject player)
    {
        NavCube.transform.position = new Vector3(NavCube.transform.position.x, NavCube.transform.position.y, player.transform.position.z);

        _navBaker.UpdateMesh();

    }
}
