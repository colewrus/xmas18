using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMgr : MonoBehaviour
{

    public int xSize;
    public int ySize;
    public GameObject matchObject;
    public Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {

        for(int x=0; x<xSize; x++)
        {
            for(int y=0; y<ySize; y++)
            {
                Instantiate(matchObject, StartPos + new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
