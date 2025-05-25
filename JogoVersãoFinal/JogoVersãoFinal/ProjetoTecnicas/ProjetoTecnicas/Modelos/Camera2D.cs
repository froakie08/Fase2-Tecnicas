using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjetoTecnicas
{
    public class Camera2D
    {
        public Matrix Transformacao { get; private set; } // Matriz de transformação da câmara
        public Vector2 Posicao { get; set; } // Posição da câmara no mundo
        public float Zoom { get; set; } // Nível de zoom da câmara
        public float Rotacao { get; set; } // Rotação da câmara (caso seja usada)

        private Viewport _viewport; // Viewport do jogo (área visível da janela)

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;
            Posicao = Vector2.Zero; // Começa no canto superior esquerdo do mundo
            Zoom = 2.69f; // Zoom padrão (1.0 = sem zoom)
            Rotacao = 0f; // Sem rotação
        }

        public Vector2 EcranParaMundo(Vector2 posicaoEcran)
        {
            // Inverte a matriz de transformação da câmara
            Matrix matrizInversa = Matrix.Invert(Transformacao);
            // Aplica a transformação inversa à posição no ecrã
            return Vector2.Transform(posicaoEcran, matrizInversa);
        }

        // Este método calcula a matriz de transformação da câmara
        public void Atualizar(Vector2 posicaoAlvo)
        {
            // Centro do ecrã
            Vector2 centroEcran = new Vector2(_viewport.Width / 2f, _viewport.Height / 2f);

            // Calcula a transformação da câmara:
            // 1. Translada o mundo para que o alvo fique no centro do ecrã
            // 2. Aplica a rotação
            // 3. Aplica o zoom
            // 4. Translada para o centro do ecrã
            Transformacao = Matrix.CreateTranslation(new Vector3(-posicaoAlvo.X, -posicaoAlvo.Y, 0)) *
                            Matrix.CreateRotationZ(Rotacao) *
                            Matrix.CreateScale(Zoom, Zoom, 1) *
                            Matrix.CreateTranslation(new Vector3(centroEcran.X, centroEcran.Y, 0));

            // Atualiza a posição da câmara para a posição do alvo (útil para clamping)
            Posicao = posicaoAlvo;
        }

        // Método para "seguir" um personagem (o jogador)
        public void Seguir(Vector2 posicaoAlvo)
        {
            // Apenas define a posição da câmara para o alvo
            // A matriz de transformação será calculada no método Atualizar
            Posicao = posicaoAlvo;
        }

        // Opcional: Limita a câmara para não sair dos limites do mapa
        public void LimitarAosLimitesDoMapa(Rectangle limitesMapa)
        {
            // Calcula metade da largura e altura da viewport em coordenadas do mundo (ajustadas pelo zoom)
            float metadeLargura = _viewport.Width / (2f * Zoom);
            float metadeAltura = _viewport.Height / (2f * Zoom);

            float minX = limitesMapa.Left + metadeLargura;
            float maxX = limitesMapa.Right - metadeLargura;
            float minY = limitesMapa.Top + metadeAltura;
            float maxY = limitesMapa.Bottom - metadeAltura;

            // Garante que a posição da câmara fica dentro dos limites do mapa
            Posicao = new Vector2(
                MathHelper.Clamp(Posicao.X, minX, maxX),
                MathHelper.Clamp(Posicao.Y, minY, maxY)
            );

            // Se o mapa for mais pequeno que a viewport numa dimensão, centra a câmara nessa dimensão
            if (limitesMapa.Width * Zoom < _viewport.Width)
            {
                Posicao = new Vector2(limitesMapa.Center.X, Posicao.Y);
            }
            if (limitesMapa.Height * Zoom < _viewport.Height)
            {
                Posicao = new Vector2(Posicao.X, limitesMapa.Center.Y);
            }
        }
    }
}