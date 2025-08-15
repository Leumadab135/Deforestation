using UnityEngine;

public class CameraCursorFollow : MonoBehaviour
{
    [Header("Rotación")]
    [SerializeField] private float _rotationAmount = 2f;    // Grados máximos de inclinación
    [SerializeField] private float _smoothSpeed = 5f;

    private Quaternion _initialRotation;

    void Start()
    {
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Diferencia del cursor desde el centro (-1 a 1)
        Vector2 offsetFromCenter = (mousePos - screenCenter) / screenCenter;
        offsetFromCenter = Vector2.ClampMagnitude(offsetFromCenter, 1f);

        // Calcular rotación objetivo
        Quaternion targetRotation = Quaternion.Euler(
            -offsetFromCenter.y * _rotationAmount,
            offsetFromCenter.x * _rotationAmount,
            0f
        );

        // Aplicar rotación suavizada desde la rotación inicial
        transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation * targetRotation, Time.deltaTime * _smoothSpeed);
    }
}

