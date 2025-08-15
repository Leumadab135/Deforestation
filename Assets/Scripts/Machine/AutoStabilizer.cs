using UnityEngine;

public class AutoStabilizer : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.02f;

    private void Update()
    {
        // Obtener la rotación actual
        Quaternion currentRotation = transform.rotation;

        // Convertir a ángulos de Euler para modificar X y Z
        Vector3 targetEuler = currentRotation.eulerAngles;
        targetEuler.x = 0f;
        targetEuler.z = 0f;

        // Crear la rotación objetivo
        Quaternion targetRotation = Quaternion.Euler(targetEuler);

        // Suavizar la rotación hacia el objetivo
        transform.rotation = Quaternion.RotateTowards(
            currentRotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime * 100f
        );
    }
}

