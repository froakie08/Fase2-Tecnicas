using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjetoTecnicas
{
    public static class Globais
    {
        // Tempo total passado desde o último frame, em segundos
        public static float TotalSeconds { get; set; }

        // ContentManager para carregar conteúdos (texturas, sons, fontes, etc)
        public static ContentManager Content { get; set; }

        // SpriteBatch usado para desenhar texturas na tela
        public static SpriteBatch SpriteBatch { get; set; }

        // Objeto que guarda o tempo atual do jogo
        public static GameTime GameTime { get; set; }

        // Câmera 2D que controla a transformação da visualização no jogo
        public static Camera2D Camera { get; set; }

        // Largura da janela/jogo em pixels
        public static int LarguraTela { get; set; }

        // Altura da janela/jogo em pixels
        public static int AlturaTela { get; set; }

        // Pontuação atual do jogador
        public static int Score { get; set; } = 0;

        // Dispositivo gráfico para operações de baixo nível (criação de texturas, buffers, etc)
        public static GraphicsDevice GraphicsDevice { get; set; }

        // Fonte usada para desenhar o score (pontuação)
        public static SpriteFont FonteScore { get; set; }

        // Efeito sonoro do disparo no jogo
        public static SoundEffect SomDisparo { get; set; }

        // Método para atualizar valores globais que dependem do tempo
        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;  // Tempo decorrido desde o último frame
        }
    }
}