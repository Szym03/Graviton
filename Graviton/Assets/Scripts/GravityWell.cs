using UnityEngine;


[ExecuteAlways]
public class GravityWell : MonoBehaviour
{
    // Pull strength
    [SerializeField] private float _gravityStrength = 10f;
    // Sphere of influence radius
    [SerializeField] private float _radius = 5f;
    private SpriteRenderer _spriteRenderer;

    public float gravityStrength
    {
        get => _gravityStrength;

        set => _gravityStrength = value;
    }

    public float radius
    {
        get => _radius;

        set
        {
            radius = value;
            UpdateVisualScale();
        }
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisualScale();
    }

    private void OnValidate()  
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateVisualScale();
    }

    private void UpdateVisualScale()
    {   
        //return if no sprite
        if (_spriteRenderer == null || _spriteRenderer.sprite == null)
            return;

        // compute diameter in world units
        float diameter = _radius * 2f;

        // sprite’s actual size in world units before scaling
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;

        // scaling the sprite 
        Vector3 newScale = new Vector3(
            diameter / spriteSize.x,
            diameter / spriteSize.y,
            1f
        );

        transform.localScale = newScale;
    }
}
