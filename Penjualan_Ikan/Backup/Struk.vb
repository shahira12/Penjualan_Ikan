Imports System.Data.SqlClient
Public Class Struk
    Sub struk()
        Call Koneksi()
        CMD = New SqlCommand(" select * from Transaksi", CONN)
        DR = CMD.ExecuteReader
        Do While DR.Read()
            ComboBox1.Items.Add(DR.Item("Id_Transaksi"))
        Loop
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If ComboBox1.Text = "" Then
                MsgBox("Pilih Id Transaksi terlebih dahulu")
                ComboBox1.Focus()
            Else
                Cetak.CrystalReportViewer1.SelectionFormula = "{Transaksi.Id_Transaksi}='" & ComboBox1.Text & "'"
                Cetak.CrystalReportViewer1.RefreshReport()
                Cetak.WindowState = FormWindowState.Maximized
                Cetak.Show()
                ComboBox1.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Struk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call struk()
    End Sub
End Class