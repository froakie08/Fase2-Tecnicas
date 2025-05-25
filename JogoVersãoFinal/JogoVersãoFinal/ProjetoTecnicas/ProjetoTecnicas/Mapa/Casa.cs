using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetoTecnicas
{
    public class Casa
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public Rectangle Bounds { get; private set; }
        private float _scale; // Guarda a escala da casa

        // Construtor que recebe a textura, posição e escala (com valor padrão 4f)
        public Casa(Texture2D texture, Vector2 position, float scale = 4f) // Valor padrão de 4.0f para aumentar o tamanho
        {
            Texture = texture;
            Position = position;
            _scale = scale; // Armazena a escala fornecida

            // Calcula os limites (Bounds) da casa com base na posição e escala
            Bounds = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Texture.Width * _scale),  // Largura da textura multiplicada pela escala
                (int)(Texture.Height * _scale)  // Altura da textura multiplicada pela escala
            );
        }

        public void Draw()
        {
            // Desenha a casa usando o SpriteBatch global, aplicando a escala definida
            Globais.SpriteBatch.Draw(Texture,
                                     Position,
                                     null,              // Usa a textura inteira (sem sourceRectangle)
                                     Color.White,
                                     0f,                // Sem rotação
                                     Vector2.Zero,      // Origem no canto superior esquerdo
                                     _scale,            // Aplica a escala guardada
                                     SpriteEffects.None,
                                     0f);               // Profundidade da camada (layerDepth)
        }
    }
}