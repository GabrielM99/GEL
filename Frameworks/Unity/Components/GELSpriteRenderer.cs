using UnityEngine;

namespace Fusyon.GEL.Unity
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GELSpriteRenderer : GELBehaviour<SpriteRenderer>, IGELSpriteRenderer
    {
        private GELMaterial _Material;

        public object Sprite { get => Base.sprite; set => Base.sprite = (Sprite)value; }
        public System.Drawing.Color Color { get => Base.color.ToGEL(); set => Base.color = value.ToUnity(); }
        public GELMaterial Material
        {
            get => _Material ??= new GELMaterial(Base.material).ToUnity();

            set
            {
                Base.material = (Material)value.Source;
                _Material = value.ToUnity();
            }
        }
    }
}