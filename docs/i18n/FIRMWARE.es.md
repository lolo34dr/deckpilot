> Esta es una traducción automática de la documentación completa del firmware. El original inglés es la referencia. Mandos y identificadores se conservan exactamente.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Para la configuración de hardware probada solamente: **Pro Micro ATmega32U4, 5 V / 16 MHz**, usando el **Arduino Leonardo** perfil de la tabla y **Caterina / AVR109** bootloader. USB-C describe el conector, no compatibilidad con la tabla. El boceto original del propietario se subió con éxito con el 1200- reajuste de baluarte y AVR109 en 57600 baud. El propietario también ha confirmado éxito en la aplicación flashing, firmware 1.0.0 identificación y funcionamiento de todos 12 botones en esa tabla de referencia.

<a id="wiring-and-behavior"></a>
## Cableado y comportamiento

Cada interruptor conecta un pin a GND. Se activan los pull-ups internos. **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** Enviar **F13 a F24** en ese orden, un establo 30 ms debounce maneja presionar y soltar de forma independiente. Comportamiento de teclado, perfiles de Windows y calibración siguen siendo compatibles; la rotación de teclado USB sigue siendo limitada por la biblioteca de teclado de Arduino. El firmware no introduce modos USB per-button ni controles de medios nativos todavía.

Pin **1** mantenido a GND en la operación de teclado desactivado de inicio, preservando el seguro de fallo original. El núcleo USB todavía puede soportar la entrada de arranque. No conectar VCC a GND.

La interfaz de serie USB CDC responde `DECKPILOT?\n` a 115200 baud con:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

No se requiere conexión en serie para el funcionamiento normal del teclado. La identidad es un cheque de compatibilidad, no autenticación de hardware criptográfico. Windows sigue informando de los identificadores USB Arduino Leonardo; muchas otras tablas comparten esos identificadores.

<a id="install-from-deckpilot"></a>
## Instalar desde DeckPilot

1. Mantenga el boceto de recuperación suministrado (`firmware/Original/Original.ino`) y una configuración de carga Arduino funcionando como una opción de recuperación. Mantenga RST y GND físicamente accesible.
2. Cerrar Arduino IDE, Serial Monitor y otras herramientas de serie. Desconectar otras tablas Leonardo.
3. Abierto **Firmware du macropad…**, elija el dispositivo correcto y confirme el tablero y el cableado. Un boceto original no identificado se puede instalar sólo después de la confirmación explícita.
4. Confirme la instalación y deje el cable USB conectado hasta la finalización. DeckPilot mientras que el diálogo de firmware está abierto; los eventos ordinarios F13–F24 todavía llegan a Windows.

El firmware y Windows AVRDUDE 8.0 se agrupan dentro de la aplicación publicada. No Arduino IDE o instalación de controlador independiente se realiza por DeckPilot. Windows ya debe exponer el puerto de serie USB de la junta, como lo hizo para la carga exitosa del propietario. Nuevas versiones de firmware se entregan con actualizaciones de aplicaciones de GitHub; actualización de la aplicación lo hace **no** hardware flash automáticamente. Cada flash requiere confirmación.

DeckPilot cheques SHA-256, Comprobaciones de registro Intel HEX, presencia de reinicio-vector, superposiciones y las **28672- Adiós.** frontera de la aplicación antes del reset. Concuerda con la ubicación física USB a través de los puertos de tiempo de ejecución y de arranque, utiliza AVR109 en 57600 baud, requiere la verificación de escritura de AVRDUDE, a continuación, comprueba la identidad y versión del firmware en ejecución. No escribe fusibles, EEPROM o el cargador de arranque. `2341:8036` / `2A03:8036` y Caterina `2341:0036` / `2A03:0036` son aceptados. Si Windows no puede proporcionar una conexión física coincidente, el flashing está bloqueado en lugar de adivinar un puerto.

El flashing no es atómico: una desconexión de cable o pérdida de potencia puede dejar el firmware de la aplicación incompleto. No hay copia de seguridad de firmware automático o retroceso. Si es necesario, vuelva a cargar el boceto original con Arduino IDE establecido para **Arduino Leonardo**. Dependiendo del cargador de arranque del tablero, conectar brevemente RST a GND dos veces puede exponer un puerto de arranque temporal; subir mientras que ese puerto está disponible. Un descargador de arranque dañado requiere hardware ISP y no puede ser reparado por este actualizador USB. No escriba una variante de tablero o reloj diferente.

El último registro de proceso AVRDUDE completado se almacena en `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## Construido

En Windows, desde la raíz del repositorio, instale Arduino CLI **1.2.2** y .NET SDK 8, entonces corre:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

El script pins Arduino AVR core **1.8.6**, Teclado **1.0.6**, y aguas arriba AVRDUDE **8.0**. Los paquetes gestionados por CLI utilizan la validación del paquete de Arduino; AVRDUDE binario/source archives have explicit SHA-256 Pins en el guión. `artifacts/firmware/DeckPilot-firmware.zip` contiene el HEX, manifiesto, herramienta de Windows, licencia y el correspondiente archivo de fuente de AVRDUDE aguas arriba. La publicación CI requiere el paquete; un desarrollador ordinario construir sin que pueda ejecutar la aplicación pero no puede flash.

El original 12- el boceto de botones fue adaptado de David Madison **Flujo barato** proyecto, derechos de autor 2018, MIT. Su aviso permanece en `DeckPilot.ino`. DeckPilot Las adiciones usan la licencia del repositorio MIT. **AVRDUDE es software separado bajo GPL-2.0- O-later**, con su aviso/licencia y fuente empaquetados; la licencia del repositorio MIT no sustituye esa licencia. https://github.com/avrdudes/avrdude/tree/v8.0 for upstream build instructions and dependncy notices.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 y protocolo USB 2

Firmware 1.1.1 añade un enlace de diagnóstico de sólo lectura mientras retiene la misma asignación de teclado F13–F24 y pin-1 startup failsafe. **La prueba física del propietario cubrió el firmware 1.0.0. Firmware 1.1.1 tiene pruebas de protocolo automatizadas y una construcción AVR, pero todavía necesita su primera prueba en la tabla real.** Los perfiles y calibración permanecen en el PC y no necesitan ser recreados para esta actualización de firmware.

La nueva aplicación identifica la tarjeta conectada, mantiene su puerto serie abierto, vuelve a conectarse después de desconectar, y comprueba su versión de firmware al inicio y después de la reconexión. Actualizaciones de firmware se entregan con versiones de aplicaciones: cheques de arranque comparan el firmware conectado con el paquete validado incluido en la aplicación instalada. Los cheques de actualización de la aplicación todavía obtienen nuevas versiones de GitHub.

Un firmware detectado más viejo requiere una vez por sesión de aplicación, con un 24- horas de retraso de recordatorio cuando se pospone. **Connexion et diagnostic**. Desconocidos bocetos Arduino, dispositivos desconectados y selecciones ambiguas de múltiples tarjetas no activan los impulsos automáticos de flashización o actualización. La instalación de firmware siempre requiere confirmación. Inauguración del administrador de firmware suspende el monitor y espera que el puerto de serie sea liberado; monitoreo se reanudará después de que el diálogo cierre.

**Para una carga manual de Arduino IDE, renuncie DeckPilot desde su icono de bandeja primero.** El cierre de la ventana principal sólo lo oculta, por lo que sigue siendo dueño de la conexión en serie. DeckPilot y otro monitor de serie no puede utilizar el mismo puerto simultáneamente. Si otra herramienta ya posee el puerto, la aplicación reporta el problema y retries sin afectar las teclas de teclado ordinario.

El nuevo `DeckPilotProtocol.h` es la misma aplicación ejercida por `tests/firmware-protocol.cpp`. Utiliza buffers de tamaño fijo, procesamiento de entrada enlazado y espacio de transmisión USB disponible; el escaneo de botones nunca se retrasa deliberadamente mientras espera un host. La última máscara de botón pendiente se coalesce bajo la presión trasera. No hay comandos para ejecutar macros, escribir EEPROM, deshabilitar el teclado, modificar perfiles o firmware flash sobre este protocolo de diagnóstico.

Atentos 115200 baud, las líneas terminan con LF (CRLF es aceptado). Las fichas de sesión contienen exactamente ocho caracteres hexadecimal mayúsculas. Todos los números son decimales sin firmar. Ejemplos:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` anuncios 12 claves y bits de la capacidad: 1 = latido del corazón / instantánea del estado, 2 = eventos de estado clave. `PONG` se hace eco del mostrador de solicitud, luego reporta tiempo de inactividad en milisegundos, secuencia del estado y el 12- máscara de botón de bit. `KEYS` informes secuencia y máscara; bit 0 es F13 y bit 11 es F24, independiente de la calibración física. Las sesiones y los contadores de solicitud rechazan las respuestas definitivas. 32-bit wraparound. Las claves siguen siendo datos de diagnóstico solamente: las acciones de la aplicación todavía se ejecutan una vez a través de la ruta del teclado existente, por lo que un evento en serie no puede ejecutar la misma acción dos veces.

Las solicitudes de Heartbeat se envían normalmente cada dos segundos. La aplicación se reconecta si falta una respuesta coincidente para 1.5 segundos; el firmware detiene diagnósticos no solicitados después de seis segundos sin un latido del corazón o cuando la conexión serial se cierra. Firmware 1.0.0 permanece apoyado a través de la consulta de identidad heredada, sin diagnóstico clave en vivo. DeckPilot aplicaciones todavía pueden leer la identidad heredada de firmware 1.1.1.

La versión fuente en `DeckPilotProtocol.h` suministra la versión de manifiesto de liberación. Los paquetes de firmware publicados ahora también contienen el boceto / encabezado actual bajo `source/DeckPilot`, el boceto de recuperación y el DeckPilot Licencia de MIT.

El enlace de diagnóstico utiliza deliberadamente `Serial.dtr()` en lugar de `bool(Serial)`: Arduino AVR core 1.8.6 insertar a 10 ms retraso en la `Serial_::operator bool()`. Reading DTR guarda directamente la ruta de diagnóstico de añadir esa pausa a cada ciclo clave-escan. La prueba del protocolo host elimina la conversión implícita del bool para que este patrón de bloqueo no pueda ser reintroducido en la implementación del protocolo compartido sin darse cuenta.
