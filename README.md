# App Nativa WinUI 3 (Windows App SDK)

Esta es la versión nativa de tu aplicación desarrollada en **C# y XAML** usando el framework oficial **WinUI 3 (Windows App SDK)**.

## Ventajas Clave Implementadas

1. **Posicionamiento de Clic 100% Preciso**: El cursor (caret) se coloca exactamente donde haces clic gracias al motor nativo **DirectWrite** y **TextServicesFramework (TSF)** de Windows.
2. **Scroll Ultra Fluido**: Renderizado mediante la GPU de Windows a 60 fps / 120 fps sin tirones ni retardo.
3. **Alto Rendimiento y Bajo Consumo de RAM**: Ejecución directa en código nativo sin navegador pesado.
4. **Indicadores en Tiempo Real**: Muestra posición del cursor (Línea:Columna), total de palabras, caracteres y opciones de personalización de fuente y ajuste de línea.

---

## Cómo Compilar y Ejecutar

### Opción A: Con Visual Studio 2022 (Recomendado)
1. Abre **Visual Studio 2022**.
2. Selecciona **Abrir un proyecto o una solución** y abre el archivo `WinUI3App.csproj`.
3. Presiona `F5` o el botón **Iniciar** para ejecutar en modo nativo x64.

### Opción B: Desde la Consola de Comandos (.NET SDK)
1. Asegúrate de tener instalado el **.NET 8 SDK** (puedes descargarlo desde [dotnet.microsoft.com](https://dotnet.microsoft.com/download)).
2. Abre la consola en esta carpeta (`WinUI3App`) y ejecuta:
   ```powershell
   dotnet build
   dotnet run
   ```
