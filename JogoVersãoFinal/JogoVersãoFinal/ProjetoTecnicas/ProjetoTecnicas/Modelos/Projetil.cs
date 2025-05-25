using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace ProjetoTecnicas;

public class Projetil : BasePersonagens
{
    public Vector2 Direction { get; set; }   // Direção do projétil (vetor normal)
    public float Lifespan { get; private set; } // Tempo de vida restante do projétil

    // Construtor que recebe a textura e dados do projétil
    public Projetil(Texture2D tex, DadosProjetil data) : base(tex, data.Position)
    {
        Speed = data.Speed;                 // Velocidade do projétil
        Rotation = data.Rotation;           // Rotação do projétil (em radianos)
        // Calcula a direção a partir da rotação, usando cosseno e seno
        Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
        Lifespan = data.Lifespan;           // Inicia o tempo de vida do projétil
    }

    // Método para destruir o projétil (definindo o tempo de vida a zero)
    public void Destruir()
    {
        Lifespan = 0;
    }

    // Atualiza a posição e reduz o tempo de vida do projétil
    public void Update()
    {
        // Move o projétil na direção definida pela velocidade e pelo tempo decorrido
        Position += Direction * Speed * Globais.TotalSeconds;

        // Decrementa o tempo de vida com base no tempo decorrido
        Lifespan -= Globais.TotalSeconds;
    }
}
