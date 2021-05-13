using UnityEngine;

namespace Dazel.Game.Entities
{
    public sealed class Player : Entity
    {
        [SerializeField] private BoxCollider2D spriteBounds;
        
        private const float MoveSpeed = 6f;

        public Vector2 Position
        {
            get => rb.position;
            set => rb.position = value;
        }

        public Vector2 Size => spriteBounds.size;
        public Vector2 Extents => spriteBounds.bounds.extents;

        private Rigidbody2D rb;

        private Vector2 moveInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            MoveDirection = moveInput;
        }

        private void FixedUpdate()
        {
            rb.velocity = MoveDirection * MoveSpeed;
        }
    }
}