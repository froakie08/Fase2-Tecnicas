using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTecnicas;

// Classe que representa uma besta normal, derivada da arma base ArmaGeral
public class BestaNormal : ArmaGeral
{
    // Construtor que define as propriedades específicas da besta normal
    public BestaNormal()
    {
        cooldown = 0.7f;       // Tempo de espera entre disparos de 0.7 segundos
        maxAmmo = 6;           // Capacidade máxima de 6 virotes
        Ammo = maxAmmo;        // Começa com munição cheia
        reloadTime = 2f;       // Tempo de recarregamento de 2 segundos
    }

    // Implementação do método abstrato para criar projéteis quando a besta dispara
    protected override void CriarProjecteis(Jogador jogador)
    {
        DadosProjetil pd = new()
        {
            Position = jogador.Position,   // Posição inicial do projétil é a do jogador
            Rotation = jogador.Rotation,   // Rotação igual à do jogador para disparo na direção certa
            Lifespan = 2f,                 // Vida útil do projétil de 2 segundos
            Speed = 300                   // Velocidade do projétil
        };

        AdministradorProjetil.AdicionarProjetil(pd); // Adiciona o projétil à lista de projéteis do jogo

    }
}