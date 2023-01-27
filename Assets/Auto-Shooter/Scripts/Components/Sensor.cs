using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask targetLayers;
    [Space(20)]

    [Header("Sensor Properties")]
    [Range(0, 120)]
    [SerializeField] private float ChecksPerSecond = 60;

    [Range(0, 25)]
    [SerializeField] private float sensorRadius = 5.0f;

    [Range(0.0f, 179.9f)]
    [SerializeField] private float InSightRadiusArc = 60;

    [SerializeField] private Transform sensorSight;

    #region Sensor Variables
    private int foundEnemies;
    private float lastCheckTime;

    public Collider Target;
    private Collider[] enemiesInRange = new Collider[15];
    private List<Collider> enemySpotList = new List<Collider>();
    #endregion

    public void SensorUpdate(float deltaTime)
    {
        if (Time.time > lastCheckTime + (1 / ChecksPerSecond))
        {
            UpdateTargetInRange();
            lastCheckTime = Time.time;
        }
    }

    private void UpdateTargetInRange()
    {
        foundEnemies = Physics.OverlapSphereNonAlloc(transform.position, sensorRadius, enemiesInRange, targetLayers);
        Debug.Log($"<color=yellow>Total Objects for that layer in the Radius : </color> {foundEnemies}");
        UpdateEnemiesOfSight();
    }

    private void UpdateEnemiesOfSight()
    {
        enemySpotList.Clear();

        Vector3 direction;

        for (int i = 0; i < foundEnemies; i++)
        {
            if (enemiesInRange[i].TryGetComponent<TargetPoint>(out TargetPoint hits))
            {
                foreach (Transform sightPoint in hits.HitPoints)
                {
                    Vector3 distance = sightPoint.position - sensorSight.position;
                    direction = distance.normalized;
                    Ray ray = new Ray(sensorSight.position, direction);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.Equals(enemiesInRange[i].gameObject))
                        {
                            float angle = Vector3.Angle(direction, transform.forward);

                            if (angle <= InSightRadiusArc)
                            {
                                Debug.Log($"<color=red>Found <b>{enemiesInRange[i].name} </b>in sight</color>");
                                enemySpotList.Add(enemiesInRange[i]);
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (enemySpotList.Count > 0)
        {
            CheckNearestTarget();
        }
        else
        {
            Target = null;
        }
    }

    private void CheckNearestTarget()
    {
        Collider closestTarget = enemySpotList[0];
        float closestDistance = Vector3.Distance(enemySpotList[0].transform.position, sensorSight.position);

        for (int i = 1; i < enemySpotList.Count; i++)
        {
            float currentDistance = Vector3.Distance(enemySpotList[i].transform.position, sensorSight.position);

            if (currentDistance < closestDistance)
            {
                closestTarget = enemySpotList[i];
                closestDistance = currentDistance;
            }
        }

        Target = closestTarget;
    }

    private void Update()
    {
        SensorUpdate(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Collider collider in enemySpotList)
        {
            if (collider != Target)
                Gizmos.DrawWireSphere(collider.transform.position, 1f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sensorSight.position, sensorRadius);

        if (Target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Target.transform.position, 1f);
        }

        Gizmos.color = Color.yellow;
        Method.DrawWireArc(transform.position, transform.forward, InSightRadiusArc * 2, sensorRadius);
    }
}
