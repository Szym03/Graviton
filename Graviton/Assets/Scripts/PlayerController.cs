using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float _thrustForce = 5f;
    [SerializeField] private float _maxFuel = 80f;
    [SerializeField] private float fuelConsumptionRate = 12f; // fuel per second when thrusting
    [SerializeField] private Image _fuelBar;

    private float _currentFuel;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentFuel = _maxFuel;
    }

    void FixedUpdate()
    {
        ApplyGravityWells();
        HandleInput();
    }

    void Update()
    {
        _fuelBar.fillAmount = _currentFuel / _maxFuel;
    }

    void ApplyGravityWells()
    {
        GravityWell[] wells = FindObjectsByType<GravityWell>(FindObjectsSortMode.None);
        foreach (var well in wells)
        {
            // ddistance from the center of the well
            Vector2 direction = (Vector2)well.transform.position - _rb.position;
            float distance = direction.magnitude;

            // pass if out of SOI
            if (distance > well.radius) continue;

            // pass to avoid unstable behavior
            if (distance < 0.1f) continue;

            float strength = well.gravityStrength;

            // strength decreases with the square of the distance
            strength /= distance * distance;

            // clamp to prevent infinite force at center of wells
            strength = Mathf.Clamp(strength, 0f, 50f);

            // apply the force vector
            Vector2 force = direction.normalized * strength;
            _rb.AddForce(force);
        }
    }

    void HandleInput()
    {

        // 'asteroid' style control
        //  thrust is up and down, and diretion is left and right

        float thrustInput = Input.GetAxis("Vertical");
        float rotateInput = -Input.GetAxis("Horizontal");
        float rotationSpeed = 180f;

        // prevent reverse thrust
        if (thrustInput < 0f)
        {
            thrustInput = 0f;
        }


        _rb.rotation += rotateInput * rotationSpeed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.UpArrow) && _currentFuel > 0f)
        {
            _rb.AddForce(thrustInput * _thrustForce * transform.up);
            _currentFuel -= fuelConsumptionRate * Time.deltaTime;

            // clamp so it never goes below 0
            _currentFuel = Mathf.Max(_currentFuel, 0f);
        }

    }
}
