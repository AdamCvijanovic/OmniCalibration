using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{


    public static RoadManager SharedInstance;



    //object pool
    public List<GameObject> _pooledObjects;
    public GameObject _objectToPool;
    public int amountToPool;
    public float segmentOffset;
    public int roadPosition;


    //Physics
    public BoxCollider advanceCollider;


    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InstatiateRoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstatiateRoads()
    {
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = _pooledObjects.Count; i < amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("BoundaryCrossed");

            RetractSegment();
            //AdvanceSegment();
            
        }
    }

    public void RetractSegment()
    {

        float lowestZ = _pooledObjects[_pooledObjects.Count-1].transform.position.z;
        GameObject tmp = _pooledObjects[_pooledObjects.Count-1];

        for (int i = 0; i < amountToPool; i++)
        {
            if(_pooledObjects[i].transform.position.z < lowestZ)
            {
                lowestZ = _pooledObjects[i].transform.position.z;
                tmp = _pooledObjects[i];
            }
        }
        //tmp.SetActive(false);

        RoadSegment roadSegment = tmp.GetComponent<RoadSegment>();
        roadSegment.SetTransform();

        //advanceCollider.center = new Vector3(advanceCollider.center.x, advanceCollider.center.y, advanceCollider.center.z + (segmentOffset));
        _pooledObjects.Remove(tmp);
        _pooledObjects.Insert(0, tmp);

        roadPosition++;
        //tmp.GetComponent<RoadSegment>().DeActivateSegment();

    }

    public void AdvanceSegment()
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
        }
        else
        {
            Debug.Log("Should be old segment here");
        }
       
    }

    //public GameObject FarthestSegment()
    //{

    //}
}
