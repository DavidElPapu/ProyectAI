FSM VS Arboles de Decisiones

Descripción:
Elegi la IA de Mascota, la cual sigue al jugador todo el tiempo, pero si este se aleja demasiado, lo espera unos segundos a que se acerque para volver a seguirlo, si no, entonces regresa a su casa.

Procesamiento de IA:
La IA tiene 3 acciones principales: Seguir al jugador, esperar al jugador y caminar a su casa. Y sus principales sentidos son la "vista" que es detectar al jugador si esta en su campo de "vision", y la percepción del tiempo el cual espera al jugador

Dificultades o problemas de desarrollo:
Para el SFM, no hubieron tantos problemas durante el desarrollo, fue mas objetos no asignados pero el unico problema que si me tomo mas tiempo solucionar fue el del NavMeshAgent, el cual cuando la mascota entra al estado de "Esperar al jugador" detiene al movimiento del
Agent, pero cuando entra a otro estado que si necesita moverse (Seguir al jugador o ir a casa) la mascota se quedaba quieta porque el Agent seguia con isStopped true, faltaba hacerlo false al entrar a estos otros estados, fue una solución sencilla pero me llevó un buen
rato darme cuenta.

Pequeño analisis de en este caso que arquitectura funciona mejor y por que:

Diagramas:
