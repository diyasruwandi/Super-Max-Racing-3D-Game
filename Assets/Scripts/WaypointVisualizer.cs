using UnityEngine;

public class WaypointVisualizer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currentWaypoint = transform.GetChild(i);

            Transform nextWaypoint;

            // Kalau waypoint terakhir balik ke awal
            if (i + 1 < transform.childCount)
            {
                nextWaypoint = transform.GetChild(i + 1);
            }
            else
            {
                nextWaypoint = transform.GetChild(0);
            }

            // Gambar garis
            Gizmos.DrawLine(
                currentWaypoint.position,
                nextWaypoint.position
            );

            // Gambar bulatan waypoint
            Gizmos.DrawSphere(currentWaypoint.position, 1f);
        }
    }
}