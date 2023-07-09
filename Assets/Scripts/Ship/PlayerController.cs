using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private HUDController _hudController;
    [SerializeField] private float _durability = 100f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 1000.0f;

    private Rigidbody2D _body;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _screenBounds;
    private float _shipWidth;
    private float _shipHeight;
    private bool _invulnerable;

    public void TakeDamage(float damage)
    {
        if (_invulnerable)
            return;

        StartCoroutine(nameof(BlinkAfterDamaged));
        _durability -= damage;
        _hudController.UpdateDurability(_durability);
        if (_durability <= 0f)
            Destroy(gameObject);
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shipWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _shipHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void FixedUpdate()
    {
        ForceMove();
        RotateToMouse();
    }

    private void LateUpdate()
    {
        transform.position = WrappingBorders(transform.position);
    }

    private IEnumerator BlinkAfterDamaged()
    {
        _invulnerable = true;

        for (int i = 0; i < 4; i++)
        {
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;

        _invulnerable = false;
    }

    private void ForceMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector2(horizontal, vertical);
        _body.AddForce(movement * _speed);
    }

    private void RotateToMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirection = mousePosition - transform.position;
        var angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private Vector3 WrappingBorders(Vector3 viewPos)
    {
        // Horizontal wrapping
        var left = _screenBounds.x * -1 - _shipWidth;
        var right = _screenBounds.x + _shipWidth;
        if (viewPos.x < left)
            viewPos.x = right;
        else if (viewPos.x > right)
            viewPos.x = left;

        // Vertical wrapping
        var up = _screenBounds.y * -1 - _shipHeight;
        var down = _screenBounds.y + _shipHeight;
        if (viewPos.y < up)
            viewPos.y = down;
        else if (viewPos.y > down)
            viewPos.y = up;

        return viewPos;
    }
}
