using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DropShadow : MonoBehaviour
    {
        public Vector2 shadowOffset;
        public Material shadowMaterial;

        private SpriteRenderer _spriteRenderer;
        private GameObject _shadowObj;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _shadowObj = new GameObject("Shadow");
            _shadowObj.transform.SetParent(gameObject.transform, false);
            
            var shadowSpriteRenderer = _shadowObj.AddComponent<SpriteRenderer>();

            shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
            shadowSpriteRenderer.material = shadowMaterial;

            shadowSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
            shadowSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
        }

        private void LateUpdate()
        {
            var transform1 = transform;
            _shadowObj.transform.localPosition = transform1.localPosition + (Vector3)shadowOffset;
            _shadowObj.transform.localRotation = transform1.localRotation;
        }
    }
}
