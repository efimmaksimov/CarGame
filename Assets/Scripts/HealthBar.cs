using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    private MaterialPropertyBlock matBlock;
    private MeshRenderer meshRenderer;
    private Camera mainCamera;
    private Damageable damageable;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        damageable = GetComponentInParent<Damageable>();
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (damageable.CurrentHealth < damageable.maxHealth) {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else {
            meshRenderer.enabled = false;
        }
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", damageable.CurrentHealth / (float)damageable.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

}