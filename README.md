# PEC2 - First Person Shooter.

Controls: WASD to move, space to jump.

Objective: Eliminate all the robot patrtols without dying, if your health reaches 0 the game will automatically restart.

Known Issues:
The FSM makes it so when the player shooting script tries to get the enemyAI component of a certain enemy it sometimes retrieves another one,
this causes it to take the HP from a different robot when shooting a certain one, this behaviour is very weird and inconsistent.