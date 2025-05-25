using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace ProjetoTecnicas;

public class Inimigo : BasePersonagens
{
    public int HP { get; private set; } // Pontos de vida do inimigo

    // Construtor que recebe a textura e a posição inicial
    public Inimigo(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 60; // Velocidade do inimigo
        HP = 1;     // Vida inicial do inimigo
    }

    // Método para o inimigo receber dano e reduzir os pontos de vida
    public void ReceberDano(int dano)
    {
        HP -= dano;
    }

    // Atualiza o inimigo: segue o jogador e roda na direção dele
    public void Update(Jogador jogador)
    {
        Vector2 direcaoParaJogador = jogador.Position - Position;
        Rotation = (float)Math.Atan2(direcaoParaJogador.Y, direcaoParaJogador.X);

        if (direcaoParaJogador.Length() > 4) // Move se estiver distante do jogador
        {
            Vector2 direcaoNormalizada = Vector2.Normalize(direcaoParaJogador);
            Position += direcaoNormalizada * Speed * Globais.TotalSeconds;
        }
    }

    // O método Draw() pode ficar na classe BasePersonagens, 
    // que usa a propriedade 'texture' herdada para desenhar o inimigo.
}