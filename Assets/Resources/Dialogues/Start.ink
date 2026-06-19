INCLUDE For_var.ink
INCLUDE globals.ink
INCLUDE Prologue.ink
INCLUDE Prologue_part_2.ink

-> start
=== start ===
    # bg:black
    //Перед началом игры введите имя главной героини.
    Перед началом игры познакомьтесь с главной героиней.
    //# input:player_name
    -> validate_name

=== validate_name ===
    { player_name == "":
        ~ player_name = "Моримукуро"
    }
    Её имя — {player_name}. // поменять на "ваше имя"
    Начать игру?
    + [Продолжить]
        -> prologue
    //+ [Изменить имя]
    //    -> start