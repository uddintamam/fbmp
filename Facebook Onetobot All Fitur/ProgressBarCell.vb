Public Class ProgressBarCell
    Inherits DataGridViewTextBoxCell

    Public Overrides Sub InitializeEditingControl(rowIndex As Integer, initialFormattedValue As Object, dataGridViewCellStyle As DataGridViewCellStyle)
        ' Jangan panggil base class
    End Sub

    Public Overrides ReadOnly Property EditType As Type
        Get
            ' Kembalikan tipe editor yang digunakan untuk mengedit sel
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType As Type
        Get
            ' Kembalikan tipe nilai yang diharapkan dari sel
            Return GetType(Integer)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue As Object
        Get
            ' Set nilai default untuk sel baru
            Return 0
        End Get
    End Property

    Protected Overrides Sub Paint(graphics As Graphics, clipBounds As Rectangle, cellBounds As Rectangle, rowIndex As Integer, cellState As DataGridViewElementStates, value As Object, formattedValue As Object, errorText As String, cellStyle As DataGridViewCellStyle, advancedBorderStyle As DataGridViewAdvancedBorderStyle, paintParts As DataGridViewPaintParts)
        ' Gambar background putih di dalam sel
        graphics.FillRectangle(Brushes.White, cellBounds)

        ' Gambar ProgressBar di dalam sel
        Dim progressValue As Integer = CInt(value)
        Dim progressBarBounds As New Rectangle(cellBounds.X + 2, cellBounds.Y + 2, cellBounds.Width - 4, cellBounds.Height - 4)

        ' Tentukan warna progress bar berdasarkan kondisi
        Dim progressBarColor As Color = If(progressValue >= 100, Color.Green, SystemColors.Highlight)

        ' Gambar progress bar
        Using progressBarBrush As New SolidBrush(progressBarColor)
            ProgressBarRenderer.DrawHorizontalBar(graphics, progressBarBounds)
            Dim progressBounds As New Rectangle(cellBounds.X + 4, cellBounds.Y + 4, CInt((progressBarBounds.Width - 8) * (progressValue / 100)), progressBarBounds.Height - 8)
            ProgressBarRenderer.DrawHorizontalChunks(graphics, progressBounds)
            graphics.FillRectangle(progressBarBrush, progressBounds)
        End Using

        ' Tentukan warna teks berdasarkan kondisi
        Dim textColor As Color = If(progressValue >= 100, Color.White, Color.Black)

        ' Tampilkan teks di tengah dan di atas progress bar
        Dim textBounds As New Rectangle(progressBarBounds.X, progressBarBounds.Y, progressBarBounds.Width, progressBarBounds.Height)
        Dim text As String = progressValue & "%"
        Dim textFormat As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Using textBrush As New SolidBrush(textColor)
            graphics.DrawString(text, cellStyle.Font, textBrush, textBounds, textFormat)
        End Using
    End Sub
End Class
