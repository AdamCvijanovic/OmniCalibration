using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    public RoadManager roadManager;


    public BoxCollider advanceCollider;

    // Start is called before the first frame update
    private void Awake()
    {
        roadManager = FindObjectOfType<RoadManager>();
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTransform()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + roadManager.segmentOffset * roadManager.roadPosition); ;
    }

    public void ActivateSegment()
    {
        gameObject.SetActive(true);

    }

    public void DeActivateSegment()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("BoundaryCrossed");

            roadManager.IncrementRoad(other.gameObject, this.gameObject);
            //AdvanceSegment();

        }
    }
}
