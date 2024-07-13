# Identificar razón reinicio Windows

Para identificar la razón por la cual tu sistema Windows se reinició inesperadamente, puedes seguir estos pasos:

### 1. Revisar el Visor de Eventos
El Visor de Eventos de Windows almacena información detallada sobre eventos del sistema, incluidas las razones de los reinicios.

1. **Abrir el Visor de Eventos:**
   - Presiona `Win + R` para abrir el cuadro de diálogo Ejecutar.
   - Escribe `eventvwr.msc` y presiona `Enter`.

2. **Navegar a los Registros del Sistema:**
   - En el panel izquierdo, expande `Registros de Windows` y selecciona `Sistema`.

3. **Filtrar los Eventos de Interés:**
   - En el panel derecho, haz clic en `Filtrar registro actual`.
   - En la lista desplegable `Registro de eventos`, selecciona `Sistema`.
   - En la lista `Id. de evento`, escribe `6008` (este ID indica que el sistema se cerró inesperadamente) y `41` (indica un reinicio del sistema sin apagado limpio).

4. **Revisar los Eventos:**
   - Busca eventos con los IDs especificados para obtener más detalles sobre el reinicio. 

### 2. Revisar el Historial de Confiabilidad
El Monitor de Confiabilidad proporciona una visión cronológica de los eventos importantes del sistema, incluidos los fallos y reinicios.

1. **Abrir el Monitor de Confiabilidad:**
   - Presiona `Win + R` para abrir el cuadro de diálogo Ejecutar.
   - Escribe `perfmon /rel` y presiona `Enter`.

2. **Revisar los Eventos:**
   - En la vista gráfica, busca los días con errores (indicados por una 'X' roja).
   - Haz clic en los eventos para obtener detalles sobre los problemas y los posibles reinicios.

### 3. Revisar el Registro de Sistema
Revisar el archivo de registro puede proporcionar información adicional sobre los reinicios.

1. **Abrir el Explorador de Archivos:**
   - Navega a `C:\Windows\System32\winevt\Logs`.

2. **Buscar el Archivo:**
   - Busca el archivo `System.evtx` y ábrelo con el Visor de Eventos.

### 4. Configurar Opciones de Inicio y Recuperación
Asegúrate de que Windows esté configurado para no reiniciarse automáticamente en caso de error, lo que te permitirá ver la pantalla azul de error y obtener más detalles.

1. **Abrir Configuración del Sistema:**
   - Haz clic derecho en `Este equipo` y selecciona `Propiedades`.
   - Haz clic en `Configuración avanzada del sistema`.

2. **Configurar Opciones de Inicio y Recuperación:**
   - En la pestaña `Avanzado`, haz clic en `Configuración` bajo `Inicio y recuperación`.
   - Desmarca la opción `Reiniciar automáticamente`.

### 5. Análisis de BlueScreenView
Si hubo una pantalla azul de la muerte (BSOD), puedes utilizar herramientas como BlueScreenView para analizar los archivos de volcado de memoria.

1. **Descargar e Instalar BlueScreenView:**
   - Ve al sitio web de NirSoft y descarga BlueScreenView.

2. **Analizar los Archivos de Volcado:**
   - Ejecuta BlueScreenView y revisa los archivos de volcado para identificar el código de error y el controlador implicado.

### 6. Revisión de Hardware
Si los pasos anteriores no indican una causa clara, considera revisar el hardware:

1. **Verificar Temperaturas:** Usa herramientas como `HWMonitor` para revisar si hay problemas de sobrecalentamiento.
2. **Pruebas de Memoria:** Usa `MemTest86` para verificar la RAM.
3. **Comprobación de Disco:** Usa `chkdsk` y otras herramientas de diagnóstico de disco para asegurar la integridad del disco duro.

Siguiendo estos pasos, deberías poder identificar la razón del reinicio inesperado de tu sistema Windows.

### Script de PowerShell para Revisar Eventos del Sistema

```powershell
# Define los IDs de eventos relevantes
$eventIDs = @(41, 6008)

# Obtén los eventos del sistema relacionados con los IDs especificados
$events = Get-WinEvent -FilterHashtable @{LogName='System'; Id=$eventIDs} | Select-Object TimeCreated, Id, Message

# Muestra los eventos en una tabla
$events | Format-Table -AutoSize
```

### Pasos para Ejecutar el Script

1. **Abrir PowerShell como Administrador:**
   - Haz clic derecho en el botón de inicio y selecciona `Windows PowerShell (Admin)`.

2. **Ejecutar el Script:**
   - Copia y pega el script en la ventana de PowerShell y presiona `Enter`.

### Explicación del Script

- `Get-WinEvent`: Cmdlet usado para obtener eventos de Windows.
- `-FilterHashtable @{LogName='System'; Id=$eventIDs}`: Filtra los eventos del registro de sistema con los IDs 41 (reinicio inesperado) y 6008 (apagado inesperado).
- `Select-Object TimeCreated, Id, Message`: Selecciona las propiedades `TimeCreated` (fecha y hora del evento), `Id` (ID del evento) y `Message` (mensaje del evento).
- `Format-Table -AutoSize`: Muestra los eventos en una tabla ajustando automáticamente el tamaño de las columnas.

### Ejemplo de Salida

El script producirá una salida similar a la siguiente:

```
TimeCreated           Id  Message
-----------           --  -------
7/12/2024 12:34:56 PM 41  The system has rebooted without cleanly shutting down first...
7/12/2024 12:20:34 PM 6008 The previous system shutdown was unexpected...
```

Esta salida te permitirá ver rápidamente cuándo ocurrieron los reinicios y leer los mensajes asociados para obtener más detalles sobre la causa del problema.

### Filtrado por Fecha (Opcional)

Si deseas filtrar los eventos por una fecha específica o un rango de fechas, puedes modificar el script así:

```powershell
# Define los IDs de eventos relevantes
$eventIDs = @(41, 6008)

# Define el rango de fechas
$startDate = (Get-Date).AddDays(-7) # Últimos 7 días
$endDate = Get-Date

# Obtén los eventos del sistema relacionados con los IDs especificados y el rango de fechas
$events = Get-WinEvent -FilterHashtable @{LogName='System'; Id=$eventIDs; StartTime=$startDate; EndTime=$endDate} | Select-Object TimeCreated, Id, Message

# Muestra los eventos en una tabla
$events | Format-Table -AutoSize
```

Este script filtra los eventos para mostrar solo los ocurridos en los últimos 7 días. Puedes ajustar `$startDate` y `$endDate` según tus necesidades.

Espero que esto te sea de ayuda para identificar la causa del reinicio de tu sistema Windows.