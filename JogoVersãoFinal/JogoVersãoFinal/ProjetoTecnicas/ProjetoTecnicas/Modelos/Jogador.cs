using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetoTecnicas
{
    public class Jogador
    {
        public Texture2D texture;               // Textura do jogador
        public Vector2 Position;                // Posição do jogador no mundo
        public float Speed = 200f;              // Velocidade do jogador

        // A arma do jogador é do tipo ArmaGeral ou uma das suas subclasses
        public ArmaGeral Arma { get; private set; } // Arma atual do jogador (alterado para ArmaGeral)

        private readonly ArmaGeral Arma1 = new BestaNormal(); // Instância da arma BestaNormal
        private readonly ArmaGeral Arma2 = new TiroDuplo();   // Instância da arma TiroDuplo

        public float Rotation { get; set; }    // Rotação do jogador (para apontar a arma)

        // Propriedade para o retângulo de colisão do jogador
        public Rectangle Bounds
        {
            get
            {
                // Calcula os limites do jogador, assumindo que a origem está no centro da textura
                return new Rectangle(
                    (int)Position.X - texture.Width / 2,
                    (int)Position.Y - texture.Height / 2,
                    texture.Width,
                    texture.Height
                );
            }
        }

        // Construtor que recebe a textura e posição inicial
        public Jogador(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            Arma = Arma1; // Inicializa a arma com a BestaNormal por defeito
        }

        // Método para alternar entre os dois tipos de armas disponíveis
        public void MudarTipo()
        {
            Arma = (Arma == Arma1) ? Arma2 : Arma1;
        }

        // Atualiza o estado do jogador
        public void Update(GameTime gameTime, Rectangle mapBounds, List<Casa> casas)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Direção desejada de movimento (definida no AdministradorInputs)
            Vector2 desiredMovement = AdministradorInputs.Direction;

            // Calcula a nova posição potencial do jogador
            Vector2 newPosition = Position + desiredMovement * Speed * deltaTime;

            // Cria um retângulo para a nova posição, para verificar colisões
            Rectangle newPlayerBounds = new Rectangle(
                (int)newPosition.X - texture.Width / 2,
                (int)newPosition.Y - texture.Height / 2,
                texture.Width,
                texture.Height
            );

            // Verifica se a nova posição está dentro dos limites do mapa
            if (mapBounds.Contains(newPlayerBounds))
            {
                bool collisionWithHouse = false;
                foreach (var casa in casas)
                {
                    // Verifica colisão com casas
                    if (newPlayerBounds.Intersects(casa.Bounds))
                    {
                        collisionWithHouse = true;
                        break;
                    }
                }

                // Só atualiza a posição se não houver colisão com casas
                if (!collisionWithHouse)
                {
                    Position = newPosition;
                }
            }

            // Rotação do jogador para apontar para o rato (posição no mundo)
            Vector2 mouseWorldPosition = Vector2.Transform(
                AdministradorInputs.MousePosition,
                Matrix.Invert(Globais.Camera.Transform)
            );
            Vector2 directionToMouse = mouseWorldPosition - Position;
            Rotation = (float)Math.Atan2(directionToMouse.Y, directionToMouse.X);

            // Atualiza a arma (sem parâmetros)
            Arma.Update();

            // Disparo da arma ao carregar no botão esquerdo do rato
            if (AdministradorInputs.MouseLeftDown)
            {
                Arma.Disparar(this); // Passa o próprio jogador para o método Disparar
            }

            // Recarregar arma com botão direito do rato (ou tecla R, conforme implementação do AdministradorInputs)
            if (AdministradorInputs.MouseRightClicked)
            {
                Arma.Recarregar();
            }

            // Alternar arma com a tecla espaço
            if (AdministradorInputs.SpacePressed)
            {
                MudarTipo();
            }
        }

        // Método para desenhar o jogador na posição atual, com rotação
        public void Draw()
        {
            // Desenha o sprite com rotação, origem no centro para rodar corretamente
            Globais.SpriteBatch.Draw(
                texture,
                Position,
                null,
                Color.White,
                Rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                1f,
                SpriteEffects.None,
                0.2f);
        }
    }
}