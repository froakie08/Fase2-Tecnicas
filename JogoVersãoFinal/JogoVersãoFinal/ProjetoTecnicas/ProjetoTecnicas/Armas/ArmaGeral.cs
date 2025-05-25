using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTecnicas;

// Classe abstrata que define o comportamento base de qualquer arma
public abstract class ArmaGeral
{
    protected float cooldown;             // Tempo de espera entre disparos
    protected float cooldownLeft;         // Tempo restante até poder disparar novamente
    protected int maxAmmo;                // Capacidade máxima de munição da arma
    public int Ammo { get; protected set; } // Munição atual disponível
    protected float reloadTime;           // Tempo necessário para recarregar a arma
    public bool Recarregando { get; protected set; } // Indica se a arma está a recarregar

    // Construtor base que inicia o estado da arma
    protected ArmaGeral()
    {
        cooldownLeft = 0f;
        Recarregando = false;
    }

    // Método que inicia o processo de recarregamento, se necessário
    public virtual void Recarregar()
    {
        if (Recarregando || (Ammo == maxAmmo)) return; // Se já está a recarregar ou se a munição está cheia, não faz nada
        cooldownLeft = reloadTime; // Inicia o tempo de recarregamento
        Recarregando = true;
        Ammo = maxAmmo; // Restaura a munição ao valor máximo
    }

    // Método abstrato que cada arma concreta deve implementar para criar projéteis
    protected abstract void CriarProjecteis(Jogador jogador);

    // Método que tenta disparar a arma, se estiver pronta
    public virtual void Disparar(Jogador jogador)
    {
        if (cooldownLeft > 0 || Recarregando) return; // Se está em cooldown ou a recarregar, não dispara

        Ammo--; // Reduz a munição

        if (Ammo > 0)
        {
            cooldownLeft = cooldown; // Reinicia o tempo de espera entre disparos
        }
        else
        {
            Recarregar(); // Se não houver mais munição, inicia o recarregamento
        }

        CriarProjecteis(jogador); // Cria os projéteis associados ao disparo

        if (Globais.SomDisparo != null)
        {
            Globais.SomDisparo.Play(); // Toca o som do disparo, se estiver definido
        }
    }

    // Atualiza o estado da arma, nomeadamente o tempo de cooldown e recarregamento
    public virtual void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Globais.TotalSeconds; // Reduz o tempo restante até novo disparo
        }
        else if (Recarregando)
        {
            Recarregando = false; // Termina o recarregamento se o tempo tiver passado
        }
    }
}