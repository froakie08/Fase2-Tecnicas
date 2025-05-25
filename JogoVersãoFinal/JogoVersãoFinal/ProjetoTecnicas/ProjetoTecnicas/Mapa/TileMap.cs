using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ProjetoTecnicas.Mapa; // Certifica-te que este using está aqui para a classe Arvores

namespace ProjetoTecnicas
{
    public class Tilemap
    {
        private Tile[,] _tiles;              // Matriz de tiles que compõem o mapa
        private int _tileWidth;              // Largura de cada tile em pixels
        private int _tileHeight;             // Altura de cada tile em pixels
        public int MapWidthTiles { get; private set; }   // Número de tiles na largura do mapa
        public int MapHeightTiles { get; private set; }  // Número de tiles na altura do mapa
        public Rectangle MapBounds { get; private set; } // Área total do mapa em pixels

        private List<Texture2D> _allTileTextures;        // Lista com todas as texturas de tiles
        private Random _random;                           // Gerador de números aleatórios
        private List<Texture2D> _houseTextures;          // Lista com texturas de casas
        public List<Casa> Houses { get; private set; }   // Lista de casas geradas no mapa

        private List<Texture2D> _arvoresTODAS;            // Lista com texturas de árvores
        public List<Arvores> Trees { get; private set; } // Lista de árvores geradas no mapa

        // Construtor que inicializa o mapa com dimensões e tamanhos dos tiles
        public Tilemap(int mapWidthTiles, int mapHeightTiles, int tileWidth, int tileHeight)
        {
            MapWidthTiles = mapWidthTiles;
            MapHeightTiles = mapHeightTiles;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _tiles = new Tile[MapWidthTiles, MapHeightTiles];

            MapBounds = new Rectangle(0, 0, MapWidthTiles * _tileWidth, MapHeightTiles * _tileHeight);
            _random = new Random();
            _allTileTextures = new List<Texture2D>();
            _houseTextures = new List<Texture2D>();
            Houses = new List<Casa>();
            _arvoresTODAS = new List<Texture2D>();
            Trees = new List<Arvores>();
        }

        // Método para carregar as texturas do conteúdo
        public void LoadContent()
        {
            _allTileTextures.Add(Globais.Content.Load<Texture2D>("tile_1"));
            _allTileTextures.Add(Globais.Content.Load<Texture2D>("tile_2"));

            _houseTextures.Add(Globais.Content.Load<Texture2D>("Casa_1CORRETA"));
            _arvoresTODAS.Add(Globais.Content.Load<Texture2D>("Tree1"));
            _arvoresTODAS.Add(Globais.Content.Load<Texture2D>("Tree2"));
        }

        // Método para gerar o mapa e popular tiles, casas e árvores
        public void GenerateMap()
        {
            for (int x = 0; x < MapWidthTiles; x++)
            {
                for (int y = 0; y < MapHeightTiles; y++)
                {
                    int randomIndex = _random.Next(0, _allTileTextures.Count);
                    Texture2D selectedTexture = _allTileTextures[randomIndex];
                    bool isSolid = false;  // Podes ajustar se alguns tiles forem sólidos
                    _tiles[x, y] = new Tile(selectedTexture, isSolid);
                }
            }

            GenerateHouses(10);  // Gera 10 casas
            GenerateTrees(30);   // Gera 30 árvores
        }

        // Método auxiliar para verificar se uma área está livre de colisões com casas ou árvores
        private bool IsAreaClear(Rectangle newBounds)
        {
            // Verifica colisão com casas existentes
            foreach (var existingHouse in Houses)
            {
                if (newBounds.Intersects(existingHouse.Bounds))
                {
                    return false; // A área colide com uma casa
                }
            }

            // Verifica colisão com árvores existentes
            foreach (var existingTree in Trees)
            {
                if (newBounds.Intersects(existingTree.Bounds))
                {
                    return false; // A área colide com uma árvore
                }
            }

            // Se quiseres verificar outras colisões, adiciona aqui

            return true; // Área livre para colocar novo objeto
        }

        // Método para gerar casas aleatoriamente no mapa
        private void GenerateHouses(int numberOfHousesToAttempt)
        {
            if (_houseTextures.Count == 0) return;

            int maxAttemptsPerHouse = 5;   // Número máximo de tentativas para colocar cada casa
            float defaultHouseScale = 4f;  // Escala padrão para casas

            for (int i = 0; i < numberOfHousesToAttempt; i++)
            {
                Texture2D houseTexture = _houseTextures[_random.Next(0, _houseTextures.Count)];
                bool placed = false;
                for (int attempt = 0; attempt < maxAttemptsPerHouse; attempt++)
                {
                    int randomTileX = _random.Next(0, MapWidthTiles);
                    int randomTileY = _random.Next(0, MapHeightTiles);

                    Vector2 housePosition = new Vector2(randomTileX * _tileWidth, randomTileY * _tileHeight);

                    Rectangle newHouseBounds = new Rectangle(
                        (int)housePosition.X,
                        (int)housePosition.Y,
                        (int)(houseTexture.Width * defaultHouseScale),
                        (int)(houseTexture.Height * defaultHouseScale)
                    );

                    // Usa o método IsAreaClear para garantir que não há sobreposição
                    if (IsAreaClear(newHouseBounds))
                    {
                        Houses.Add(new Casa(houseTexture, housePosition, defaultHouseScale));
                        placed = true;
                        break;
                    }
                }
            }
        }

        // Método para gerar árvores aleatoriamente no mapa
        private void GenerateTrees(int numberOfArvoresToAttempt)
        {
            if (_arvoresTODAS.Count == 0) return;

            int maxAttemptsPerTree = 5;    // Número máximo de tentativas para colocar cada árvore
            float defaultArvoreScale = 2f; // Escala padrão para árvores

            for (int i = 0; i < numberOfArvoresToAttempt; i++)
            {
                Texture2D arvoreTexture = _arvoresTODAS[_random.Next(0, _arvoresTODAS.Count)];
                bool placed = false;
                for (int attempt = 0; attempt < maxAttemptsPerTree; attempt++)
                {
                    int randomTileX = _random.Next(0, MapWidthTiles);
                    int randomTileY = _random.Next(0, MapHeightTiles);

                    Vector2 arvorePosition = new Vector2(randomTileX * _tileWidth, randomTileY * _tileHeight);

                    Rectangle newTreeBounds = new Rectangle(
                        (int)arvorePosition.X,
                        (int)arvorePosition.Y,
                        (int)(arvoreTexture.Width * defaultArvoreScale),
                        (int)(arvoreTexture.Height * defaultArvoreScale)
                    );

                    // Usa o método IsAreaClear para garantir que não há sobreposição
                    if (IsAreaClear(newTreeBounds))
                    {
                        Trees.Add(new Arvores(arvoreTexture, arvorePosition, defaultArvoreScale));
                        placed = true;
                        break;
                    }
                }
            }
        }

        // Método para desenhar o mapa, só desenhando os tiles visíveis na área da câmara
        public void Draw(Camera2D camera)
        {
            Matrix inverseViewMatrix = Matrix.Invert(camera.Transform);
            Vector2 worldTopLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 worldBottomRight = Vector2.Transform(new Vector2(Globais.LarguraTela, Globais.AlturaTela), inverseViewMatrix);

            int startX = (int)(worldTopLeft.X / _tileWidth);
            int startY = (int)(worldTopLeft.Y / _tileHeight);
            int endX = (int)(worldBottomRight.X / _tileWidth);
            int endY = (int)(worldBottomRight.Y / _tileHeight);

            // Garante que os índices estão dentro dos limites do mapa
            startX = MathHelper.Max(0, startX);
            startY = MathHelper.Max(0, startY);
            endX = MathHelper.Min(MapWidthTiles - 1, endX);
            endY = MathHelper.Min(MapHeightTiles - 1, endY);

            // Desenha apenas os tiles visíveis
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile tile = _tiles[x, y];
                    if (tile != null && tile.Texture != null)
                    {
                        Vector2 drawPosition = new Vector2(x * _tileWidth, y * _tileHeight);
                        Globais.SpriteBatch.Draw(tile.Texture, drawPosition, Color.White);
                    }
                }
            }
        }
    }
}