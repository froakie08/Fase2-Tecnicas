using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace ProjetoTecnicas
{
    public class BasePersonagens
    {
        // ***** ESTA É A PROPRIEDADE QUE ESTAVA EM FALTA OU INACESSÍVEL *****
        public Texture2D Textura { get; protected set; } // Guarda a textura do personagem
        // *******************************************************************

        protected readonly Vector2 Origem;
        public Vector2 Posicao { get; set; }
        public int Velocidade { get; set; }
        public float Rotacao { get; set; }

        public Rectangle Limites
        {
            get
            {
                // ***** CORREÇÃO AQUI: Se Posicao é o centro (devido ao uso de 'Origem' no Draw),
                // então, para obter o canto superior esquerdo do retângulo, subtraímos a 'Origem'. *****
                return new Rectangle(
                    (int)(Posicao.X - Origem.X), // Ajusta X para o canto superior esquerdo
                    (int)(Posicao.Y - Origem.Y), // Ajusta Y para o canto superior esquerdo
                    Textura.Width,
                    Textura.Height
                );
            }
        }

        public BasePersonagens(Texture2D textura, Vector2 posicao)
        {
            this.Textura = textura;
            this.Posicao = posicao;
            Velocidade = 150;
            Origem = new Vector2(textura.Width / 2f, textura.Height / 2f);
        }

        public virtual void Desenhar()
        {
            Globais.SpriteBatch.Draw(Textura, Posicao, null, Color.White, Rotacao, Origem, 1f, SpriteEffects.None, 1f);
        }
    }
}