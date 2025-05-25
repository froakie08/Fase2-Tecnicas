using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTecnicas;

// Classe que representa uma arma de tiro duplo, derivada de ArmaGeral
public class TiroDuplo : ArmaGeral
{
    // Construtor que define as propriedades específicas do TiroDuplo
    public TiroDuplo()
    {
        cooldown = 0.75f;      // Tempo de espera entre disparos de 0.75 segundos
        maxAmmo = 4;           // Capacidade máxima de 4 munições
        Ammo = maxAmmo;        // Começa com munição cheia
        reloadTime = 3f;       // Tempo de recarregamento de 3 segundos
    }

    // Implementação do método abstrato para criar múltiplos projéteis quando a arma dispara
    protected override void CriarProjecteis(Jogador jogador)
    {
        const float angleStep = (float)(Math.PI / 16); // Definição do passo do ângulo entre projéteis

        DadosProjetil pd = new()
        {
            Position = jogador.Position,               // Posição inicial do projétil é a do jogador
            Rotation = jogador.Rotation - (3 * angleStep),  // Começa a rotação deslocada para a esquerda
            Lifespan = 0.5f,                          // Vida útil do projétil de meio segundo
            Speed = 300                               // Velocidade do projétil
        };

        // Cria e adiciona 5 projéteis em intervalos de angleStep para formar um leque
        for (int i = 0; i < 5; i++)
        {
            pd.Rotation += angleStep;                 // Incrementa o ângulo para o próximo projétil
            AdministradorProjetil.AdicionarProjetil(pd);  // Adiciona o projétil à lista global
        }

    }
}