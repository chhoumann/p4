using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Example : MonoBehaviour
    {
        private const float MoveSpeed = 10f;
        
        private void Update()
        {
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            transform.position += move * (MoveSpeed * Time.deltaTime);
        }
    }
}