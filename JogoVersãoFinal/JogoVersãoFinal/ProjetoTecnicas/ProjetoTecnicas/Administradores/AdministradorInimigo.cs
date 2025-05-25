using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetoTecnicas;

public static class AdministradorInimigo
{
    public static List<Inimigo> Inimigos { get; } = new();

    // Lista que guarda todas as texturas disponíveis para os inimigos
    private static List<Texture2D> _inimigoTextures;

    private static float _spawnCooldown;
    private static float _spawnTime;
    private static Random _random;
    private static int _padding;

    // Inicia as variáveis e carrega as texturas dos inimigos
    public static void Init()
    {
        _inimigoTextures = new List<Texture2D>();

        // Carrega várias texturas de inimigos do Content
        _inimigoTextures.Add(Globais.Content.Load<Texture2D>("Goblin"));
        _inimigoTextures.Add(Globais.Content.Load<Texture2D>("TLOU"));
        _inimigoTextures.Add(Globais.Content.Load<Texture2D>("REC"));

        // Define o padding com base na largura da primeira textura (caso existam texturas)
        if (_inimigoTextures.Any())
        {
            _padding = _inimigoTextures[0].Width / 2;
        }
        else
        {
            // Valor por defeito, caso não haja texturas carregadas
            _padding = 32;
        }

        _spawnCooldown = 1f; // Tempo entre cada spawn de inimigos
        _spawnTime = _spawnCooldown;
        _random = new Random();
    }

    // Calcula uma posição aleatória fora dos limites do ecrã para gerar um inimigo
    public static Vector2 PosicaoRandom()
    {
        // Define um dos quatro lados possíveis para spawn (0: cima, 1: direita, 2: baixo, 3: esquerda)
        int side = _random.Next(4);

        float x, y;

        switch (side)
        {
            case 0: // Cima
                x = _random.Next(_padding, Globais.LarguraTela - _padding);
                y = -_padding;
                break;
            case 1: // Direita
                x = Globais.LarguraTela + _padding;
                y = _random.Next(_padding, Globais.AlturaTela - _padding);
                break;
            case 2: // Baixo
                x = _random.Next(_padding, Globais.LarguraTela - _padding);
                y = Globais.AlturaTela + _padding;
                break;
            case 3: // Esquerda
                x = -_padding;
                y = _random.Next(_padding, Globais.AlturaTela - _padding);
                break;
            default:
                x = 0;
                y = 0;
                break;
        }

        return new Vector2(x, y);
    }

    // Cria um novo inimigo com uma textura aleatória e adiciona-o à lista
    public static void AdicionarInimigo()
    {
        if (_inimigoTextures.Count == 0)
        {
            Console.WriteLine("Nenhuma textura de inimigo carregada para spawnar!");
            return;
        }

        // Escolhe uma textura aleatória da lista carregada
        Texture2D randomTexture = _inimigoTextures[_random.Next(0, _inimigoTextures.Count)];

        // Cria o inimigo com a textura escolhida e posição aleatória
        Inimigos.Add(new Inimigo(randomTexture, PosicaoRandom()));
    }

    // Atualiza o estado de todos os inimigos e trata da lógica de spawn
    public static void Update(Jogador jogador)
    {
        // Atualiza todos os inimigos com base na posição do jogador
        foreach (var z in Inimigos)
        {
            z.Update(jogador);
        }

        // Remove os inimigos que já não têm vida
        Inimigos.RemoveAll((z) => z.HP <= 0);

        // Reduz o tempo até ao próximo spawn
        _spawnTime -= Globais.TotalSeconds;
        if (_spawnTime <= 0)
        {
            AdicionarInimigo();
            _spawnTime = _spawnCooldown;
        }
    }

    // Desenha todos os inimigos no ecrã
    public static void Draw()
    {
        foreach (var z in Inimigos)
        {
            z.Draw();
        }
    }
}