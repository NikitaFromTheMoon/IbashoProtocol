INCLUDE For_var.ink
INCLUDE globals.ink
INCLUDE Prologue.ink

=== start ===
    # bg:black
    Перед началом игры введите имя главной героини.
    # input:player_name
    -> validate_name

=== validate_name ===
    { player_name == "":
        ~ player_name = "Моримукуро"
    }
    Ваше имя — {player_name}.
    Начать игру?
    + [Продолжить]
        -> prologue
    + [Изменить имя]
        -> start