using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    
    public void DeleteWayPoint()
    {
        Object.Destroy(this.gameObject);
    }
}
