using P4.MapGenerator.Entities;
using UnityEngine;

namespace P4.MapGenerator
{
    [RequireComponent(typeof(Entity))]
    public sealed class EntityAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private const string IdleName = "Idle";
        private const string MoveName = "Move";

        private const string Up = "Up";
        private const string Down = "Down";
        private const string Left = "Left";
        private const string Right = "Right";

        private const float MinMoveAmount = 0.01f;

        private Entity entity;

        private Vector2 moveDirLast;

        private string animName;
        private string moveAnimName;
        
        private void Start()
        {
            entity = GetComponent<Entity>();
            animName = IdleName;
            moveAnimName = Down;
        }

        private void Update()
        {
            PlayMoveAnimation(entity.MoveDirection);
        }
        
        private void PlayMoveAnimation(Vector2 moveDir)
        {
            float moveAmount = moveDir.magnitude;
            
            animName = moveAmount <= MinMoveAmount ? IdleName : MoveName;

            if (moveAmount > MinMoveAmount)
            {
                if (moveDir.y == 0 || moveDirLast.x == 0 && moveDir.x != 0)
                {
                    moveAnimName = Mathf.Sign(moveDir.x) > 0 ? Right : Left;
                }
                else if (moveDir.x == 0 || moveDirLast.y == 0 && moveDir.y != 0)
                {
                    moveAnimName = Mathf.Sign(moveDir.y) > 0 ? Up : Down;
                }
            }

            animator.Play($"{animName}{moveAnimName}");
            moveDirLast = moveDir;
        }
    }
}
