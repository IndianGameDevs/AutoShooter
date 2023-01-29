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

    public Transform Target;
    private Collider[] enemiesInRange = new Collider[15];
    private List<Transform> enemySpotList = new List<Transform>();
    private int cachedFoundEnemies = -1;
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

        if (foundEnemies != cachedFoundEnemies)
        {
            Debug.Log($"<color=yellow>Total Objects for that layer in the Radius : </color> {foundEnemies}");
            cachedFoundEnemies = foundEnemies;
        }
        UpdateEnemiesOfSight();
    }

    private void UpdateEnemiesOfSight()
    {
        enemySpotList.Clear();

        Vector3 direction;

        for (int i = 0; i < foundEnemies; i++)
        {
            foreach (Collider collider in enemiesInRange)
            {
                if (collider == null) break;
                if (collider.TryGetComponent<TargetPoint>(out TargetPoint hitCollider))
                {
                    foreach (Transform t in hitCollider.HitPoints)
                    {
                        Vector3 distance = t.position - sensorSight.position;
                        direction = distance.normalized;
                        Ray ray = new Ray(sensorSight.position, direction);

                        if (Physics.Raycast(ray, out RaycastHit raycastHit))
                        {
                            if (raycastHit.collider.Equals(collider))
                            {
                                float angle = Vector3.Angle(direction, transform.forward);
                                if (angle <= InSightRadiusArc)
                                {
                                    enemySpotList.Add(t);
                                    break;
                                }
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
        Transform closestTarget = enemySpotList[0];
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

        foreach (Transform t in enemySpotList)
        {
            if (t != Target)
                Gizmos.DrawWireSphere(t.position, 1f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sensorSight.position, sensorRadius);

        if (Target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Target.transform.position, .2f);
        }

        Gizmos.color = Color.yellow;
        Method.DrawWireArc(transform.position, transform.forward, InSightRadiusArc * 2, sensorRadius);
    }
}
