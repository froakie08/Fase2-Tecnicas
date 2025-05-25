<h1>🎮 Fase2 - Trabalho de Técnicas de Desenvolvimento de Vídeojogos</h1>

  <div class="section">
🔧 Trabalho realizado por: <br></br>
* Guilherme Torres nº31486 <br></br>
* Diogo Gomes nº31493
<h3>🧙🏻 Shadow Valley 🧙🏻</h3>

<p>&nbsp; Em <b>Shadow Valley</b>, és um poderoso mago, da <i>última união de defesa</i> de um vale outrora pacífico!
Agora, <i>infestado por forças sombrias</i>, o vale precisa de ser liberto de tal <b>maldição</b>.
Enfrenta implacáveis inimigos em <b>combates frenéticos</b>, usando um cajado de feitiços destrutivos. Desvia-te, lança feitiços com precisão e fortalece as tuas habilidades de gamer para sobreviver.</p>
<p>Consegues purificar o <b>"Shadow Valley"</b> e restaurar a sua antiga glória?</p>

  </div>

  <div class="section">
    <h2>🧙‍♂️ Funcionalidades do Jogo</h2>
    <ul>
      <li>Movimentação livre do jogador com normalização da direção</li>
      <li>Mapa gerado com <code>Tilemap</code> de 50x50 tiles (cada tile tem 32x32 píxeis)</li>
      <li>Desenho de casas e árvores no mapa</li>
      <li>Colisões entre jogador e inimigo causa a derrota<strong>GameOver</strong></li>
      <li>HUD com elementos gráficos carregados via textura</li>
      <li>Câmara dinâmica que segue o jogador</li>
    </ul>
  </div>

  <div class="section">
    <h2>🎮 Controlos</h2>
    <ul>
      <li><strong>WASD</strong> — Movimentação do jogador</li>
      <li><strong>Botão esquerdo do rato</strong> — Disparar projétil</li>
      <li><strong>Botão direito do rato</strong> — Pode ser usado para ações alternativas (reservado para extensões)</li>
      <li><strong>Tecla Espaço</strong> — Trocar o tipo de disparo</li>
    </ul>
  </div>

  <div class="section">
    <h2>🧠 Lógica dos Inimigos</h2>
    <p>Os inimigos são geridos pelo método <code>AdministradorInimigo</code>, que:</p>
    <ul>
      <li>Inicia todos os inimigos com posições e comportamentos definidos</li>
      <li>Atualiza os inimigos com base na posição do jogador, seguindo sempre o mesmo</li>
      <li>Deteta colisões com projéteis e remove inimigos atingidos</li>
      <li>Termina o jogo quando o <strong>jogador colide fisicamente com um inimigo</strong></li>
    </ul>
  </div>

  <div class="section">
    <h2>🧱 Estruturas do Mapa</h2>
    <p>O mapa contém diferentes tipos de estruturas:</p>
    <ul>
      <li><strong>Tiles de base</strong> — Solo do mapa</li>
      <li><strong>Casas</strong> — Estruturas fixas desenhadas por cima do terreno</li>
      <li><strong>Árvores</strong> — Elementos desenhados sem colisão e tem sobreposição ao jogador</li>
    </ul>
    <p>A classe <code>Tilemap</code> gere todas estas estruturas e fornece as <code>bounds</code> do mapa para a lógica de câmara e colisões.</p>
  </div>

  <div class="section">
    <h2>🖱️ Sistema de Input</h2>
    <p>A classe <code>AdministradorInputs</code> gere o estado atual do teclado e rato:</p>
    <ul>
      <li>Deteta direção do jogador com base nas teclas pressionadas</li>
      <li>Garante normalização da direção (para evitar vantagem ao mover na diagonal)</li>
      <li>Deteta clique único do botão esquerdo e direito do rato</li>
      <li>Deteta <code>SpacePressed</code> apenas no frame de transição (solto para pressionado)</li>
    </ul>
  </div>

  <div class="section">
    <h2>📷 Lógica da Câmara</h2>
    <p>O jogo inclui uma <strong>câmara dinâmica</strong> que:</p>
    <ul>
      <li>Segue o jogador a cada frame</li>
      <li>Garante que não sai dos limites do mapa (função <code>ClampToMapBounds</code>)</li>
    </ul>
  </div>

  <div class="section">
    <h2>📦 Organização do Código</h2>
    <p>As classes estão organizadas de forma modular:</p>
    <ul>
      <li><code>AdministradorJogo</code> — Coordena toda a lógica de jogo</li>
      <li><code>Jogador</code> — Gera, atualiza e desenha o jogador</li>
      <li><code>AdministradorInputs</code> — Gere inputs</li>
      <li><code>AdministradorProjetil</code> — Gere projéteis lançados pelo jogador</li>
      <li><code>AdministradorInimigo</code> — Gera e atualiza inimigos</li>
      <li><code>AdministradorHUD</code> — Desenha elementos da interface</li>
      <li><code>Tilemap</code> — Gera e desenha o mapa, casas e árvores</li>
    </ul>
  </div>

  <div class="section">
    <h2>🚧 Funcionalidades Futuras (Ideias)</h2>
    <ul>
      <li>Implementar barra de vida e sistema de dano</li>
      <li>Adicionar múltiplos níveis ou zonas</li>
      <li>Power-ups temporários (velocidade, dano)</li>
      <li>Sons e música ambiente</li>
    </ul>
  </div>

  <div class="section">
    <h2>📎 Notas Técnicas</h2>
    <ul>
      <li>O jogo corre com o motor MonoGame</li>
      <li>Utiliza a biblioteca <code>Microsoft.Xna.Framework</code></li>
      <li>Texturas são carregadas através de <code>Globais.Content</code></li>
      <li>Todos os estados são atualizados a cada <code>Update()</code> do ciclo de jogo</li>
    </ul>
  </div>

  <div class="highlight">
    <strong>👨‍🏫 Projeto criado como parte da UC de Técnicas de Programação. Este jogo visa demonstrar competências em desenvolvimento de jogos, organização modular do código, gestão de inputs e aplicação de lógica de jogo 2D com colisões.</strong>
  </div>
