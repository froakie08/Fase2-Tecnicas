using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTecnicas;

// Classe responsável por gerir todos os projéteis existentes no jogo
public static class AdministradorProjetil
{
    private static Texture2D _texture; // Textura usada para desenhar os projéteis
    public static List<Projetil> Projeteis { get; } = new(); // Lista de projéteis ativos no jogo

    // Inicia a textura base usada pelos projéteis
    public static void Init(Texture2D tex)
    {
        _texture = tex;
    }

    // Adiciona um novo projétil à lista, com base nos dados fornecidos
    public static void AdicionarProjetil(DadosProjetil data)
    {
        Projeteis.Add(new(_texture, data));
    }

    // Atualiza o estado de todos os projéteis e verifica colisões com inimigos
    public static void Update(List<Inimigo> inimigos)
    {
        foreach (var p in Projeteis)
        {
            p.Update(); // Atualiza a posição e o tempo de vida do projétil

            foreach (var z in inimigos)
            {
                if (z.HP <= 0) continue; // Ignora inimigos já derrotados

                // Verifica se a distância entre o projétil e o inimigo é suficientemente pequena para considerar colisão
                if ((p.Position - z.Position).Length() < 32)
                {
                    z.ReceberDano(1); // Aplica dano ao inimigo
                    p.Destruir();     // Marca o projétil para remoção

                    if (z.HP <= 0) // Verifica se o inimigo foi eliminado após receber dano
                    {
                        Globais.Score += 100; // Atribui 100 pontos ao jogador por eliminar um inimigo
                    }

                    break; // Evita que o mesmo projétil atinja múltiplos inimigos no mesmo frame
                }
            }
        }

        // Remove todos os projéteis cujo tempo de vida chegou ao fim
        Projeteis.RemoveAll((p) => p.Lifespan <= 0);
    }

    // Desenha todos os projéteis no ecrã
    public static void Draw()
    {
        foreach (var p in Projeteis)
        {
            p.Draw();
        }
    }
}