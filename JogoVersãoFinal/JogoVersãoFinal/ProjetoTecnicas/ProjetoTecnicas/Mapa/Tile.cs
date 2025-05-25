using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetoTecnicas
{
    public class Tile
    {
        public Texture2D Texture { get; private set; }
        public bool IsSolid { get; private set; } // Indica se o tile é sólido (ex: parede, etc.)

        // Construtor que recebe a textura e se o tile é sólido (padrão: falso)
        public Tile(Texture2D texture, bool isSolid = false)
        {
            Texture = texture;
            IsSolid = isSolid; // Guarda se o tile bloqueia movimento
        }
    }
}