using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotString : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float minLaunchForce = 5f;
    [SerializeField] float maxLaunchForce = 20f;
    [SerializeField] float maxDragDistance = 200f;
    private bool isDragging = false;
    private bool hasLaunched = false;

    [Header("Components")]
    [SerializeField] LineRenderer trackRenderer;
    private Vector2 startPoint;
    private Rigidbody ballRigidbody;

    [SerializeField] gameManagement _gameManagement;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        trackRenderer.positionCount = 2;
        trackRenderer.enabled = false;

    }


    private void Update()
    {
        if (!hasLaunched)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = Input.mousePosition;
                isDragging = true;
                trackRenderer.enabled = true;
            }
            else if (Input.GetMouseButton(0))
            {
                if (isDragging)
                {
                    Vector2 endPoint = Input.mousePosition;
                    float dragDistance = Vector2.Distance(startPoint, endPoint);
                    float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, dragDistance / maxDragDistance);
                    Vector2 swipeDirection = startPoint - endPoint;
                    UpdateTrackVisualization(swipeDirection.normalized, launchForce);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    Vector2 endPoint = Input.mousePosition;
                    float dragDistance = Vector2.Distance(startPoint, endPoint);
                    float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, dragDistance / maxDragDistance);
                    Vector2 swipeDirection = startPoint - endPoint;
                    LaunchBall(swipeDirection.normalized, launchForce);
                    trackRenderer.enabled = false;
                    
                }
                isDragging = false;
            }
        }
    }

    private void UpdateTrackVisualization(Vector2 direction, float launchForce)
    {
        Vector3 launchDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        Vector3 ballPosition = transform.position;
        float timeStep = 0.1f;
        float gravity = Physics.gravity.magnitude;
        trackRenderer.positionCount = Mathf.CeilToInt(launchForce * launchForce / (gravity * timeStep * timeStep)) + 1;

        for (int i = 0; i < trackRenderer.positionCount; i++)
        {
            float t = i * timeStep;
            Vector3 displacement = launchDirection * launchForce * t + 0.5f * Physics.gravity * t * t;
            trackRenderer.SetPosition(i, ballPosition + displacement);
        }
    }

    private void LaunchBall(Vector2 direction, float launchForce)
    {
        Vector3 launchDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("firstGround"))
        {
            hasLaunched = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("firstGround"))
        {
            hasLaunched = true;
            _gameManagement.ballCounter();
        }
    }
}