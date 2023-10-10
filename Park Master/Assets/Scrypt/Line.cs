using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManager;
    
    private LineRenderer lineRenderer;
    private List<Vector3> PointsList = new List<Vector3>();
    private Vector3 lastPoint;

    private LayerMask ground;
    private LayerMask stopDraw;


    private float distanceBetweenPoints;
    private float lineWidth;


    private bool isDrawing = false;

    private Material lineColor;

    public void Initialize(GameManager manager)
    {
        gameManager = manager;
        ground = gameManager.groundLayer;
        stopDraw = gameManager.stopDraw;
        lineWidth = gameManager.lineWidth;
        distanceBetweenPoints = gameManager.distanceBetweenPoints;
    }

    public void setLineColor(Material lineColor)
    {
        this.lineColor = lineColor;
        lineRenderer.startColor = lineColor.color;
        lineRenderer.endColor = lineColor.color;
    }

    void Start()
    {
        

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        Initialize(gameManager);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
        //setLineColor(lineColor);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("Finish"))
            {
                Debug.Log("Hit on stop part");
                Invoke("delayStopDrawing",0.03f);
            }
        }


        if (isDrawing)
        {
            updateDrawing();
        }
    }

    private void delayStopDrawing()
    {
        isDrawing = false;
    }

    public void startDrawing()
    {
        lineRenderer.enabled = true;
        
        //clearPath();

        lastPoint = GetMouseWorldPosition();
        isDrawing = true;
    }

    public void updateDrawing()
    {
       
            Vector3 mousePoint = GetMouseWorldPosition();
            if (Vector3.Distance(lastPoint, mousePoint) > distanceBetweenPoints)
            {
                lastPoint = mousePoint;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastPoint);

                PointsList.Add(mousePoint);
            }
    }

    public void stopDrawing()
    {
        isDrawing = false;
    }

    public bool pathExist()
    {
        if (PointsList.Count > 0)
        {
            //Debug.Log("path exist");
            return true;
        }
        return false;
    }

    public List<Vector3> getCarPath()
    {
        return PointsList;
    }

    public void clearPath()
    {
        PointsList.Clear();
        lineRenderer.positionCount = 0;
    }
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            return hit.point;
        }
        //Debug.Log(hit.point);
        return lastPoint;
    }
}
