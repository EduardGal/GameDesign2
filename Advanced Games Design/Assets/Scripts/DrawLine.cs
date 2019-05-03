using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject linePrefab;
    public GameObject currentLine;
    public GameObject player;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> playerPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
        if(player.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
        {
            Vector2 tempPos = player.transform.position;
            CreateLine();
            if (Vector2.Distance(tempPos, playerPositions[playerPositions.Count - 1]) > 1f)
            {
                UpdateLine(tempPos);
            }
                 
        }

    }

    void CreateLine()
    {
        
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLine.transform.SetParent(this.gameObject.transform, false);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        playerPositions.Clear();
        playerPositions.Add(player.transform.position);
        playerPositions.Add(player.transform.position);
        lineRenderer.SetPosition(0, playerPositions[0]);
        lineRenderer.SetPosition(1, playerPositions[1]);
        edgeCollider.points = playerPositions.ToArray();
    }

    void UpdateLine(Vector2 newPos)
    {
        playerPositions.Add(newPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);
        edgeCollider.points = playerPositions.ToArray();

    }
}
