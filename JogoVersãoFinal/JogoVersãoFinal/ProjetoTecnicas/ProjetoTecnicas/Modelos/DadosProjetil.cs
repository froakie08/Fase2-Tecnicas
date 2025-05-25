using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjetoTecnicas
{
    public sealed class DadosProjetil
    {
        public Vector2 Posicao { get; set; }       // Posição do projétil no mundo
        public float Rotacao { get; set; }         // Rotação do projétil (em radianos)
        public float DuracaoVida { get; set; }     // Tempo de vida restante do projétil (em segundos)
        public int Velocidade { get; set; }        // Velocidade do projétil (unidades por segundo)
    }
}