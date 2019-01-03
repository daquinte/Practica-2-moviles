# Practica-2-moviles
Esto es un juego para Android creado en Unity por Daniel Pérez Luque y Daniel Quintero Bernal.

Es un clon del juego Bricks and Balls de -Cheetah games-.
Los niveles se superan realizando disparos desde abajo. Para cada uno, el jugador selecciona una dirección, y múltiples bolas serán enviadas desde una determinada posición
en el borde inferior de la pantalla siguiendo la dirección especificada. Las bolas rebotarán y golpearán a los bloques hasta que terminen cayendo de vuelta abajo del todo. La primera
bola que alcanza el límite inferior determina la posición exacta desde la que saldrán las bolas del siguiente disparo, y según van cayendo las 
demás se concentrarán en ese punto.
Tras cada disparo los bloques que no hayan sido destruídos descienden una posición. Si descienden mucho, el jugador pierde la partida

###### Requisitos de implementacion

- [x] Adaptar correctamente la visualización en pantalla en función de la relación de aspecto (ancho-alto) del dispositivo.
- [x] Leer los mapas de ficheros de texto.
- [ ] Incluir al menos un tipo de bloque especial
- [x] Mantener el progreso del usuario (niveles desbloqueados) de tal forma que sea difícil romperlo y modificarlo externamente accediendo a los ficheros en el dispositivo.
- [ ] Incorporar al menos un potenciador. Para ello será necesario incorporar monedavirtual, por lo que también deberá guardarse, de forma segura, la cantidad de rubíes
      acumulados. El modo de conseguir esos rubíes puede escogerse como se desee.
- [ ]Mostrar anuncios al usuario (con UnityAds) para monetizar el juego.
