using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTecnicas
{
    public class AdministradorJogo
    {
        private readonly Jogador _jogador;
        public static bool GameOver = false;

        private Rectangle _mapBounds;
        private Tilemap _tilemap;

        public Jogador Jogador => _jogador;

        public AdministradorJogo()
        {
            // Define o tamanho do mapa em tiles e o tamanho de cada tile em pixeis
            int mapWidthTiles = 50;
            int mapHeightTiles = 50;
            int tilePixelSize = 32;

            // Cria e inicia a tilemap com estas dimensões
            _tilemap = new Tilemap(mapWidthTiles, mapHeightTiles, tilePixelSize, tilePixelSize);
            _tilemap.LoadContent();   // Carrega os conteúdos (texturas, etc)
            _tilemap.GenerateMap();   // Gera o layout do mapa

            _mapBounds = _tilemap.MapBounds;  // Guarda os limites do mapa em pixeis

            // Carrega a textura dos projéteis
            var texture = Globais.Content.Load<Texture2D>("Efeitomago");

            // Inicia os sistemas de projéteis e da HUD com esta textura
            AdministradorProjetil.Init(texture);
            AdministradorHUD.Init(texture);

            // Cria o jogador no centro do mapa com a textura "MagoLindo"
            _jogador = new(Globais.Content.Load<Texture2D>("MagoLindo"), new(
                         _mapBounds.Width / 2, _mapBounds.Height / 2));

            // Inicia o sistema de inimigos e adiciono o primeiro inimigo
            AdministradorInimigo.Init();
            AdministradorInimigo.AdicionarInimigo();
        }

        public void Update(GameTime gameTime)
        {
            // Se o jogo acabou, não faz sentido continuar a atualizar nada
            if (GameOver)
            {
                return;
            }

            // Atualiza os inputs do teclado e do rato
            AdministradorInputs.Update();

            // Atualiza o jogador, passando os limites do mapa e a lista de casas (para colisões, etc)
            _jogador.Update(gameTime, _mapBounds, _tilemap.Houses);

            // Atualiza os projéteis, passando a lista de inimigos (para deteção de colisões, etc)
            AdministradorProjetil.Update(AdministradorInimigo.Inimigos);

            // Atualiza o comportamento dos inimigos, com acesso ao jogador
            AdministradorInimigo.Update(_jogador);

            // A câmara segue o jogador e fica limitada aos limites do mapa
            Globais.Camera.Follow(_jogador.Position);
            Globais.Camera.ClampToMapBounds(_mapBounds);
            Globais.Camera.Update(Globais.Camera.Position);

            // Verifica se o jogador colidiu com algum inimigo
            // Se sim, termina o jogo
            foreach (var inimigo in AdministradorInimigo.Inimigos.ToList())
            {
                if (_jogador.Bounds.Intersects(inimigo.Bounds))
                {
                    GameOver = true;
                    return;
                }
            }
        }

        public void Draw()
        {
            // Desenha o mapa (tiles de fundo)
            _tilemap.Draw(Globais.Camera);

            // Desenha os inimigos
            AdministradorInimigo.Draw();

            // Desenha as casas (vêm depois dos tiles para ficarem por cima)
            foreach (var house in _tilemap.Houses)
            {
                house.Draw();
            }

            // Desenha as árvores (primeira camada)
            foreach (var arvores in _tilemap.Trees)
            {
                arvores.Draw();
            }

            // Desenha os projéteis (por cima de tudo exceto HUD)
            AdministradorProjetil.Draw();

            // Desenha o jogador
            _jogador.Draw();

            // Volta a desenhar as árvores — isto pode ser útil para sobreposição ao jogador (ex: o jogador passa atrás de árvores)
            foreach (var arvores in _tilemap.Trees)
            {
                arvores.Draw();
            }

            // Desenho a HUD (munição, score, etc)
            AdministradorHUD.Draw(_jogador);
        }
    }
}