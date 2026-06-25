using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RacePositionManager : MonoBehaviour
{
    public Transform player;
    public Transform ai;

    public TMP_Text p1Text;
    public TMP_Text p2Text;

    public Image p1DriverImage;
    public Image p2DriverImage;

    public Sprite playerDriverSprite;
    public Sprite aiDriverSprite;

    public Transform waypointParent;

    private Transform[] waypoints;

    private void Start()
    {
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
            p1Text.text = "P1";
            p2Text.text = "P2";

            p1DriverImage.sprite = playerDriverSprite;
            p2DriverImage.sprite = aiDriverSprite;
        }
        else
        {
            p1Text.text = "P1";
            p2Text.text = "P2";

            p1DriverImage.sprite = aiDriverSprite;
            p2DriverImage.sprite = playerDriverSprite;
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