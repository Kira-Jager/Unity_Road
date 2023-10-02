using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float speed = 0.1f;
    public float rotationSpeed = 1.0f;
    public float distanceBetweenPoints = 1.0f;
    public LayerMask groundLayer;

    public GameObject carParent;
    public Dictionary<int, List<Vector3>> carPoints = new Dictionary<int, List<Vector3>>();

    private void Start()
    {
        int carID = 0;

        foreach( Transform carTransform  in carParent.transform)
        {
            carTransform.GetComponent<Car>().setCarID(carID);
            //carPoints[carID] = new List<Vector3>();
            carID++;
        }
    }

/*    public void AddPointToCarPath(int carID, Vector3 point)
    {
        if (carPoints.ContainsKey(carID))
        {
            carPoints[carID].Add(point);
        }
    }

    public List<Vector3> getCarPath(int carID)
    {
        //return PointsList;
        return carPoints[carID];
    }
*/

    private void Update()
    {
       
    }
}
