# Practica-2-moviles
Esto es un juego para Android creado en Unity por Daniel Pérez Luque y Daniel Quintero Bernal.

Es un clon del juego Bricks and Balls de -Cheetah games-.
Los niveles se superan realizando disparos desde abajo. Para cada uno, el jugador selecciona una dirección, y múltiples bolas serán enviadas desde una determinada posición
en el borde inferior de la pantalla siguiendo la dirección especificada. Las bolas rebotarán y golpearán a los bloques hasta que terminen cayendo de vuelta abajo del todo. La primera
bola que alcanza el límite inferior determina la posición exacta desde la que saldrán las bolas del siguiente disparo, y según van cayendo las 
demás se concentrarán en ese punto.
Tras cada disparo los bloques que no hayan sido destruídos descienden una posición. Si descienden mucho, el jugador pierde la partida

###### Requisitos de recuperación

- [ ] No tenéis anuncios, que era una de los requisitos de la práctica. (Están, falta recompensas)
- [X] El emisor de bolas debería desaparecer durante un disparo (hasta que se vuelve a situar con la primera bola que cae).
- [ ] Si las bolas se quedan totalmente en horizontal no se puede seguir. -> ¿Marcar el boton tras n segundos?
- [X] El disparo de las bolas es incorrecto. Sale una primera y luego todas las demás de golpe demasiado seguidas (y a distinta        velocidad que la primera). Había que usar el fixed update. (Está pero sin Fixed Update)
- [ ] Se notifica que el nivel se ha completado antes de que todas las bolas lleguen abajo.
- [ ] Las etiquetas del número de golpeos pendientes de los bloques no quedan bien colocadas.
- [ ] El disparador es extraño. A veces no detecta que se suelta, y se queda la línea activada erróneamente.
- [X] Se pierde antes de tiempo. La fila justo por encima del punto de disparo es válida para los bloques.
- [ ] El ancho del tablero es mayor que el del juego original. Si una fila está llena de tiles, no debía haber hueco en los laterales para que las bolas entraran.
- [X] El número de bolas que se dispara es muy bajo.
- [ ] El cálculo de la dirección de disparo no es muy fino. La línea no sale en el punto de pulsación.
- [X] El GUI de selección de nivel está hecho a mano. Con los mapas en datos que pueden crecer arbitrariamente, poner una restricción así es muy mala idea (aparte de que el GUI es un caos y no se sabe cuáles están bloqueados y cuáles no).
- [X] No deberíais tener ya mensajes de log.
- [X] Las bolas van demasiado lentas…
- [X] Los rayos NO CAEN en el tablero. ¿¿¡¡Por qué!!?? Ocurre que se solapan rayos con bloques cuando estos caen.
- [ ] Cuando se pulsa sobre ""Comprar power up"" se activa el disparo…
- [X] No es bueno avisar de los singleton. ¿Seguro que necesitáis tantos?
- [X] PowerUpLaserVertical y PowerUpLaserHorizontal son iguales salvo por una pequeñez. Debería haber sido una única clase.
- [X] Se debe minimizar el uso de Update() y FixedUpdate(). Dejar esos métodos vacíos es muy mal síntoma. 
