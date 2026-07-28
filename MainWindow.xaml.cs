using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace WinUI3App
{
    /// <summary>
    /// Ventana principal de la aplicación WinUI 3 demostrando el motor de texto nativo con scroll fluido y posicionamiento de cursor preciso.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Cargar texto inicial de demostración al iniciar
            LoadDemoContent();
        }

        #region Eventos de Texto y Posicionamiento de Cursor (Caret)

        /// <summary>
        /// Se dispara cada vez que el usuario hace clic en el texto o mueve la selección.
        /// WinUI 3 coloca el cursor (caret) de forma exacta y calculada directamente por Windows.
        /// </summary>
        private void NativeTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateCursorPosition(NativeTextBox.Text, NativeTextBox.SelectionStart);
        }

        private void NativeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStatistics(NativeTextBox.Text);
            UpdateCursorPosition(NativeTextBox.Text, NativeTextBox.SelectionStart);
        }

        private void NativeRichEditBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Para el RichEditBox nativo
            NativeRichEditBox.Document.GetText(Microsoft.UI.Text.TextGetOptions.UseObjectText, out string text);
            UpdateStatistics(text);
        }

        private void NativeRichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            NativeRichEditBox.Document.GetText(Microsoft.UI.Text.TextGetOptions.UseObjectText, out string text);
            UpdateStatistics(text);
        }

        /// <summary>
        /// Calcula en tiempo real la línea y columna exactas donde el usuario hizo clic en el texto.
        /// </summary>
        private void UpdateCursorPosition(string fullText, int caretIndex)
        {
            if (string.IsNullOrEmpty(fullText) || caretIndex < 0)
            {
                CursorPosText.Text = "Línea: 1, Columna: 1";
                return;
            }

            // Asegurar que el índice esté dentro del rango del texto
            int validCaretIndex = Math.Min(caretIndex, fullText.Length);

            // Calcular número de salto de línea '\n' antes del cursor
            int line = 1;
            int lastNewLineIndex = -1;

            for (int i = 0; i < validCaretIndex; i++)
            {
                if (fullText[i] == '\n')
                {
                    line++;
                    lastNewLineIndex = i;
                }
            }

            int column = validCaretIndex - lastNewLineIndex;

            CursorPosText.Text = $"Línea: {line}, Columna: {column} (Índice: {validCaretIndex})";
        }

        /// <summary>
        /// Actualiza estadísticas de rendimiento de texto (Palabras y Caracteres).
        /// </summary>
        private void UpdateStatistics(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                StatsText.Text = "0 palabras | 0 caracteres";
                return;
            }

            int charCount = text.Length;

            // Contar palabras limpiando espacios y saltos de línea
            char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
            int wordCount = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

            StatsText.Text = $"{wordCount:N0} palabras | {charCount:N0} caracteres";
        }

        #endregion

        #region Personalización de Fuentes y Estilo de Texto

        private void OnFontFamilyChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyCombo?.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content is string fontFamilyName)
            {
                FontFamily font = new FontFamily(fontFamilyName);
                if (NativeTextBox != null) NativeTextBox.FontFamily = font;
                if (NativeRichEditBox != null) NativeRichEditBox.FontFamily = font;
            }
        }

        private void OnFontSizeChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if (!double.IsNaN(args.NewValue) && args.NewValue >= 8 && args.NewValue <= 72)
            {
                if (NativeTextBox != null) NativeTextBox.FontSize = args.NewValue;
                if (NativeRichEditBox != null) NativeRichEditBox.FontSize = args.NewValue;
            }
        }

        private void OnWordWrapToggled(object sender, RoutedEventArgs e)
        {
            bool isWrapped = WordWrapToggle.IsChecked ?? true;
            TextWrapping wrapMode = isWrapped ? TextWrapping.Wrap : TextWrapping.NoWrap;

            if (NativeTextBox != null) NativeTextBox.TextWrapping = wrapMode;
            if (NativeRichEditBox != null) NativeRichEditBox.TextWrapping = wrapMode;
        }

        private void OnModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeSelector == null || NativeTextBox == null || NativeRichEditBox == null) return;

            if (ModeSelector.SelectedIndex == 0)
            {
                // Modo TextBox
                NativeTextBox.Visibility = Visibility.Visible;
                NativeRichEditBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Modo RichEditBox
                NativeTextBox.Visibility = Visibility.Collapsed;
                NativeRichEditBox.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Acciones de Botones y Demostración de Texto Masivo

        private void OnClearTextClicked(object sender, RoutedEventArgs e)
        {
            NativeTextBox.Text = string.Empty;
            NativeRichEditBox.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, string.Empty);
        }

        private void OnLoadDemoTextClicked(object sender, RoutedEventArgs e)
        {
            LoadDemoContent();
        }

        private void LoadDemoContent()
        {
            string demoText = @"================================================================================
DEMOSTRACIÓN DE MOTOR DE TEXTO NATIVO WINUI 3 (WINDOWS APP SDK)
================================================================================

¿Por qué este control responde tan bien al hacer clic en el texto?
--------------------------------------------------------------------------------
A diferencia de las aplicaciones web empaquetadas (como Electron o Canvas custom), 
esta aplicación es 100% Nativa de Windows. 

Usa DirectWrite y el Text Services Framework (TSF) del sistema operativo Windows:
1. Precisión de clic (Hit-Testing): Al hacer clic en cualquier lugar entre dos letras, 
   Windows calcula con resolución de subpíxel dónde ubicar la barra de cursor (caret).
2. Scroll Ultra Fluido: El renderizado y el desplazamiento (scroll) están acelerados por GPU 
   utilizando la API de Composición de Windows a 60 fps / 120 fps sin tirones.
3. Consumo Eficiente: Requiere una fracción diminuta de la memoria RAM del sistema.

Pruébalo tú mismo:
--------------------------------------------------------------------------------
- Haz clic rápido en cualquier palabra o entre caracteres de este párrafo.
- Usa las teclas de dirección (flechas), Inicio, Fin, Ctrl+Flecha para navegar.
- Selecciona fragmentos de texto usando el ratón o Shift+Flecha.
- Cambia la tipografía arriba (Cascadia Code, Consolas, Segoe UI Variable).
- Desactiva el 'Ajuste de línea' para probar el scroll horizontal nativo.

Párrafo de prueba de rendimiento con scroll masivo:
" + string.Join("\r\n", Enumerable.Range(1, 100).Select(i => $"Línea {i:D3}: Esta es una línea de prueba nativa en WinUI 3 ejecutándose a la máxima velocidad en tu GPU de Windows. Puedes hacer clic exactamente en cualquier caracter."));

            NativeTextBox.Text = demoText;
            NativeRichEditBox.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, demoText);
        }

        #endregion
    }
}
