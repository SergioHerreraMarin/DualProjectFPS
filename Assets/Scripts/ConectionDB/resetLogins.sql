/*Cuando hacemos pruebas, si hacemos loggin y luego paramos el programa, el usuario
no se le quita el login en la BBDD, solo lo hace si al juego le damos al boton quit
o hacemos cambio de usuario*/

SET SQL_SAFE_UPDATES = 0;
UPDATE profileS SET isLogged = FALSE;
SET SQL_SAFE_UPDATES = 1;

