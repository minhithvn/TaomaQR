Public Class Form1
    ' Private WithEvents pd As New Printing.PrintDocument
    ' Private ppd As New PrintPreviewDialog

    Private QR_Image As Bitmap
    Private Function Tao_QR_Code(Data As String, Size As Size)
        Try
            Dim web As New System.Net.WebClient()
            Dim url As String = "http://chart.googleapis.com/chart?chs="
            url &= Size.Width.ToString() & "x" & Size.Height.ToString()
            url &= "&cht=qr&chl=" & System.Uri.EscapeDataString(Data)
            'MsgBox(url)
            Dim bytes As Byte() = web.DownloadData(url)
            Dim bmp As Bitmap = Nothing
            Using mem As New IO.MemoryStream(bytes)
                bmp = Bitmap.FromStream(mem)
            End Using
            Return bmp
        Catch ex As Exception
            MessageBox.Show("Đã xảy ra lỗi, xin kiểm tra lại" & vbNewLine & "Dưới đây là mã lỗi: " & vbNewLine & String.Format("Error: {0}", ex.Message))
        End Try
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            PictureBox1.Image = Tao_QR_Code(TextBox1.Text, New Size(256, 256))
        Catch ex As Exception
            MessageBox.Show("Đã xảy ra lỗi, xin kiểm tra lại" & vbNewLine & "Dưới đây là mã lỗi: " & vbNewLine & String.Format("Error: {0}", ex.Message))
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

        QR_Image = New Bitmap(PictureBox1.ClientSize.Width, PictureBox1.ClientSize.Height)
            Dim gr As Graphics = Graphics.FromImage(QR_Image)
            gr.CopyFromScreen(PictureBox1.PointToScreen(Point.Empty), Point.Empty, PictureBox1.ClientSize)

            Dim sfd As New SaveFileDialog
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            sfd.Filter = "JPEG (*.jpg)|*.jpg"
            sfd.FilterIndex = 0
            sfd.AddExtension = True
            If sfd.ShowDialog = DialogResult.OK Then
            QR_Image.Save(sfd.FileName, Drawing.Imaging.ImageFormat.Jpeg)
            Label2.Text = "Đã lưu thành công"
        End If

    End Sub
End Class
