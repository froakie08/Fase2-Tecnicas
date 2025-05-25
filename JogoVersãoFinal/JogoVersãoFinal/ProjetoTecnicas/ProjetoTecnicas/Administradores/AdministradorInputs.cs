using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetoTecnicas;

public static class AdministradorInputs
{
    private static MouseState _lastMouseState;
    private static KeyboardState _lastKeyboardState;

    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();

    public static bool MouseClicked { get; private set; }
    public static bool MouseRightClicked { get; private set; }
    public static bool MouseLeftDown { get; private set; }
    public static bool SpacePressed { get; private set; }

    public static void Update()
    {
        // Garantir que a direção começa sempre a 0 em cada frame
        _direction = Vector2.Zero;

        var estadoTeclado = Keyboard.GetState();

        // Atualizo a direção com base nas teclas pressionadas (WASD)
        if (estadoTeclado.IsKeyDown(Keys.W)) _direction.Y--;
        if (estadoTeclado.IsKeyDown(Keys.S)) _direction.Y++;
        if (estadoTeclado.IsKeyDown(Keys.A)) _direction.X--;
        if (estadoTeclado.IsKeyDown(Keys.D)) _direction.X++;

        // Se houver movimento, normalizo para evitar que o movimento na diagonal seja mais rápido
        if (_direction != Vector2.Zero)
        {
            _direction.Normalize();
        }

        // Controlo do rato:
        // - MouseLeftDown serve para saber se o botão esquerdo está a ser mantido
        // - MouseClicked deteta o clique (transição de solto para pressionado)
        // - MouseRightClicked deteta clique com o botão direito
        MouseLeftDown = Mouse.GetState().LeftButton == ButtonState.Pressed;
        MouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (_lastMouseState.LeftButton == ButtonState.Released);
        MouseRightClicked = Mouse.GetState().RightButton == ButtonState.Pressed && (_lastMouseState.RightButton == ButtonState.Released);
        _lastMouseState = Mouse.GetState();

        // Detetar quando a tecla de espaço é pressionada (transição de solta para pressionada)
        SpacePressed = _lastKeyboardState.IsKeyUp(Keys.Space) && estadoTeclado.IsKeyDown(Keys.Space);
        _lastKeyboardState = estadoTeclado;
    }
}