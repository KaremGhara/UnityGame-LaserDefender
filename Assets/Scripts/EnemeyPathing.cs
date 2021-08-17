using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPathing : MonoBehaviour
{
    WaveConfig WaveConfig;
    List<Transform> waypoints;
    int waypointsIndex = 0;
    void Start()
    {
        waypoints = WaveConfig.GetWaypoint();
        transform.position = waypoints[waypointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.WaveConfig = waveConfig;
    }
    private void Move()
    {
        if (waypointsIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointsIndex].transform.position;
            var moveThisFrame = WaveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);

            if (transform.position == targetPosition)
            {
                waypointsIndex++;

            }
          
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
