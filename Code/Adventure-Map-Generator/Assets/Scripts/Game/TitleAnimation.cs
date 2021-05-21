using UnityEngine;

namespace Dazel.Game
{
    public sealed class TitleAnimation : MonoBehaviour
    {
        private RectTransform title;
        private float y;

        private void Start()
        {
            title = GetComponent<RectTransform>();
            y = title.anchoredPosition.y;
        }

        private void Update()
        {
            float rot = Mathf.Sin(Time.time * 0.5f);
            float move = Mathf.Sin(Time.time * 1f);
            float size = (Mathf.Sin(Time.time * 1f) + 1f) * 0.1f;

            title.anchoredPosition = new Vector2(title.anchoredPosition.x, y + move * 10f);
            title.rotation = Quaternion.Euler(0, 0, rot * 5f);
            title.localScale = new Vector3(1 + size, 1 + size, 1);
        }
    }
}
