using UnityEngine;
using TMPro;

public class RacePositionManager : MonoBehaviour
{
    public Transform player;
    public Transform ai;

    public string playerName = "PLAYER";
    public string aiName = "BOT";

    public TMP_Text positionText;

    // Parent waypoint
    public Transform waypointParent;

    private Transform[] waypoints;

    private void Start()
    {
        // Ambil semua child waypoint otomatis
        waypoints = new Transform[waypointParent.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    private void Update()
    {
        int playerWaypoint = GetClosestWaypoint(player);
        int aiWaypoint = GetClosestWaypoint(ai);

        if (playerWaypoint >= aiWaypoint)
        {
            positionText.text =
                "P1 - " + playerName +
                "\nP2 - " + aiName;
        }
        else
        {
            positionText.text =
                "P1 - " + aiName +
                "\nP2 - " + playerName;
        }
    }

    int GetClosestWaypoint(Transform car)
    {
        int closest = 0;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance =
                Vector3.Distance(car.position, waypoints[i].position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = i;
            }
        }

        return closest;
    }
}