using Microsoft.Playwright;

namespace INSServicios_Cumpleanios.Services
{
    public static class PdfService
    {
        public static async Task<byte[]> RenderHtmlToPdfAsync(string htmlContent)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();

            // Establecer contenido asegurando que la red esté quieta
            await page.SetContentAsync(htmlContent, new PageSetContentOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle
            });

            // Generar PDF con márgenes personalizados
            var pdfBytes = await page.PdfAsync(new PagePdfOptions
            {
                Format = "A4",
                PrintBackground = true, // ¡Importante! Para que se respeten los colores de fondo
                Margin = new Margin
                {
                    Top = "20mm",
                    Bottom = "20mm",
                    Left = "15mm",
                    Right = "15mm"
                },
                Scale = 1.0f // Escala normal
            });

            return pdfBytes;
        }


        public static string ConvertHtmlToJsonString(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new ArgumentException("HTML content cannot be null or empty.", nameof(html));

            // Escapar las comillas
            string escapedHtml = html
                .Replace("\\", "\\\\") // Primero, escapa las barras
                .Replace("\"", "\\\"")  // Luego, escapa las comillas
                .Replace("\r", "")      // Elimina retornos de carro
                .Replace("\n", "");     // Elimina saltos de línea

            // Formatear como JSON
            string jsonString = $"{{ \"html\": \"{escapedHtml}\" }}";

            return jsonString;
        }

    }
}
