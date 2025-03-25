<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDistribucion_Prs
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_distribucion = New System.Windows.Forms.DataGridView()
        Me.Id_g = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monto_g = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_saldo = New System.Windows.Forms.TextBox()
        Me.txt_id = New System.Windows.Forms.TextBox()
        Me.txt_operacion = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btn_añadir = New System.Windows.Forms.Button()
        CType(Me.dgv_distribucion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_distribucion
        '
        Me.dgv_distribucion.AllowUserToAddRows = False
        Me.dgv_distribucion.AllowUserToDeleteRows = False
        Me.dgv_distribucion.AllowUserToResizeColumns = False
        Me.dgv_distribucion.AllowUserToResizeRows = False
        Me.dgv_distribucion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgv_distribucion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_distribucion.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_g, Me.Monto_g})
        Me.dgv_distribucion.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv_distribucion.Location = New System.Drawing.Point(12, 12)
        Me.dgv_distribucion.Name = "dgv_distribucion"
        Me.dgv_distribucion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_distribucion.Size = New System.Drawing.Size(206, 150)
        Me.dgv_distribucion.TabIndex = 0
        '
        'Id_g
        '
        Me.Id_g.HeaderText = "Operación"
        Me.Id_g.Name = "Id_g"
        Me.Id_g.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Id_g.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Monto_g
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Monto_g.DefaultCellStyle = DataGridViewCellStyle2
        Me.Monto_g.HeaderText = "Monto"
        Me.Monto_g.Name = "Monto_g"
        Me.Monto_g.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Monto_g.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Monto_g.Width = 60
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 7.4!)
        Me.Label1.Location = New System.Drawing.Point(122, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Saldo"
        '
        'txt_saldo
        '
        Me.txt_saldo.Font = New System.Drawing.Font("Tahoma", 7.4!)
        Me.txt_saldo.Location = New System.Drawing.Point(164, 172)
        Me.txt_saldo.Name = "txt_saldo"
        Me.txt_saldo.ReadOnly = True
        Me.txt_saldo.Size = New System.Drawing.Size(54, 19)
        Me.txt_saldo.TabIndex = 3
        Me.txt_saldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_id
        '
        Me.txt_id.Location = New System.Drawing.Point(49, 62)
        Me.txt_id.Name = "txt_id"
        Me.txt_id.Size = New System.Drawing.Size(75, 20)
        Me.txt_id.TabIndex = 4
        Me.txt_id.Visible = False
        '
        'txt_operacion
        '
        Me.txt_operacion.Location = New System.Drawing.Point(49, 36)
        Me.txt_operacion.Name = "txt_operacion"
        Me.txt_operacion.Size = New System.Drawing.Size(75, 20)
        Me.txt_operacion.TabIndex = 6
        Me.txt_operacion.Visible = False
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.Util.My.Resources.Resources.btn_enfasis
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 7.4!)
        Me.Button1.Location = New System.Drawing.Point(12, 172)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 20)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Crear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btn_añadir
        '
        Me.btn_añadir.BackgroundImage = Global.Util.My.Resources.Resources.Button_INCREASE
        Me.btn_añadir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_añadir.FlatAppearance.BorderSize = 0
        Me.btn_añadir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_añadir.Location = New System.Drawing.Point(224, 12)
        Me.btn_añadir.Name = "btn_añadir"
        Me.btn_añadir.Size = New System.Drawing.Size(21, 18)
        Me.btn_añadir.TabIndex = 1
        Me.btn_añadir.UseVisualStyleBackColor = True
        '
        'FrmDistribucion_Prs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(250, 197)
        Me.Controls.Add(Me.txt_operacion)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txt_id)
        Me.Controls.Add(Me.txt_saldo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_añadir)
        Me.Controls.Add(Me.dgv_distribucion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmDistribucion_Prs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Distribucion de pagos recibidos"
        CType(Me.dgv_distribucion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv_distribucion As System.Windows.Forms.DataGridView
    Friend WithEvents btn_añadir As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_saldo As System.Windows.Forms.TextBox
    Friend WithEvents txt_id As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txt_operacion As System.Windows.Forms.TextBox
    Friend WithEvents Id_g As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto_g As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
