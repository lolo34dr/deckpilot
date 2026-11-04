> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 1.6.4 fija el reemplazo de búsqueda-result durante el arrastrar y soltar y estabiliza la verificación de Windows.

- Mantenga los botones de búsqueda existentes cuando el temporizador de debounce o el catálogo instalado-app produce los mismos resultados, por lo que una fuente de arrastre no se desprende inesperadamente.
- Diferir el reemplazo de búsqueda-result mientras se está en curso una interacción con el ratón o la arrastre OLE.
- Windows UI comprueba el uso de coordenadas de cursor físico, controles de enfoque antes de medirlos, y escanear el rango de desplazamiento completo en lugar de asumir un número fijo de aumentos de la línea.
- La configuración de Windows guarda el uso de la API de sustitución atómica dedicada para que un lector existente pueda conservar su vieja instantánea sin bloquear el siguiente ahorro. Una regresión de núcleo verifica tres sustituciones sucesivas con un lector compartido mantenido abierto.
- Los lectores de configuración en las pruebas de Windows usan UTF-8 y permitir la sustitución de archivos atómicos. Apertura de instantáneas espera hasta 500 ms sólo para las violaciones de Windows compartido/bloqueo durante el reemplazo; otros errores de lectura y bloqueos persistentes fallan la prueba. Tres ciclos de deshacer/redo también mantienen una vieja instantánea abierta para verificar que ahorra éxito sin modificar los lectores existentes.
- Los controles de arranque de fondo esperan que WPF termine de ocultar su ventana, y luego requieren que el proceso siga funcionando sin una ventana principal visible.
- Actualizar GitHub Acciones a Node.js 24 versiones e instalar el mismo Arduino CLI 1.2.2 de su archivo de checksum-verified upstream.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan perfiles y calibración. No se necesita un flash de firmware.
