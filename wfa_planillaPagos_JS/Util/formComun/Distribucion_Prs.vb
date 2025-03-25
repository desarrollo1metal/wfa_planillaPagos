Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Util
Imports System.Data.OleDb
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Reflection

Public Class FrmDistribucion_Prs


    Public Monto As Double
    Public ID As Integer
    Public Operacion As String
    Private Sub btn_añadir_Click(sender As System.Object, e As System.EventArgs) Handles btn_añadir.Click

        If Decimal.Compare(Convert.ToDecimal(Me.txt_saldo.Text), 0D) <= 0 Then
            Return
        End If

        Me.dgv_distribucion.Rows.Add((Me.Operacion & "/" & (Me.dgv_distribucion.Rows.Count + 1).ToString()), "0")
        Me.dgv_distribucion.CurrentCell = Me.dgv_distribucion.Rows(Me.dgv_distribucion.Rows.Count - 1).Cells(1)
        Me.dgv_distribucion.BeginEdit(True)

    End Sub

    Private Sub FrmDistribucion_Prs_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txt_saldo.Text = Convert.ToString(Monto)
        txt_operacion.Text = Operacion
        txt_id.Text = ID.ToString()

        formateargrilla(dgv_distribucion)
        dgv_distribucion.Columns(0).ReadOnly = True
        dgv_distribucion.Columns(0).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(231, 231, 231)

    End Sub

    Private Sub dgv_distribucion_CellEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_distribucion.CellEnter
        dgv_distribucion.Rows(e.RowIndex).Cells(1).Style.BackColor = System.Drawing.Color.FromArgb(254, 240, 158)

    End Sub

    Private Sub dgv_distribucion_CellLeave(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_distribucion.CellLeave
        dgv_distribucion.Rows(e.RowIndex).Cells(1).Style.BackColor = System.Drawing.Color.White

    End Sub

    Sub formateargrilla(grilla As DataGridView)

        grilla.RowHeadersVisible = False
        grilla.RowHeadersWidth = 31
        grilla.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(231, 231, 231)
        grilla.RowHeadersDefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8)
        grilla.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grilla.RowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(194, 200, 205)
        grilla.RowHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        grilla.BackgroundColor = System.Drawing.Color.FromArgb(231, 231, 231)
        grilla.BackColor = System.Drawing.Color.FromArgb(231, 231, 231)
        grilla.GridColor = System.Drawing.Color.FromArgb(204, 204, 204)
        grilla.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(231, 231, 231)
        grilla.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        grilla.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 7.4F)
        grilla.RowTemplate.Height = 16
        grilla.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(231, 231, 231)
        grilla.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black
        grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        grilla.ColumnHeadersHeight = 16
        grilla.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(252, 221, 130)
        grilla.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black

    End Sub

    Private Sub dgv_distribucion_DoubleClick(sender As System.Object, e As System.EventArgs) Handles dgv_distribucion.DoubleClick
        If dgv_distribucion.SelectedRows.Count > 0 Then
            dgv_distribucion.Rows.RemoveAt(dgv_distribucion.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub dgv_distribucion_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_distribucion.CellEndEdit
        Dim num1 As Double = 0.0
        Try
            For Each row As DataGridViewRow In Me.dgv_distribucion.Rows
                num1 = Conversions.ToDouble(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(Math.Round(num1, 2), row.Cells(1).Value))
            Next
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        If Convert.ToDouble(Me.txt_saldo.Text) <= 0.0 Then
            Me.dgv_distribucion.ReadOnly = True
            Dim num2 As Integer = MessageBox.Show("Imposible distribuir monto, el monto distribuido supera al monto inicial")
            Me.dgv_distribucion.CurrentCell = Me.dgv_distribucion.Rows(Me.dgv_distribucion.Rows.Count - 1).Cells(1)
            Me.dgv_distribucion.BeginEdit(True)
        Else
            Me.txt_saldo.Text = Conversions.ToString(Me.Monto - Conversions.ToDouble(Convert.ToString(Math.Round(num1, 2))))
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dataSource As DataTable = DirectCast(Me.dgv_distribucion.DataSource, DataTable)
        Try
            If Convert.ToDouble(Me.txt_saldo.Text) = 0.0 Then
                If Me.dgv_distribucion.Rows.Count <> 0 Then
                    Try
                        For Each row As DataGridViewRow In DirectCast(Me.dgv_distribucion.Rows, IEnumerable)
                            Dim str As String = Conversions.ToString(row.Cells(0).Value)
                            Dim d1 As Decimal = Conversions.ToDecimal(row.Cells(1).Value)
                            If Decimal.Compare(d1, 0D) <= 0 Then
                                Dim num As Integer = CInt(MsgBox("Existen valores 0 en alguna de las lineas, ingrese un valor valido", MsgBoxStyle.OkOnly))
                                Return
                            End If
                            ModuleSQLComun.dtb_ejecutarSQL_NET("exec gmi_sp_Distribuir_Prs '" & Conversions.ToString(Convert.ToInt32(Me.txt_id.Text)) & "','" & str & "'," & Conversions.ToString(d1))
                        Next
                    Finally
                        Dim enumerator As IEnumerator
                        If TypeOf enumerator Is IDisposable Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                    Dim num1 As Integer = CInt(MsgBox("Operacion finalizada con exito", MsgBoxStyle.OkOnly))
                    Me.Close()
                End If
            Else
                Dim num2 As Integer = MessageBox.Show("Imposible distribuir monto, el monto distribuido supera al monto inicial")
            End If
        Catch ex As Exception
            ProjectData.SetProjectError(ex)
            ModuleGlobalDatos.sub_mostrarMensaje(ex.Message, Assembly.GetExecutingAssembly().GetName().Name, "sqlCargarEC", MethodBase.GetCurrentMethod().Name, 3)
            ProjectData.ClearProjectError()
        End Try
    End Sub
End Class