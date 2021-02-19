using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Player : MonoBehaviour
    {
        private const float MoveSpeed = 6f;

        public Vector2 Position => rb.position;

        private Rigidbody2D rb;

        private Vector2 moveInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveInput * (MoveSpeed * Time.fixedDeltaTime));
        }
    }
}