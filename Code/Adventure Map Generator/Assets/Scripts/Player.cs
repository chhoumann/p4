using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Player : MonoBehaviour
    {
        private const float MoveSpeed = 6f;

        public Vector2 Position
        {
            get => rb.position;
            set => rb.position = value;
        }

        public Vector2 Size => bounds.size;
        public Vector2 Extents => bounds.extents;

        private Rigidbody2D rb;

        private Bounds bounds;
        private Vector2 moveInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            bounds = new Bounds(rb.position, new Vector3(1, 1));
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveInput * (MoveSpeed * Time.fixedDeltaTime));
        }
    }
}