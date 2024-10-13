using System.Drawing;

namespace Fusyon.GEL
{
    public interface IGELSpriteRenderer
    {
        object Sprite { get; set; }
        Color Color { get; set; }
        GELMaterial Material { get; set; }
    }
}