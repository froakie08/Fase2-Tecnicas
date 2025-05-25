using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjetoTecnicas
{
    public static class AdministradorHUD
    {
        private static Texture2D _projetilTexture;

        // Método para iniciar a textura do projétil usada na HUD
        public static void Init(Texture2D tex)
        {
            _projetilTexture = tex;
        }

        // Método responsável por desenhar a HUD com base no estado atual do jogador
        public static void Draw(Jogador jogador)
        {
            // Define a cor dos projéteis na HUD:
            // Vermelho se a arma estiver a recarregar, branco caso contrário
            Color c = jogador.Arma.Recarregando ? Color.Red : Color.White;

            // Calcula a posição base da HUD em relação ao jogador
            // Neste caso, um pouco abaixo e à direita do jogador
            Vector2 hudBasePosition = jogador.Position + new Vector2(-50, jogador.texture.Height / 3 + 30);

            // Desenha uma "bala" por cada unidade de munição disponível
            for (int i = 0; i < jogador.Arma.Ammo; i++)
            {
                // A posição de cada projétil é calculada com base num espaçamento horizontal
                Vector2 pos = hudBasePosition + new Vector2(i * (_projetilTexture.Width + 2), 0);

                // Desenha a textura do projétil com uma leve transparência (75%)
                Globais.SpriteBatch.Draw(_projetilTexture, pos, null, c * 0.75f, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }

            // Se a fonte do score estiver definida, desenha o texto do score
            if (Globais.FonteScore != null)
            {
                string scoreText = $"Score: {Globais.Score}";

                // Define a posição do texto do score em relação ao jogador
                // Neste caso, um pouco acima e à direita do jogador
                Vector2 scorePosition = jogador.Position + new Vector2(90, -jogador.texture.Height / 2 + 55);

                // Calcula a origem do texto para que este fique centrado na posição indicada
                Vector2 origin = Globais.FonteScore.MeasureString(scoreText) / 2;

                // Desenha o texto do score
                Globais.SpriteBatch.DrawString(Globais.FonteScore, scoreText, scorePosition, Color.White, 0, origin, 1f, SpriteEffects.None, 1);
            }
        }
    }
}