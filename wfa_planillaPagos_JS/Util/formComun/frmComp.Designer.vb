﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmComp))
        Me.btnConectar = New System.Windows.Forms.Button()
        Me.cboComp = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtContrasena = New System.Windows.Forms.TextBox()
        Me.btnHabil = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnConectar
        '
        Me.btnConectar.Location = New System.Drawing.Point(141, 108)
        Me.btnConectar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnConectar.Name = "btnConectar"
        Me.btnConectar.Size = New System.Drawing.Size(149, 28)
        Me.btnConectar.TabIndex = 5
        Me.btnConectar.Tag = "sub_conectar"
        Me.btnConectar.Text = "Conectar"
        Me.btnConectar.UseVisualStyleBackColor = True
        '
        'cboComp
        '
        Me.cboComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComp.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboComp.FormattingEnabled = True
        Me.cboComp.Location = New System.Drawing.Point(116, 17)
        Me.cboComp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboComp.Name = "cboComp"
        Me.cboComp.Size = New System.Drawing.Size(312, 21)
        Me.cboComp.TabIndex = 4
        Me.cboComp.Tag = "company"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(23, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Compañia"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 49)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Usuario"
        '
        'txtUsuario
        '
        Me.txtUsuario.Enabled = False
        Me.txtUsuario.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Location = New System.Drawing.Point(116, 47)
        Me.txtUsuario.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(201, 21)
        Me.txtUsuario.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 78)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Contraseña"
        '
        'txtContrasena
        '
        Me.txtContrasena.Enabled = False
        Me.txtContrasena.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContrasena.Location = New System.Drawing.Point(116, 76)
        Me.txtContrasena.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContrasena.Size = New System.Drawing.Size(239, 21)
        Me.txtContrasena.TabIndex = 9
        '
        'btnHabil
        '
        Me.btnHabil.Location = New System.Drawing.Point(327, 47)
        Me.btnHabil.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnHabil.Name = "btnHabil"
        Me.btnHabil.Size = New System.Drawing.Size(29, 22)
        Me.btnHabil.TabIndex = 10
        Me.btnHabil.UseVisualStyleBackColor = True
        '
        'frmComp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(452, 140)
        Me.Controls.Add(Me.btnHabil)
        Me.Controls.Add(Me.txtContrasena)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUsuario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnConectar)
        Me.Controls.Add(Me.cboComp)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmComp"
        Me.Text = "Seleccionar Compañia"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConectar As System.Windows.Forms.Button
    Friend WithEvents cboComp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtContrasena As System.Windows.Forms.TextBox
    Friend WithEvents btnHabil As System.Windows.Forms.Button
End Class
