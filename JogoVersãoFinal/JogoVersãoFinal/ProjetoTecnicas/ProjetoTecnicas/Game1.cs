using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ProjetoTecnicas
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;          // Gerenciador de dispositivo gráfico
        private AdministradorJogo _administradorJogo;     // Instância do administrador principal do jogo

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            // Define a pasta onde os conteúdos são carregados
            IsMouseVisible = true;                         // Exibe o cursor do rato na janela do jogo
        }

        protected override void Initialize()
        {
            // Define a resolução preferida da janela
            _graphics.PreferredBackBufferWidth = 1920;    // Largura da janela
            _graphics.PreferredBackBufferHeight = 1080;   // Altura da janela

            // Atualiza as variáveis globais para largura e altura da tela
            Globais.LarguraTela = _graphics.PreferredBackBufferWidth;
            Globais.AlturaTela = _graphics.PreferredBackBufferHeight;

            _graphics.ApplyChanges();                       // Aplica as alterações nas configurações gráficas

            base.Initialize();                              // Chama o método base para inicialização padrão
        }

        protected override void LoadContent()
        {
            // Cria o SpriteBatch global para desenhar texturas
            Globais.SpriteBatch = new SpriteBatch(GraphicsDevice);

            Globais.Content = Content;                      // Guarda o ContentManager global

            // Inicializa a câmera 2D com a viewport atual do dispositivo gráfico
            Globais.Camera = new Camera2D(GraphicsDevice.Viewport);

            // Carrega a fonte para o score (pontuação)
            Globais.FonteScore = Content.Load<SpriteFont>("ScoreFont");

            // Carrega o efeito sonoro do disparo
            Globais.SomDisparo = Content.Load<SoundEffect>("EfeitoSomMago");

            // Cria a instância do administrador do jogo
            _administradorJogo = new AdministradorJogo();

            // Inicializa o administrador dos inimigos
            AdministradorInimigo.Init();
        }

        protected override void Update(GameTime gameTime)
        {
            // Permite sair do jogo ao pressionar o botão "Back" no gamepad ou a tecla "Escape"
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Atualiza o tempo global do jogo
            Globais.GameTime = gameTime;

            // Atualiza outros valores globais que dependem do tempo
            Globais.Update(gameTime);

            // Atualiza a lógica do jogo através do administrador
            if (_administradorJogo != null)
            {
                _administradorJogo.Update(gameTime);
            }

            base.Update(gameTime);  // Chama a atualização base
        }

        protected override void Draw(GameTime gameTime)
        {
            // Limpa a tela com a cor azul clara
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Inicia o SpriteBatch usando a matriz de transformação da câmera para o mundo 2D
            Globais.SpriteBatch.Begin(transformMatrix: Globais.Camera.Transform);

            // Chama o método Draw do administrador do jogo para desenhar todos os elementos
            if (_administradorJogo != null)
            {
                _administradorJogo.Draw();
            }

            // Finaliza o batch de desenho
            Globais.SpriteBatch.End();

            base.Draw(gameTime);  // Chama a base para completar o desenho
        }
    }
}