Imports System.Data.SqlClient

Public Class Transaksi

    Sub Kosongkan()
        TextBox1.Clear()
        DateTimePicker1.Text = ""
        ComboBox1.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        ComboBox2.Text = ""
        TextBox12.Clear()
        TextBox13.Clear()
        TextBox14.Clear()
        TextBox1.Focus()
    End Sub

    Sub enable()
        DateTimePicker1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox12.Enabled = False
        TextBox14.Enabled = False
    End Sub

    Sub databaru()
        DateTimePicker1.Text = ""
        ComboBox1.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        ComboBox2.Text = ""
        TextBox12.Clear()
        TextBox13.Clear()
        TextBox14.Clear()
        TextBox1.Focus()
    End Sub

    Sub Ikan()
        Call Koneksi()
        CMD = New SqlCommand(" select * from Ikan", CONN)
        DR = CMD.ExecuteReader
        Do While DR.Read()
            ComboBox2.Items.Add(DR.Item("Id_Ikan"))
        Loop
    End Sub

    Sub Pembeli()
        Call Koneksi()
        CMD = New SqlCommand(" select * from Pembeli", CONN)
        DR = CMD.ExecuteReader
        Do While DR.Read()
            ComboBox1.Items.Add(DR.Item("Id_Pembeli"))
        Loop
    End Sub

    Sub tampilgrid()
        Call Koneksi()
        DA = New SqlDataAdapter("select * from Transaksi", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DGV.DataSource = DS.Tables(0)
        DGV.ReadOnly = True
    End Sub

    Sub kodeotomatis()
        Call Koneksi()
        TextBox1.Enabled = False
        CMD = New SqlCommand("SELECT * from Transaksi order by Id_Transaksi desc", CONN)
        DR = CMD.ExecuteReader
        DR.Read()

        If Not DR.HasRows Then
            TextBox1.Text = "TR-0001"
        Else
            TextBox1.Text = Val(Microsoft.VisualBasic.Mid(DR.Item("Id_Transaksi").ToString, 5, 7)) + 1

            If Len(TextBox1.Text) = 1 Then
                TextBox1.Text = "TR-000" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 2 Then
                TextBox1.Text = "TR-00" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 3 Then
                TextBox1.Text = "TR-0" & TextBox1.Text & ""
            End If
        End If
    End Sub

    Sub kondisiawal()
        Call tampilgrid()
        Call Kosongkan()
        Call kodeotomatis()
        Call enable()
    End Sub

    Private Sub Form_Admin_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call Ikan()
        Call Pembeli()
        Call Koneksi()
        Call tampilgrid()
        Call Kosongkan()
        Call kodeotomatis()
        Call enable()
        lblTanggal.Text = DateTime.Now.ToString("ddd-MMMM-yyyy")
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        lblWaktu.Text = DateTime.Now.ToString("H:mm:ss")
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or DateTimePicker1.Text = "" Or ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox2.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            Try
                Call Koneksi()
                CMD = New SqlCommand("select * from Transaksi where Id_Transaksi='" & TextBox1.Text & "'", CONN)
                DR = CMD.ExecuteReader
                DR.Read()
                If Not DR.HasRows Then
                    Call Koneksi()
                    Dim SIMPAN As String = " insert into Transaksi values('" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox2.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')"
                    CMD = New SqlCommand(SIMPAN, CONN)
                    CMD.ExecuteNonQuery()
                    Dim SIMPAN2 As String = " insert into Detail values('" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox2.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')"
                    CMD = New SqlCommand(SIMPAN2, CONN)
                    CMD.ExecuteNonQuery()
                    CMD = New SqlCommand("select * from Ikan where Id_Ikan='" & ComboBox2.Text & "'", CONN)
                    DR = CMD.ExecuteReader
                    DR.Read()
                    If DR.HasRows Then
                        Call Koneksi()
                        Dim KURANGI As String = " update Ikan set STOK='" & DR.Item("STOK") - TextBox9.Text & "' where Id_Ikan='" & ComboBox2.Text & "'"
                        CMD = New SqlCommand(KURANGI, CONN)
                        CMD.ExecuteNonQuery()
                        Call Kosongkan()
                    End If
                    Call kondisiawal()
                Else
                    Call Koneksi()
                    Dim edit As String = " update Transaksi set Tanggal='" & DateTimePicker1.Text & "',Id_Pembeli='" & ComboBox1.Text & "',Nama='" & TextBox2.Text & "',Alamat='" & TextBox3.Text & "',No_Telepon='" & TextBox4.Text & "',Id_Ikan='" & ComboBox2.Text & "'Jenis='" & TextBox5.Text & "',Ukuran='" & TextBox6.Text & "',Stok='" & TextBox7.Text & "',Harga='" & TextBox8.Text & "',Jumlah_Beli='" & TextBox9.Text & "',Total='" & TextBox12.Text & "',Bayar='" & TextBox13.Text & "' where Id_Transaksi'" & TextBox1.Text & "'"
                    CMD = New SqlCommand(edit, CONN)
                    CMD.ExecuteNonQuery()
                    Dim edit2 As String = " update Detail set Tanggal='" & DateTimePicker1.Text & "',Id_Pembeli='" & ComboBox1.Text & "',Nama='" & TextBox2.Text & "',Alamat='" & TextBox3.Text & "',No_Telepon='" & TextBox4.Text & "',Id_Ikan='" & ComboBox2.Text & "'Jenis='" & TextBox5.Text & "',Ukuran='" & TextBox6.Text & "',Stok='" & TextBox7.Text & "',Harga='" & TextBox8.Text & "',Jumlah_Beli='" & TextBox9.Text & "',Total='" & TextBox12.Text & "',Bayar='" & TextBox13.Text & "' where Id_Transaksi'" & TextBox1.Text & "'"
                    CMD = New SqlCommand(edit2, CONN)
                    CMD.ExecuteNonQuery()
                    CMD = New SqlCommand("select * from Ikan where Id_Ikan='" & ComboBox2.Text & "'", CONN)
                    DR = CMD.ExecuteReader
                    DR.Read()
                    If DR.HasRows Then
                        Call Koneksi()
                        Dim KURANGI As String = " update Ikan set STOK='" & DR.Item("STOK") - TextBox9.Text & "' where Id_Ikan='" & ComboBox2.Text & "'"
                        CMD = New SqlCommand(KURANGI, CONN)
                        CMD.ExecuteNonQuery()
                        Call Kosongkan()
                    End If
                    Call kondisiawal()
                End If
                Call kodeotomatis()
                Call enable()
            Catch EX As Exception
                MsgBox(EX.Message)
            End Try
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Call Kosongkan()
        Call kodeotomatis()
        Call enable()
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Me.Visible = False
        Menu_Utama.Show()
    End Sub

    Private Sub DGV_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
        On Error Resume Next
        TextBox1.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        DateTimePicker1.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        ComboBox1.Text = DGV.Rows(e.RowIndex).Cells(2).Value
        TextBox2.Text = DGV.Rows(e.RowIndex).Cells(3).Value
        TextBox3.Text = DGV.Rows(e.RowIndex).Cells(4).Value
        TextBox4.Text = DGV.Rows(e.RowIndex).Cells(5).Value
        ComboBox2.Text = DGV.Rows(e.RowIndex).Cells(6).Value
        TextBox5.Text = DGV.Rows(e.RowIndex).Cells(7).Value
        TextBox6.Text = DGV.Rows(e.RowIndex).Cells(8).Value
        TextBox7.Text = DGV.Rows(e.RowIndex).Cells(9).Value
        TextBox8.Text = DGV.Rows(e.RowIndex).Cells(10).Value
        TextBox9.Text = DGV.Rows(e.RowIndex).Cells(11).Value
        TextBox12.Text = DGV.Rows(e.RowIndex).Cells(12).Value
        TextBox13.Text = DGV.Rows(e.RowIndex).Cells(13).Value
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("anda harus mengisi data dulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Hapus Data Ini...?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                Dim hapus As String = "delete from Transaksi where Id_Transaksi='" & TextBox1.Text & "'"
                CMD = New SqlCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call Kosongkan()
                Call tampilgrid()
                Call kodeotomatis()
                Call enable()
            Else
                Call Kosongkan()
                Call kodeotomatis()
                Call enable()
            End If
        End If
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox15.TextChanged
        Call Koneksi()
        CMD = New SqlCommand("select * from Transaksi where Id_Transaksi like '%" & TextBox15.Text & "%'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            Call Koneksi()
            DA = New SqlDataAdapter("select * from Transaksi where Id_Transaksi like '%" & TextBox15.Text & "%'", CONN)
            DS = New DataSet
            DA.Fill(DS)
            DGV.DataSource = DS.Tables(0)
        Else
            MsgBox("ID TRANSAKSI TIDAK DI TEMUKAN")
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        Try
            Dim TX7 As Double = TextBox7.Text
            Dim TX8 As Double = TextBox8.Text
            Dim TX9 As Double = TextBox9.Text
            If TX9 > TX7 Then
                MsgBox("Jumlah Beli Melebihi Stok")
                TextBox9.Clear()
            ElseIf TX9 = 0 Then
                MsgBox("Jumlah Beli Tidak Boleh Nol")
            ElseIf TX9 < 0 Then
                MsgBox("Jumlah Beli Tidak Boleh Kurang Dari Nol")
            Else
                TextBox12.Text = TX9 * TX8
            End If
        Catch EX As Exception
            MsgBox(EX.Message)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click
        Dim TX12 As Double = TextBox12.Text
        Dim TX13 As Double = TextBox13.Text
        If TX13 < TX12 Then
            MsgBox("Uang Tidak Cukup")
        Else
            TextBox14.Text = TX13 - TX12
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call Koneksi()
        CMD = New SqlCommand("select * from Pembeli where Id_Pembeli='" & ComboBox1.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            TextBox2.Text = DR.Item("Nama")
            TextBox3.Text = DR.Item("Alamat")
            TextBox4.Text = DR.Item("No_Telepon")
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Call Koneksi()
        Struk.Show()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Call Koneksi()
        CMD = New SqlCommand("select * from Ikan where Id_Ikan='" & ComboBox2.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            TextBox5.Text = DR.Item("Jenis")
            TextBox6.Text = DR.Item("Ukuran")
            TextBox7.Text = DR.Item("Stok")
            TextBox8.Text = DR.Item("Harga")
        End If
    End Sub
End Class
