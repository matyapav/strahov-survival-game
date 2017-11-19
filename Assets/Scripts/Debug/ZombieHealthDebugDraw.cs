using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draw the gizmos above zombie for debug purposes.
/// </summary>
[RequireComponent(typeof(ZombieHealth))]
[RequireComponent(typeof(CapsuleCollider))]
public class ZombieHealthDebugDraw : MonoBehaviour {

    private ZombieHealth zombieHealth;

    [Range(0, 20f), SerializeField]
    private float DeltaY = 15f;

    [SerializeField]
    private float MaxLineLenght = 10f;

    private void Start()
    {
        zombieHealth = GetComponent<ZombieHealth>();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) {
			Vector3 left, right;
			float y = transform.position.y + DeltaY;

            left = transform.position - Vector3.right * MaxLineLenght / 2f;
			right = left +  Vector3.right * MaxLineLenght * zombieHealth.health / zombieHealth.GetStartingHealth();
			
			left.y = y;
			right.y = y;

            Color prevColor = Gizmos.color;

            Gizmos.color = Color.red;
			Gizmos.DrawLine(left, right);
            Gizmos.color = prevColor;
        }
    }
}
