# PdfService

Este proyecto contiene una clase utilitaria `PdfService` que permite:
- **Renderizar HTML a PDF** usando [Microsoft.Playwright](https://playwright.dev/dotnet/).
- **Convertir HTML a un string JSON** escapando correctamente los caracteres especiales.

Este código está licenciado bajo la **Licencia Apache 2.0**.

---

## Requisitos

- [.NET 6.0 o superior](https://dotnet.microsoft.com/)
- Paquete NuGet: [Microsoft.Playwright](https://www.nuget.org/packages/Microsoft.Playwright/)

Instalación:

```bash
dotnet add package Microsoft.Playwright
```

**Importante**: Después de instalar Playwright en tu proyecto, ejecuta:

```bash
playwright install
```

para instalar los navegadores necesarios.

---

## Uso

### Renderizar HTML a PDF

```csharp
using INSServicios_Cumpleanios.Services;

string htmlContent = "<html><body><h1>Hola, Mundo</h1></body></html>";

byte[] pdfBytes = await PdfService.RenderHtmlToPdfAsync(htmlContent);

// Guardar el PDF a un archivo
await File.WriteAllBytesAsync("salida.pdf", pdfBytes);
```

- El contenido HTML se procesa en un navegador **headless** usando **Chromium**.
- El PDF se genera en formato **A4**, con márgenes personalizados y respetando fondos de color.

---

### Convertir HTML a un JSON String

```csharp
using INSServicios_Cumpleanios.Services;

string html = "<h1>Ejemplo \"Título\"</h1>";
string jsonString = PdfService.ConvertHtmlToJsonString(html);

// Resultado:
// { "html": "<h1>Ejemplo \\\"Título\\\"</h1>" }
```

Esta función:
- Escapa correctamente las comillas (`"`) y las barras (`\`) en el HTML.
- Elimina saltos de línea (`\n`) y retornos de carro (`\r`).
- Devuelve un string en formato JSON que contiene el HTML.

---

## Consideraciones

- `RenderHtmlToPdfAsync` **lanza una instancia de navegador Chromium** en segundo plano. Se recomienda usarlo de manera eficiente en aplicaciones de alta concurrencia.
- `ConvertHtmlToJsonString` **no valida** si el HTML es válido o seguro. Asegúrate de sanitizar el HTML si se obtiene de usuarios externos.



